using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHouse : MonoBehaviour
{
    [SerializeField]GameObject player;
    private float housePosx = 4.41f, housePosY = 8.77f; 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //집으로 이동
            Debug.Log("player enter the house");
            player.transform.position = new Vector2(housePosx, housePosY);

        }
    }
}
