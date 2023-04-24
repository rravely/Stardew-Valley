using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField]private GameObject inventory;
    [SerializeField]private GameObject craft;
    [SerializeField]private GameObject exit;

    public void InventoryButtonClick() {
        inventory.SetActive(true);
        craft.SetActive(false);
        exit.SetActive(false);
    }
    
    public void CraftButtonClick() {
        inventory.SetActive(false);
        craft.SetActive(true);
        exit.SetActive(false);
    }

    public void ExitButtonClick() {
        inventory.SetActive(false);
        craft.SetActive(false);
        exit.SetActive(true);
    }

    
}
