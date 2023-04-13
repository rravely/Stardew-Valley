using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> inventory;

    [SerializeField]private GameManager gameManager;

    public SaveData player;
    public UIInventory inventoryUI;
    public bool activeInventory2Line = false, activeInventory3Line = false;

    void Start() {
        //현재 플레이어의 savedata를 가지고 와서 저장
        player = gameManager.player;
        Debug.Log(player.saveInventory.Count);
        for (int i = 0; i < player.saveInventory.Count; i++)
        {
            inventoryUI.AddNewItem(player.saveInventory[i]);
        }
    }

    void Update() {
        
        
    }


    public void GiveItem(int Id) 
    {
        Item itemToAdd = ItemDatabase.GetItem(Id);
        inventory.Add(itemToAdd);
        //Debug.Log(itemToAdd.ItemName);
        //inventoryUI.AddNewItem(itemToAdd); 
    }

    public void GiveItem(string ItemName)
    {
        Item itemToAdd = ItemDatabase.GetItem(ItemName);
        inventory.Add(itemToAdd);
        //inventoryUI.AddNewItem(itemToAdd);
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
            //inventoryUI.RemoveItem(itemToRemove);
        }
    }
}
