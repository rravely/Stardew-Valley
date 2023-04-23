using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoney : MonoBehaviour
{
    private Text playerMoney;

    //player data
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerMoney = GetComponent<Text>();

        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMoney.text = gameManager.player.playerMoney.ToString();
    }
}
