using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Item[, ] inventory = new Item[3, 12];

    public ItemDatabase itemDatabase;
    public bool activeInventory2Line = false, activeInventory3Line = false;

    void Start() {
        SetDefault();
    }

    void Update() {
        for (int i = 0; i < 5; i++) {
            
        }
    }


    void SetDefault() {
        inventory[0, 0] = itemDatabase.GetItem(0);
        inventory[0, 1] = itemDatabase.GetItem(1);
        inventory[0, 2] = itemDatabase.GetItem(2);
        inventory[0, 3] = itemDatabase.GetItem(3);
        inventory[0, 4] = itemDatabase.GetItem(4);
    }
}
