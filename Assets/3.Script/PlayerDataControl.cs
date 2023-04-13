using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataControl : MonoBehaviour
{
    private SaveData playerData;
    private string playerName = "모카";
    private string farmName = "라떼";
    //private List<Item> inventory = new List<Item>();
    private List<Item> currentInventory = new List<Item>();

    public void SavePlayerData() {
        currentInventory = GetComponent<PlayerInventory>().inventory;
        playerData = new SaveData(playerName, farmName, currentInventory);
        SaveSystem.Save(playerData, "Default");
        Debug.Log(currentInventory.Count);
        for (int i = 0; i < currentInventory.Count; i++) {
            Debug.Log(currentInventory[i].ItemName);
        }
    }
}
