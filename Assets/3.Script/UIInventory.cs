using System.Collections;
using System.Collections.Generic;
using UnityEngine;


struct Items {
    bool isEmpty;
    UIItem uiItem;
}
public class UIInventory : MonoBehaviour
{
    public List<UIItem> uiItems = new List<UIItem>();
    //UIItem[] uIItems;
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int currentUIItemsCount = 0;
    public int numberOfSlots = 12;
    public int numberOfFullSlots = 0;

    private void Awake() {
        //uIItems = new UIItem[numberOfSlots]; //슬롯의 길이 만큼 UIItem 배열 선언

        //슬롯의 길이만큼 UIItem Prefab만들기
        for (int i = 0; i < numberOfSlots; i++) {
            GameObject instance = Instantiate(slotPrefab); //slotPrefab 생성
            instance.transform.SetParent(slotPanel); //slotPanel을 상속
            uiItems.Add(instance.GetComponentInChildren<UIItem>()); //slotPrefab의 자식(Item)으로부터 UIItem 컴포넌트 가져와서 리스트에 추가
        }
    }


    public void UpdateSlot(int slot, Item item)
    {
        uiItems[slot].UpdateItem(item);
        //currentUIItemsCount++;
    }

    public void AddNewItem(Item item) 
    {
        //UpdateSlot(currentUIItemsCount, item);
        if (numberOfFullSlots < numberOfSlots) //인벤토리 크기만큼 추가할 수 있게
        { 
            UpdateSlot(uiItems.FindIndex(i => i.item == null), item);
            numberOfFullSlots++;;
            Debug.Log("꽉 찬 인벤토리 슬롯 수: " + numberOfFullSlots);
        }
        
    }

    public void RemoveItem(Item item)
    {
        if (uiItems.Count > 0) 
        {
            UpdateSlot(uiItems.FindIndex(i => i.item == item), null);
        }
        
    }
}
