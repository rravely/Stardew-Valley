using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{
    public string name;
    public string farmName;
    
    public InventoryItem[] inventoryItems = new InventoryItem[36];
    
    public bool[] isFullArray = new bool [36];
    public int[] itemsIdArray = new int[36];
    public int[] itemsCountArray = new int[36];
    

    //public SaveItemArray[] saveItemArrays = new SaveItemArray[36];

    public SaveData(string name, string farmName, bool[] isFullArray, int[] itemsIdArray, int[]itemsCountArray)
    {
        this.name = name;
        this.farmName = farmName;
        this.isFullArray = isFullArray;
        this.itemsIdArray = itemsIdArray;
        this.itemsCountArray = itemsCountArray;
    }

    
}
