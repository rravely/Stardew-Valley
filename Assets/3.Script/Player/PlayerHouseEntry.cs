using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHouseEntry : MonoBehaviour
{
    [SerializeField]GameObject player;
    [SerializeField]private float Posx = 4.41f, PosY = 8.77f; 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            player.transform.position = new Vector2(Posx, PosY);

        }
    }
}
