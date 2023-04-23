using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryFarmName : MonoBehaviour
{
    private Text farmName;

    //player data
    private GameManager gameManager;

    void Start() {
        farmName = GetComponent<Text>();

        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        farmName.text = "농장 이름: " + gameManager.player.farmName;
    }
}
