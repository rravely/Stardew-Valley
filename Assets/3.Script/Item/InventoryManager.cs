using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots; 
    public GameObject inventoryItemPrefab;
    public GameObject selectedSquare, itemInSlot;
    [HideInInspector]public int selectedSlot = 0;
    private int maxStackedItems = 9;
    
    private PlayerControl playerControl;

    void Start() {
        playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        ChangeSelectedSlot(0);
    }


    void Update() {
        //if (Input.inputString != null) {
        //bool isNumber = int.TryParse(Input.inputString, out int number);
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ChangeSelectedSlot(0);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ChangeSelectedSlot(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            ChangeSelectedSlot(2);
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            ChangeSelectedSlot(3);
        } else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            ChangeSelectedSlot(4);
        } else if (Input.GetKeyDown(KeyCode.Alpha6)) {
            ChangeSelectedSlot(5);
        } else if (Input.GetKeyDown(KeyCode.Alpha7)) {
            ChangeSelectedSlot(6);
        } else if (Input.GetKeyDown(KeyCode.Alpha8)) {
            ChangeSelectedSlot(7);
        } else if (Input.GetKeyDown(KeyCode.Alpha9)) {
            ChangeSelectedSlot(8);
        } else if (Input.GetKeyDown(KeyCode.Alpha0)) {
            ChangeSelectedSlot(9);
        } else if (Input.GetKeyDown(KeyCode.Minus)) {
            ChangeSelectedSlot(10);
        } else if (Input.GetKeyDown(KeyCode.Plus)) {
            ChangeSelectedSlot(11);
        }
    }
    

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0 && inventorySlots[selectedSlot] != null) {
            inventorySlots[selectedSlot].Deselected();
        }
        InventorySlot slot = inventorySlots[newValue];
        SlotItem itemInSlot = slot.GetComponentInChildren<SlotItem>();
        if (itemInSlot != null) {
            inventorySlots[newValue].Selected();
            selectedSlot = newValue;
            playerControl.selectedToolId = itemInSlot.item.id;
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
