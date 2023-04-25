using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMoney : MonoBehaviour
{
    private Text playerMoney;

    //player data
    private GameManager gameManager;

    void Start() {
        playerMoney = GetComponent<Text>();

        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        playerMoney.text = "소지금: " + gameManager.player.playerMoney;
    }

    void Update() {
        playerMoney.text = "소지금: " + gameManager.player.playerMoney;
    }
}
