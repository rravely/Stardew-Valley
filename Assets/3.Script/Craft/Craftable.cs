using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Craftable : MonoBehaviour
{
    //craft resource database
    [Header("Item resources")]
    [SerializeField]private List<Item> resourceItems;
    private Dictionary<int, int> resourceItemDict = new Dictionary<int, int>();

    //result item
    [Header("Result")]
    [SerializeField]private Item item;

    //inventory
    [Header("Inventory")]
    [SerializeField]private GameObject inventoryPanel;
    private List<Transform> inventorySlots = new List<Transform>();

    //change alpha
    private bool craftableResult;
    
    //for mouse double click
    float interval = 0.25f;
    float doubleClickedTime = -1.0f;
    bool isDoubleClicked = false;

    //Add item in inventory
    private InventoryManager inventoryManager;

    void Start() {
        for (int i = 0; i < 36; i++) {
            inventorySlots.Add(inventoryPanel.transform.GetChild(i)); //panel에 있는 슬롯들 리스트에 추가
        }
        ChangeItemListToDict();
        
        inventoryManager = GameObject.FindWithTag("InventoryManager").GetComponent<InventoryManager>();
    }

    void Update() {
        craftableResult = CheckCraftable();
        if(craftableResult) {
            ChangeAlpha(1);
        }
        else {
            ChangeAlpha(0);
        }

        if (isDoubleClicked) {
            Debug.Log("더블클릭");
            isDoubleClicked = false;

            MakeCraft();
            ItemSubstract();
        }
    }

    private void ChangeItemListToDict() {
        //입력된 리스트에서 중복된 값을 딕셔너리로 정리 
        //중복된 것만 확인해주므로 재료가 하나 들어갈 경우 코드 추가 필요
        var result = resourceItems.GroupBy(x => x.id)
                        .Where(g => g.Count() > 1)
                        .ToDictionary(x => x.Key, x => x.Count());
        resourceItemDict = result;
    }

    private bool CheckCraftable() {
        List<int> result = new List<int>();
        //인벤토리 아이템과 제작에 필요한 아이템 비교
        foreach (KeyValuePair<int, int> item in resourceItemDict) 
        {
            for (int i = 0; i < inventorySlots.Count; i++) {
                Transform inventorySlot = inventorySlots[i];
                SlotItem itemInSlot = inventorySlot.GetComponentInChildren<SlotItem>();
                if (itemInSlot != null && itemInSlot.item.id == item.Key && itemInSlot.count >= item.Value) { //딕셔너리에 있는 아이템이 인벤토리 슬롯에 존재하고 그 개수가 충분하면
                    result.Add(0); //0더하기
                    break;
                } 
            }
        }
        if (result.Count.Equals(resourceItemDict.Count)) { //개수가 충분해서 0이 추가된 아이템과 딕셔너리의 아이템의 개수가 같다면
            return true;
        } else {
            return false;
        }
    }

    private void ChangeAlpha(int num) {
        if (num.Equals(0)) {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        }
        else {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        }
    }

    public void OnMouseUp() {
        if((Time.time - doubleClickedTime) < interval)
        {
            isDoubleClicked = true;
            doubleClickedTime = -1.0f;
        }
        else
        {
            isDoubleClicked = false;
            doubleClickedTime = Time.time;
        }
    }

    private void MakeCraft() {
        //더블클릭하면 해당 아이템 제작
        inventoryManager.AddItem(item);
    }

    private void ItemSubstract() {
        //아이템 제거
        foreach (KeyValuePair<int, int> item in resourceItemDict) 
        {
            for (int i = 0; i < inventorySlots.Count; i++) {
                Transform inventorySlot = inventorySlots[i];
                SlotItem itemInSlot = inventorySlot.GetComponentInChildren<SlotItem>();
                if (itemInSlot != null && itemInSlot.item.id == item.Key && itemInSlot.count >= item.Value) { //딕셔너리에 있는 아이템이 인벤토리 슬롯에 존재하고 그 개수가 충분하면
                    itemInSlot.count -= item.Value;
                    break;
                } 
            }
        }
    }

}
