using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAndFarming : MonoBehaviour
{   
    //플레이어
    private GameObject player;
    private PlayerControl playerControl;
    private Animator animator;
    
    private InventoryManager inventoryManager;
    private Transform droppedItem;
    //[SerializeField]private Item item;
    [SerializeField]private int SpawnItemCount = 1;
    [SerializeField]private GameObject spawnItemPrefab;
    [SerializeField]private List<int> toolId;

    //플레이어 애니메이션
    private Animator playerAnimator;
    private string playerState = "player_state";
    enum PLAYERIDLESTATE{right = 1, left = 2, up = 3, down = 4}

    //플레이어 도구 애니메이션
    private Animator toolAnimator;
    private string[] toolName = new string[5] {"Axe", "Hoe", "Pickaxe", "Wateringcan", "Scythe"};

    //Map Info
    private FarmMap farmMap;
    

    void Start() {
        player = GameObject.FindWithTag("Player");
        playerControl = player.GetComponent<PlayerControl>();
        animator = gameObject.GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();
        inventoryManager = GameObject.FindWithTag("InventoryManager").GetComponent<InventoryManager>();
        droppedItem = GameObject.FindWithTag("DroppedItem").transform;
        toolAnimator = GameObject.FindWithTag("Tool").GetComponent<Animator>();
        farmMap = GameObject.FindWithTag("Farm").GetComponent<FarmMap>();
    }

    void Update() {
    }

    void OnCollisionStay2D(Collision2D collision) {
        //플레이어랑 충돌하고 플레이어가 선택한 도구가 해당 작물과 상호작용
        if (collision.transform.CompareTag("Player") && toolId.Contains(playerControl.selectedToolId)) 
        {
            switch (playerControl.workDirection) //플레이어가 일하는 방향이
            { 
                case 1: //right
                    if (player.transform.position.x < transform.position.x) //플레이어의 위치가 이 오브젝트보다 왼쪽에 있으면
                    {
                        //playerControl.workDirection = 0;
                        animator.SetBool("isRemoved", true);
                        playerAnimator.SetInteger(playerState, (int)PLAYERIDLESTATE.right);
                    }
                    break;
                case 2: //left
                    if (player.transform.position.x > transform.position.x) //플레이어의 위치가 이 오브젝트보다 오른쪽에 있으면
                    {
                        //playerControl.workDirection = 0;
                        animator.SetBool("isRemoved", true);
                        playerAnimator.SetInteger(playerState, (int)PLAYERIDLESTATE.left);
                    }
                    break;
                case 3: //up
                    if (player.transform.position.z < transform.position.z) //플레이어의 위치가 이 오브젝트보다 아래쪽에 있으면
                    {
                        //playerControl.workDirection = 0;
                        animator.SetBool("isRemoved", true);
                        playerAnimator.SetInteger(playerState, (int)PLAYERIDLESTATE.up);
                    }
                    break;
                case 4: //down
                    if (player.transform.position.z > transform.position.z) //플레이어의 위치가 이 오브젝트보다 위쪽에 있으면
                    {
                        //playerControl.workDirection = 0;
                        animator.SetBool("isRemoved", true);
                        playerAnimator.SetInteger(playerState, (int)PLAYERIDLESTATE.down);
                    }
                    break;
            }
        }
    }

    void DestroyObject() {
        animator.SetBool("isRemoved", true); //파괴 애니메이션
        Destroy(gameObject); //이 오브젝트 파괴

        //아이템 생성할 좌표
        float x = transform.position.x + 0.01f * Random.Range(0, 10);
        float y = transform.position.y + 0.01f * Random.Range(0, 10);
        //아이템 생성
        for (int i = 0; i < SpawnItemCount; i++) {
        GameObject spawnItem = Instantiate(spawnItemPrefab, new Vector3(x, y, transform.position.z), Quaternion.identity); //아이템 바닥에 생성
        spawnItem.transform.SetParent(droppedItem); //생성한 아이템이 DroppedItem에 상속되도록
        }
        playerControl.workDirection = 0;
    }

    
}
