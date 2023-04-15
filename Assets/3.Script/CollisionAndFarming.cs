using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAndFarming : MonoBehaviour
{   
    //플레이어
    private GameObject player;
    private PlayerControl playerControl;
    [SerializeField]private Item item;
    private InventoryManager inventoryManager;


    void Start() {
        player = GameObject.FindWithTag("Player");
        playerControl = player.GetComponent<PlayerControl>();
        inventoryManager = GameObject.FindWithTag("InventoryManager").GetComponent<InventoryManager>();
    }

    void Update() {
    }

    void OnCollisionStay2D(Collision2D collision) {
        if (collision.transform.CompareTag("Player")) {
            switch (playerControl.workDirection) //플레이어가 일하는 방향이
            { 
                case 1: //right
                    if (player.transform.position.x < transform.position.x) //플레이어의 위치가 이 오브젝트보다 왼쪽에 있으면
                    {
                        inventoryManager.AddItem(item);
                        playerControl.workDirection = 0;
                    }
                    break;
                case 2: //left
                    if (player.transform.position.x > transform.position.x) //플레이어의 위치가 이 오브젝트보다 왼쪽에 있으면
                    {
                        inventoryManager.AddItem(item);
                        playerControl.workDirection = 0;
                    }
                    break;
                case 3: //up
                    if (player.transform.position.y < transform.position.y) //플레이어의 위치가 이 오브젝트보다 왼쪽에 있으면
                    {
                        inventoryManager.AddItem(item);
                        playerControl.workDirection = 0;
                    }
                    break;
                case 4: //down
                    if (player.transform.position.y > transform.position.y) //플레이어의 위치가 이 오브젝트보다 왼쪽에 있으면
                    {
                        inventoryManager.AddItem(item);
                        playerControl.workDirection = 0;
                    }
                    break;
            }
        }
    }

    
}
