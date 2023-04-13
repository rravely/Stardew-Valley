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

        //마우스로 선택하면 아이템 들기
        
        //아이템을 든 상태로 다른 아이템을 누르면 교체, 빈 슬롯을 누르면 그곳에 두기
        

        
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
