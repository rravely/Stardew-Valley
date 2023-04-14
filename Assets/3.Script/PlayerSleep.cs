using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSleep : MonoBehaviour
{
    [SerializeField] private DayTimeControl dayControl;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player")) //침대와 플레이어 충돌 -> sleep
        {
            //화면 어두워지고

            
            //날짜 하루 +1
            dayControl.currentDay++;

            //HP와 Energy 충전

        }
    }
}
