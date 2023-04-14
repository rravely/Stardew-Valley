using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    [SerializeField]private InventoryManager inventoryManager;
    public Item[] itemToPickup;

    public void PickupItem(int id) 
    {
        bool result = inventoryManager.AddItem(itemToPickup[id]);
        if (result) 
        {
            Debug.Log("Add Item");
        }
        else {
            Debug.Log("Add Item fail");
        }
    } 
}
