using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{
    public string name;
    public string farmName;
    public List<Item> saveInventory = new List<Item>();
    public int[] itemsIdArray;
    public int[] itemsCountArray;

    public SaveData(string name, string farmName, List<Item> saveInventory)
    {
        this.name = name;
        this.farmName = farmName;
        this.saveInventory = saveInventory;
        itemsIdArray = new int[saveInventory.Count];
        itemsCountArray = new int[saveInventory.Count];
        for (int i = 0; i < saveInventory.Count; i++) {
            itemsIdArray[i] = saveInventory[i].Id;
            itemsCountArray[i] = saveInventory[i].ItemCount;
        }
    }

    
}
