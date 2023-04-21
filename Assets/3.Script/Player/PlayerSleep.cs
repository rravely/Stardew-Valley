using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSleep : MonoBehaviour
{
    //for sleep
    [SerializeField] private DayTimeControl dayControl;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject UIAskSleep;
    [SerializeField] private GameObject Yes;
    [SerializeField] private GameObject No;

    //ask sleep
    private Text yes;
    private Text no;

    string playerAnswer = "";

    //for growing crops
    private FarmManager farmManager;

    private bool askSleepUiActivate = false;
    private GameObject toolbar;
    
    //for playerHP
    private PlayerControl playerControl;

    //fading
    [SerializeField]private GameObject dark;

    void Awake() {
        toolbar = GameObject.FindWithTag("Toolbar");
        yes = Yes.GetComponent<Text>();
        no = No.GetComponent<Text>();

        farmManager = GameObject.FindWithTag("Farm").GetComponent<FarmManager>();
        playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
    }

    void Update() {
        if (askSleepUiActivate) {
            AskSleep();   
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player")) //침대와 플레이어 충돌 -> sleep
        {
            //잘지 물어보기
            toolbar.SetActive(false);
            UIAskSleep.SetActive(true);
            player.SetActive(false);
            askSleepUiActivate = true;
        }
    }

    private void AskSleep() {
        float x = Input.GetAxisRaw("Vertical");
        bool returnKey = Input.GetKey(KeyCode.Return);

        if (x > 0) {
            yes.fontSize = 60;
            no.fontSize = 50;
            playerAnswer = "yes";
        }
        if (x < 0) {
            no.fontSize = 60;
            yes.fontSize = 50;
            playerAnswer = "no";
        }
        if (returnKey) {
            if (playerAnswer.Equals("yes")) {
                WakeUp();
            }
            else if (playerAnswer.Equals("no")) 
            {
                toolbar.SetActive(true);
                UIAskSleep.SetActive(false);
                player.SetActive(true);
                askSleepUiActivate = false;
            }
            player.transform.position = new Vector3(player.transform.position.x - 0.01f, player.transform.position.y, player.transform.position.z);
        }   
    }

    public void WakeUp() {
        //fade in and out
        dark.SetActive(true);

        //플레이어 위치 이동
        GameObject.FindWithTag("Player").transform.position = new Vector3(5.082f, 8.52f, 8.37f);

        //하루 지나기
        dayControl.currentDay++;
        dayControl.currentTime = 0;

        toolbar.SetActive(true);
        UIAskSleep.SetActive(false);
        player.SetActive(true);
        askSleepUiActivate = false;

        //작물 자라기
        farmManager.GrowningCrops();

        //hp 채우기
        playerControl.playerEnergy = 15f;
    }
}
