using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    //Map Info
    private FarmMap farmMap;
    private Tilemap dirtTileMap;
    

    void Start() {
        player = GameObject.FindWithTag("Player");
        playerControl = player.GetComponent<PlayerControl>();
        animator = gameObject.GetComponent<Animator>();
        inventoryManager = GameObject.FindWithTag("InventoryManager").GetComponent<InventoryManager>();
        droppedItem = GameObject.FindWithTag("DroppedItem").transform;
        farmMap = GameObject.FindWithTag("Farm").GetComponent<FarmMap>();
        dirtTileMap = GameObject.FindWithTag("Dirt").GetComponent<Tilemap>();
    }

    void OnCollisionStay2D(Collision2D collision) {
        //플레이어랑 충돌하고 플레이어가 선택한 도구가 해당 작물과 상호작용
        if (collision.transform.CompareTag("Player") && toolId.Contains(playerControl.selectedToolId) && playerControl.isAnimationEnd == true) 
        {
            switch (playerControl.playerDirection) //플레이어가 일하는 방향이
            { 
                case 1: //right
                    if (player.transform.position.x < transform.position.x) //플레이어의 위치가 이 오브젝트보다 왼쪽에 있으면
                    {
                        animator.SetBool("isRemoved", true);
                    }
                    break;
                case 2: //left
                    if (player.transform.position.x > transform.position.x) //플레이어의 위치가 이 오브젝트보다 오른쪽에 있으면
                    {
                        animator.SetBool("isRemoved", true);
                    }
                    break;
                case 3: //up
                    if (player.transform.position.z < transform.position.z) //플레이어의 위치가 이 오브젝트보다 아래쪽에 있으면
                    {
                        animator.SetBool("isRemoved", true);
                    }
                    break;
                case 4: //down
                    if (player.transform.position.z > transform.position.z) //플레이어의 위치가 이 오브젝트보다 위쪽에 있으면
                    {
                        animator.SetBool("isRemoved", true);
                    }
                    break;
            }
            playerControl.isAnimationEnd = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
    }
    
    void OnTriggerExit2D(Collider2D collider) {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
    }

    void DestroyObject() {
        //이 오브젝트가 있던 자리 맵 정보에서 0으로 바꾸기
        Vector3Int objCellPos = dirtTileMap.LocalToCell(transform.position); //일단 셀 좌표로 바꾸기
        farmMap.farmResData[Mathf.Abs(objCellPos.y - 31), objCellPos.x - 12] = 0; //맵 좌표에 맞게 보정

        Destroy(gameObject); //이 오브젝트 파괴
        
        //아이템 생성
        for (int i = 0; i < SpawnItemCount; i++) {
            //아이템 생성할 좌표
            float x = transform.position.x + 0.01f * Random.Range(0, 20);
            float y = transform.position.y + 0.01f * Random.Range(0, 20);
            GameObject spawnItem = Instantiate(spawnItemPrefab, new Vector3(x, y, y - 0.1f), Quaternion.identity); //아이템 바닥에 생성
            spawnItem.transform.SetParent(droppedItem); //생성한 아이템이 DroppedItem에 상속되도록
        }
        playerControl.workDirection = 0;
    }

    
}
