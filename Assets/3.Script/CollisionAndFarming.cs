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
    private Animator animator;


    void Start() {
        player = GameObject.FindWithTag("Player");
        playerControl = player.GetComponent<PlayerControl>();
        animator = gameObject.GetComponent<Animator>();
        inventoryManager = GameObject.FindWithTag("InventoryManager").GetComponent<InventoryManager>();
    }

    void Update() {
    }

    void OnCollisionStay2D(Collision2D collision) {
        Debug.Log(playerControl.workDirection);
        Debug.Log(player.transform.position.z + " " + transform.position.z);
        if (collision.transform.CompareTag("Player")) {
            switch (playerControl.workDirection) //플레이어가 일하는 방향이
            { 
                case 1: //right
                    if (player.transform.position.x < transform.position.x) //플레이어의 위치가 이 오브젝트보다 왼쪽에 있으면
                    {
                        inventoryManager.AddItem(item);
                        playerControl.workDirection = 0;
                        animator.SetBool("isRemoved", true);
                    }
                    break;
                case 2: //left
                    if (player.transform.position.x > transform.position.x) //플레이어의 위치가 이 오브젝트보다 오른쪽에 있으면
                    {
                        inventoryManager.AddItem(item);
                        playerControl.workDirection = 0;
                        animator.SetBool("isRemoved", true);
                    }
                    break;
                case 3: //up
                    if (player.transform.position.z < transform.position.z) //플레이어의 위치가 이 오브젝트보다 아래쪽에 있으면
                    {
                        Debug.Log("collapse");
                        inventoryManager.AddItem(item);
                        playerControl.workDirection = 0;
                        animator.SetBool("isRemoved", true);
                    }
                    break;
                case 4: //down
                    if (player.transform.position.z > transform.position.z) //플레이어의 위치가 이 오브젝트보다 위쪽에 있으면
                    {
                        inventoryManager.AddItem(item);
                        playerControl.workDirection = 0;
                        animator.SetBool("isRemoved", true);
                    }
                    break;
            }
        }
    }

    void DestroyObject() {
        Destroy(gameObject);
    }

    
}
