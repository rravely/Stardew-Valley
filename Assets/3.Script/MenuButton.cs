using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    //menu type
    [SerializeField]private GameObject inventory;
    [SerializeField]private GameObject craft;
    [SerializeField]private GameObject exit;

    //inventory move
    [SerializeField]private GameObject inventoryToolbar;
    [SerializeField]private GameObject inventoryRemain;
    [SerializeField]private GameObject craftInventory;

    public void InventoryButtonClick() {
        inventory.SetActive(true);
        craft.SetActive(false);
        exit.SetActive(false);

        //toolbar에 있는 slot 옮기기
        for (int i = 0; i < 12; i++) {
            craftInventory.transform.GetChild(0).SetParent(inventoryToolbar.transform);
        }
        //남은 inventory에 있는 slot 옮기기
        while (craftInventory.transform.childCount > 0) {
            craftInventory.transform.GetChild(0).SetParent(inventoryRemain.transform);
        }
    }
    
    public void CraftButtonClick() {
        inventory.SetActive(false);
        craft.SetActive(true);
        exit.SetActive(false);

        //인벤토리에 있는 아이템들 여기 인벤토리로 옮기기
        //toolbar에 있는 slot 옮기기
        while (inventoryToolbar.transform.childCount > 0) {
            inventoryToolbar.transform.GetChild(0).SetParent(craftInventory.transform);
        }
        //남은 inventory에 있는 slot 옮기기
        while (inventoryRemain.transform.childCount > 0) {
            inventoryRemain.transform.GetChild(0).SetParent(craftInventory.transform);
        }
    }

    public void ExitButtonClick() {
        inventory.SetActive(false);
        craft.SetActive(false);
        exit.SetActive(true);
    }

    
}
