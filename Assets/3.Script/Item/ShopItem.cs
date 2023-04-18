using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    private Image itemIcon;
    private Text itemName;
    private Text itemCost;

    //Item select
    [SerializeField]private Image selectedItemIcon;
    private ShopItemManager shopItemManager;
    
    void Start() {
        //아이템 보여지는 슬롯에 채우기
        itemIcon = transform.GetChild(0).GetComponent<Image>();
        itemName = transform.GetChild(1).GetComponent<Text>();
        itemCost = transform.GetChild(2).GetComponent<Text>();

        itemIcon.sprite = item.icon;
        itemName.text = item.itemName;
        itemCost.text = item.cost.ToString();

        //부모 오브젝트에 있는 컴포넌트 가져오기
        shopItemManager = transform.GetComponentInParent<ShopItemManager>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left) //이 아이템이 선택됐을 때
        {
            if (!shopItemManager.isHold) //현재 잡고 있는 아이템이 없다면
            {
                selectedItemIcon.gameObject.SetActive(true);
                //아이템 집기
                selectedItemIcon.sprite = item.icon;
                shopItemManager.selectedItemName = item.itemName;
                shopItemManager.itemCount = 1;
                selectedItemIcon.raycastTarget = false; 

                shopItemManager.isHold = true;
            }
            else { //잡고 있는 아이템이 있다면
                if (item.itemName.Equals(shopItemManager.selectedItemName)) // 클릭한 아이템이 잡은 아이템과 같다면
                {
                    //선택한 아이템 개수 올려주기
                    shopItemManager.itemCount++;
                }
                else { //선택한 아이템이 이전에 잡은 아이템과 다르면
                    shopItemManager.itemCount = 1; //잡은 개수 초기화
                    selectedItemIcon.sprite = item.icon;
                    shopItemManager.selectedItemName = item.itemName;
                }
            }
        }
        Debug.Log(shopItemManager.selectedItemName + " " + shopItemManager.itemCount);
    }
    
}
