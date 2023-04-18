using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemManager : MonoBehaviour
{
    //for selectedItem
    [HideInInspector]public bool isHold;
    public int itemCount;
    public string  selectedItemName;
    [SerializeField]private Image selectedItemIcon;
    [SerializeField]private Text itemCountView;

    //for mouse position
    [SerializeField]Camera camera;

    //shop item list
    [SerializeField]private InventoryManager inventoryManager = new InventoryManager();
    [SerializeField]private List<Item> shopItemList = new List<Item>();


    void Start() {
        isHold = false;
    }

    void Update() {
        ViewSelectedItem();
        BuyItems();
        if (!isHold) { //아이템을 잡고 있지 않으면
            selectedItemIcon.gameObject.SetActive(false);
        }
    }

    private void ViewSelectedItem() {
        selectedItemIcon.transform.position = new Vector3(camera.ScreenToWorldPoint(Input.mousePosition).x + 0.1f, camera.ScreenToWorldPoint(Input.mousePosition).y - 0.1f, 0f);
        //선택한 아이템 개수 보여주기
        itemCountView.text = itemCount.ToString();
    }

    private void BuyItems() {
        if (Input.GetKeyDown(KeyCode.Return)) //엔터 누르면 구매
        {
            for (int i = 0; i < shopItemList.Count; i++) {
                if (shopItemList[i].itemName.Equals(selectedItemName)) {
                    //헤당 아이템 인벤토리에 추가
                    for (int j = 0; j < itemCount; j++) {
                        bool addItemResult = inventoryManager.AddItem(shopItemList[i]);
                        //bool calculateMoney =
                        if (addItemResult) {    // 잘 추가되면
                            Debug.Log("구매 완료");
                        }
                        else {
                            Debug.Log("구매 실패");
                        }
                        isHold = false;
                    }
                }
            }
        }
    }
}
