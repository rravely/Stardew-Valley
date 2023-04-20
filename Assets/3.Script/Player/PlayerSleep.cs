using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSleep : MonoBehaviour
{
    [SerializeField] private DayTimeControl dayControl;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject UIAskSleep;
    [SerializeField] private GameObject Yes;
    [SerializeField] private GameObject No;

    private Text yes;
    private Text no;

    string playerAnswer = "";

    private bool askSleepUiActivate = false;
    private GameObject toolbar;
    

    void Awake() {
        toolbar = GameObject.FindWithTag("Toolbar");
        yes = Yes.GetComponent<Text>();
        no = No.GetComponent<Text>();

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
                //하루 지나기
                dayControl.currentDay++;
                dayControl.currentTime = 0;

                toolbar.SetActive(true);
                UIAskSleep.SetActive(false);
                player.SetActive(true);
                askSleepUiActivate = false;
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
}
