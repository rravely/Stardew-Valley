using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots; 
    public GameObject inventoryItemPrefab;
    public GameObject selectedSquare, itemInSlot;
    private int maxStackedItems = 9;
    private int selectedSlot = -1;

    void Start() {
        ChangeSelectedSlot(0);
    }

/*
    void Update() {
        //선택한 slot이 null이 아니고 이미 선택되어진게 아니라면
        for (int i = 0; i < inventorySlots.Length; i++) {
            SlotItem slotItem = inventorySlots[i].GetComponentInChildren<SlotItem>();
            if (slotItem.clicked) {
                inventorySlots[i].Selected();
            }
            else{
                inventorySlots[i].Selected();
            }
        }
    }
    */

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0) {
            inventorySlots[selectedSlot].Deselected();
            
        }
        InventorySlot slot = inventorySlots[newValue];
        SlotItem itemInSlot = slot.GetComponentInChildren<SlotItem>();
        if (itemInSlot != null) {
            inventorySlots[newValue].Selected();
            selectedSlot = newValue;
        }
        
    }

    public bool AddItem(Item item) //아이템 추가
    {
        //같은 아이템이 있고 그 아이템이 stackble하다면 count만 늘린다
        for (int i = 0; i < inventorySlots.LongLength; i++)
        {
            InventorySlot slot = inventorySlots[i];
            SlotItem itemInSlot = slot.GetComponentInChildren<SlotItem>();
            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.item.stackable && itemInSlot.count <= maxStackedItems) {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        //빈 슬롯 찾기
        for (int i = 0; i < inventorySlots.LongLength; i++) {
            InventorySlot slot = inventorySlots[i];
            SlotItem itemInSlot = slot.GetComponentInChildren<SlotItem>();
            if (itemInSlot == null) {
                SpawnItem(item, slot);
                return true;
            }
        }
        return false;
    }

    void SpawnItem(Item item, InventorySlot slot) //아이템 생성
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        SlotItem slotItem = newItemGo.GetComponent<SlotItem>();
        slotItem.InitialiseItem(item);
    }
}
