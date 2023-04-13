using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]private GameManager gameManager;

    //public SaveData player;
    public UIInventory inventoryUI;
    [SerializeField] private int inventoryLength = 12;
    private bool isHold = false; //마우스로 선택하고 있는지

    void Start() {
        //현재 플레이어의 savedata를 가지고 와서 저장
        Debug.Log(gameManager.inventory[2].isFull);
        int itemCount = 0;
        for (int i = 0; i < gameManager.inventory.Length; i++) {
            if (gameManager.inventory[i].isFull) {
                itemCount++;
            }
        }
        Debug.Log("현재 플레이어의 인벤토리에 들어있는 아이템 수:" + itemCount);
        
    }

    void Update() {
        for (int i = 0; i < inventoryLength; i++)
        {
            if (gameManager.inventory[i].isFull) {
                inventoryUI.UpdateSlot(i, gameManager.inventory[i].item);
            }
            
        }
    }


    public void GiveItem(int Id) 
    {
        //Item itemToAdd = ItemDatabase.GetItem(Id);
        //inventory.Add(itemToAdd);
        //Debug.Log(itemToAdd.ItemName);
        //inventoryUI.AddNewItem(itemToAdd); 
    }

    public void GiveItem(string ItemName)
    {
        //Item itemToAdd = ItemDatabase.GetItem(ItemName);
        //inventory.Add(itemToAdd);
        //inventoryUI.AddNewItem(itemToAdd);
    }

    /*
    public Item checkForItem(int Id) 
    {
        return inventory.Find(item => item.Id == Id);
    }
    */

    public void RemoveItem(int Id)
    {
        /*
        Item itemToRemove = checkForItem(Id);
        if (itemToRemove != null)
        {
            inventory.Remove(itemToRemove);
            //inventoryUI.RemoveItem(itemToRemove);
        }
        */
    }
}
