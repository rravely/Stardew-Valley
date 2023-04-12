using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public List<UIItem> uiItems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int currentUIItemsCount = 0;
    public int numberOfSlots = 12;

    private void Awake() {
        for (int i = 0; i < numberOfSlots; i++) {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
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
        UpdateSlot(uiItems.FindIndex(i => i.item == null), item);
    }

    public void RemoveItem(Item item)
    {
        UpdateSlot(uiItems.FindIndex(i => i.item == item), null);
    }
}
