using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]private GameObject toolbar;
    [SerializeField]private GameObject inventory;
    [SerializeField]private GameObject inventoryToolbar;
    [SerializeField]private GameObject inventoryRemain;

    //input count
    private int inputICount = 0;

    void Update() {
        InventoryButton();
    }

    
    private void InventoryButton() {
        if (Input.GetKeyDown("i")) {
            inputICount++;
        }
        if ((inputICount % 2).Equals(0)) {
            CloseInventory();
        } else {
            OpenInventory();
        }
    }


    private void OpenInventory() {
        toolbar.SetActive(false);
        inventory.SetActive(true);

        //toolbar에 있는 slot 옮기기
        while (toolbar.transform.childCount > 0) {
            toolbar.transform.GetChild(0).SetParent(inventoryToolbar.transform);
        }
    }

    private void CloseInventory() {
        toolbar.SetActive(true);
        inventory.SetActive(false);

        //toolbar에 있는 slot 옮기기
        while (inventoryToolbar.transform.childCount > 0) {
            inventoryToolbar.transform.GetChild(0).SetParent(toolbar.transform);
        }
    }
}
