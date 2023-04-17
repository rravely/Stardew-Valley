using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayTimeControl : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Text date;
    [SerializeField] private Text time;
    [HideInInspector] public int currentTime = 0;
    [HideInInspector] public int currentDay = 1;
    private string[] day = new string[7]; //요일
    private Transform timebar;

    void Start() {
        //시계침 처음 상태
        timebar = transform.GetChild(0);
        timebar.transform.Rotate(0f, 0f, -180f);
    }

    void Update() {
        currentTime++;
        CalculateDayTime(currentTime / 10000, currentDay);
        RotateClockHand();
    }

    void CalculateDayTime(int currentTime, int currentDay) {
        int hour = currentTime / 6 + 6; // 하루는 아침 6시부터 시작
        if (hour.Equals(24)) //하루 끝나면
        {
            hour -= 24;
            currentDay++;
        }
        if ((hour / 12).Equals(0)) //오전이면
        {
            time.text = hour + " : " + ((currentTime % 6) * 10 == 0? "00" : currentTime % 6 * 10) + " 오전";
        }
        else 
        {
            time.text = hour + " : " + ((currentTime % 6) * 10 == 0 ? "00" : currentTime % 6 * 10) + " 오후";
        }

        switch (currentDay / 7) {
            case 0:
                date.text = "일, " + currentDay;
                break;
            case 1:
                date.text = "월, " + currentDay;
                break;
            case 2:
                date.text = "화, " + currentDay;
                break;
            case 3:
                date.text = "수, " + currentDay;
                break;
            case 4:
                date.text = "목, " + currentDay;
                break;
            case 5:
                date.text = "금, " + currentDay;
                break;
            default:
                date.text = "토, " + currentDay;
                break;
        }
    }

    void RotateClockHand() {
        timebar.transform.Rotate(0f, 0f, - 180f / (108f * 10000));
    }

}
