using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataControl : MonoBehaviour
{
    [SerializeField]private GameManager gameManager;
    private SaveData playerData;
    //private string playerName = "모카";
    //private string farmName = "라떼";
    //private List<Item> inventory = new List<Item>();
    //private InventoryItem[] currentInventory = new InventoryItem[36];

    public void SavePlayerData() {
        playerData = gameManager.player;
        SaveSystem.Save(playerData, "Default");
    }
}
