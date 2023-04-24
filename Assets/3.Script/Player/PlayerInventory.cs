using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]private GameObject toolbar;
    [SerializeField]private GameObject inventory;
    [SerializeField]private GameObject inventoryToolbar;
    [SerializeField]private GameObject inventoryRemain;
    [SerializeField]private GameObject craftMenu;
    [SerializeField]private GameObject exitMenu;
    [SerializeField]private GameObject menuButton;

    //mouse control
    private GameManager gameManager;

    //input count
    private int inputICount = 0;

    void Start() {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    void Update() {
        InventoryButton();
    }

    
    private void InventoryButton() {
        if (Input.GetKeyDown("i") ) {
            inputICount++;
        }
        if ((inputICount % 2).Equals(1) && gameManager.menuLock.Equals(false)) {
            OpenInventory();
            gameManager.playerMouseButtonActive = false;
            gameManager.menuLock = true;
        } else if ((inputICount % 2).Equals(0) && gameManager.menuLock.Equals(true)){
            CloseInventory();
            gameManager.playerMouseButtonActive = true;
            gameManager.menuLock = false;
        }
    }


    private void OpenInventory() {
        toolbar.SetActive(false);
        inventory.SetActive(true);
        menuButton.SetActive(true);
        
        //toolbar에 있는 slot 옮기기
        while (toolbar.transform.childCount > 0) {
            toolbar.transform.GetChild(0).SetParent(inventoryToolbar.transform);
        }
        
        /*
        for (int i = 0; i < 12; i++) {
            if (toolbar.transform.GetChild(i).childCount > 0) { //툴바의 자식 객체(슬롯)에게 자식(아이템)이 있다면
                toolbar.transform.GetChild(i).GetChild(0).SetParent(inventoryToolbar.transform.GetChild(i));
            }
        }
        */
    }

    private void CloseInventory() {
        toolbar.SetActive(true);
        inventory.SetActive(false);
        craftMenu.SetActive(false);
        exitMenu.SetActive(false);
        menuButton.SetActive(false);

        //toolbar에 있는 slot 옮기기
        while (inventoryToolbar.transform.childCount > 0) {
            inventoryToolbar.transform.GetChild(0).SetParent(toolbar.transform);
        }
        
    }
}
