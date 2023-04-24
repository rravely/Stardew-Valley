using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craftable : MonoBehaviour
{
    //craft resource database
    [Header("Item resources")]
    [SerializeField]private List<Item> resourceItems;
    private Dictionary<int, int> resourceItemDict = new Dictionary<int, int>();

    //inventory
    [Header("Inventory")]
    [SerializeField]private GameObject inventoryPanel;
    private List<Transform> inventorySlots = new List<Transform>();

    //change alpha
    private bool craftableResult;

    void Start() {
        for (int i = 0; i < 36; i++) {
            inventorySlots.Add(inventoryPanel.transform.GetChild(i)); //panel에 있는 슬롯들 리스트에 추가
        }
        ChangeItemListToDict();
    }

    void Update() {
        craftableResult = CheckCraftable();
        if(craftableResult) {
            ChangeAlpha(1);
        }
        else {
            ChangeAlpha(0);
        }
    }

    private void ChangeItemListToDict() {
        //입력된 리스트에서 중복된 값을 딕셔너리로 정리
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
}
