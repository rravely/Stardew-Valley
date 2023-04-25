using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryName : MonoBehaviour
{
    private Text playerName;

    //player data
    private GameManager gameManager;

    void Start() {
        playerName = GetComponent<Text>();

        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        playerName.text = "이름: " + gameManager.player.name;
    }
}
