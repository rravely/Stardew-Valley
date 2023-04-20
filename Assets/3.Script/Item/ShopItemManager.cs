using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItemManager : MonoBehaviour, IDropHandler
{
    //for player info
    [SerializeField]private GameManager gameManager;
    [SerializeField]private Text playerMoney;

    //for selectedItem
    [HideInInspector]public bool isHold;
    [HideInInspector]public int itemCount;
    [HideInInspector]public string  selectedItemName;
    [SerializeField]private Image selectedItemIcon;
    [SerializeField]private Text itemCountView;

    //for mouse position
    [SerializeField]Camera shopCamera;

    //shop item list
    [SerializeField]private InventoryManager inventoryManager;
    [SerializeField]private List<Item> shopItemList = new List<Item>();

    //no Money
    [SerializeField]private GameObject noMoneyUI;

    //for sell item
    private Shop shop;


    void Start() {
        isHold = false;
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        shop = GameObject.FindWithTag("Town").GetComponent<Shop>();
    }

    void Update() {
        ViewSelectedItem();
        BuyItems();
        if (!isHold) { //아이템을 잡고 있지 않으면
            selectedItemIcon.gameObject.SetActive(false);
        }
        UpdatePlayerMoney();
    }

    private void ViewSelectedItem() {
        selectedItemIcon.transform.position = new Vector3(shopCamera.ScreenToWorldPoint(Input.mousePosition).x + 0.1f, shopCamera.ScreenToWorldPoint(Input.mousePosition).y - 0.1f, 0f);
        //선택한 아이템 개수 보여주기
        itemCountView.text = itemCount.ToString();
    }

    //아이템 사기
    private void BuyItems() {
        if (Input.GetKeyDown(KeyCode.Return)) //엔터 누르면 구매
        {
            for (int i = 0; i < shopItemList.Count; i++) {
                if (shopItemList[i].itemName.Equals(selectedItemName)) {
                    //헤당 아이템 인벤토리에 추가
                    for (int j = 0; j < itemCount; j++) {
                        if (gameManager.player.playerMoney >= shopItemList[i].cost) { //플레이어의 소지금이 충분하면
                            bool addItemResult = inventoryManager.AddItem(shopItemList[i]);
                            //bool calculateMoney =
                            if (addItemResult) {    // 잘 추가되면
                                gameManager.player.playerMoney -= shopItemList[i].cost;
                            }
                            else { //돈은 충분한데 인벤토리 자리가 없는 경우
                                
                            }
                            isHold = false;
                        }
                        else{ //플레이어의 소지금이 충분하지 않으면
                            noMoneyUI.SetActive(true);
                            isHold = false;
                            StartCoroutine("DisplayNoMoneyUI_co");
                        }
                    }
                }
            }
        }
    }

    public void OnDrop(PointerEventData eventData) {
        if (!eventData.pointerDrag.GetComponent<SlotItem>().item.isTool) { //도구가 아니라면 팔기
            gameManager.player.playerMoney += eventData.pointerDrag.GetComponent<SlotItem>().count * eventData.pointerDrag.GetComponent<SlotItem>().item.cost;
            Destroy(eventData.pointerDrag);
        }
    }

    //플레이어 돈 갱신
    private void UpdatePlayerMoney() {
        playerMoney.text = gameManager.player.playerMoney.ToString();
    }

    //알림창 꺼지기
    IEnumerator DisplayNoMoneyUI_co() {
        yield return new WaitForSeconds( 2f );
        noMoneyUI.SetActive(false);
    }
}
