using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField]private GameManager gameManager;
    [SerializeField]private GameObject player;
    [SerializeField]private GameObject shop;
    [SerializeField]private GameObject UI;
    [SerializeField]private Text day, time, money;
    [SerializeField]private Text playerMoney;

    //check player in shop
    [HideInInspector]public bool isPlayerInShop;

    //data of selling item
    [HideInInspector]public int itemCount;
    [HideInInspector]public int itemCost;

    private PlayerControl playerControl;
    //private Text playerMoney;

    void Start() {
        isPlayerInShop = false;
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        playerMoney.text = gameManager.player.playerMoney.ToString();
    }

    void Update() {
        if (player.transform.position.x > 12f && player.transform.position.y > 9f) { //플레이어가 shop에 들어오면
            isPlayerInShop = true;
            playerMoney.text = gameManager.player.playerMoney.ToString();
            shop.gameObject.SetActive(true);
            UI.SetActive(false);
            player.SetActive(false);
            day.gameObject.SetActive(false);
            time.gameObject.SetActive(false);
            money.gameObject.SetActive(false); 

            //여기서 player.SetActive(false); 를 하면 slotitem이 player를 찾지 못한다. 그래서 slotitem에서 player 찾지 않아도 되게 함
            player.transform.position = new Vector3(13f, 9.08f);
        }
        if (shop.activeSelf) //shop인 동안
        {
            EscapeShop();
        }
    }

    void EscapeShop() {
        if (Input.GetKey(KeyCode.Escape)) 
        {
            isPlayerInShop = false;
            shop.gameObject.SetActive(false);
            UI.SetActive(true);
            player.SetActive(true);
            day.gameObject.SetActive(true);
            time.gameObject.SetActive(true);
            money.gameObject.SetActive(true);
            player.transform.position = new Vector2(13.68f, 3.82f); //잡화점 문앞 위치 
        }
    }
    
}
