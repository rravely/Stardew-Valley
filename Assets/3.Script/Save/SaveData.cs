using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{
    public string name;
    public string farmName;
    
    //public bool[] isFullArray = new bool [36];
    //public int[] itemsIdArray = new int[36];
    //public int[] itemsCountArray = new int[36];
    

    //public SaveItemArray[] saveItemArrays = new SaveItemArray[36];

    public SaveData(string name, string farmName)
    {
        this.name = name;
        this.farmName = farmName;
    }

    
}
