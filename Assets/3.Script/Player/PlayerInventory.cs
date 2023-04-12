using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();

    public ItemDatabase itemDatabase;
    public UIInventory inventoryUI;
    public bool activeInventory2Line = false, activeInventory3Line = false;

    void Start() {
        GiveItem(0);
        GiveItem(1);
        GiveItem(2);
        GiveItem(3);
        GiveItem(4);
    }

    void Update() {

    }

    public void GiveItem(int Id) 
    {
        Item itemToAdd = itemDatabase.GetItem(Id);
        inventory.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd); 
    }

    public void GiveItem(string ItemName)
    {
        Item itemToAdd = itemDatabase.GetItem(ItemName);
        inventory.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
    }

    public Item checkForItem(int Id) 
    {
        return inventory.Find(item => item.Id == Id);
    }

    public void RemoveItem(int Id)
    {
        Item itemToRemove = checkForItem(Id);
        if (itemToRemove != null)
        {
            inventory.Remove(itemToRemove);
            inventoryUI.RemoveItem(itemToRemove);
        }
    }
}
