using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerControl : MonoBehaviour
{
    private Movement2D movement2D; //플레이어 이동
    private Animator animator;
    private string playerState = "player_state";
    [HideInInspector]public int playerDirection = 0; //플레이어가 움직이는 방향
    [HideInInspector]public int workDirection = 0; //플레이어 일하는 방향
    [HideInInspector]public int selectedToolId = 0; //플레이어가 선택한 도구 (-1은 아직 선택 x)

    //tool control
    private Animator toolAnimator;
    private string[] toolName = new string[5] {"Axe", "Hoe", "Pickaxe", "Wateringcan", "Scythe"};

    //player sleep
    [HideInInspector]public float playerEnergy;
    [SerializeField]private GameObject playerSleepArea;

    //mouse control
    private GameManager gameManager;

    //After player animation 
    [HideInInspector]public bool isAnimationEnd = false;
    private bool isLock = false;

    //to compare resources position and player
    FarmMap farmMap;
    FarmManager farmManager;
    [SerializeField]private Tilemap dirtTileMap;

    //for selected slot item update
    private InventoryManager inventoryManager;

    //parsnip add
    [SerializeField]private Item parsnipItem;
    [SerializeField]private Item beanItem;

    private SpriteRenderer spriteRenderer; 
    private float playerPosZ; //플레이어와 건물 사이의 z 위치 비교를 위해

    enum PLAYERIDLESTATE{right = 1, left = 2, up = 3, down = 4}
    enum PLAYERWALKSTATE{right = 5, left = 6, up = 7, down = 8}
    enum PLAYERWORKSTATE{right = 9, left = 10, up = 11, down = 12}
    enum PLAYERWATERSTATE{right = 13, left = 14, up = 15, down = 16}
  
    void Awake()
    {
        movement2D = transform.GetComponent<Movement2D>();
        animator = transform.GetComponent<Animator>();
        isLock = false;
    }

    void Start()
    {
        ChangeZSameAsY();

        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

        toolAnimator = GameObject.FindWithTag("Tool").GetComponent<Animator>();
        farmMap = GameObject.FindWithTag("Farm").GetComponent<FarmMap>();
        farmManager = GameObject.FindWithTag("Farm").GetComponent<FarmManager>();

        inventoryManager = GameObject.FindWithTag("InventoryManager").GetComponent<InventoryManager>();

        playerEnergy = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDirection();
        MouseClickForWork();
        ChangeZSameAsY();

        if (playerEnergy.Equals(0)) {
            playerSleepArea.GetComponent<PlayerSleep>().WakeUp();
        }
    }

    void ChangeDirection() {
        
        if (isLock == false) {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            if (x > 0) //right
            {
                //애니메이션 끝나기 전에 움직이면 자원 충돌 조건 성립 안되게
                isAnimationEnd = false; 
                animator.SetInteger(playerState, (int)PLAYERWALKSTATE.right);
                playerDirection = 1;
                movement2D.MoveTo(new Vector3(x, 0, 0f));
            
            }
            else if (x < 0) // left
            {
                isAnimationEnd = false; 
                animator.SetInteger(playerState, (int)PLAYERWALKSTATE.left);
                playerDirection = 2;
                movement2D.MoveTo(new Vector3(x, 0, 0f));
            }
            else if (y > 0) //up
            {
                isAnimationEnd = false; 
                animator.SetInteger(playerState, (int)PLAYERWALKSTATE.up);
                playerDirection = 3;
                movement2D.MoveTo(new Vector3(0, y, y));
            }
            else if (y < 0) //down
            {
                isAnimationEnd = false; 
                animator.SetInteger(playerState, (int)PLAYERWALKSTATE.down);
                playerDirection = 4;
                movement2D.MoveTo(new Vector3(0, y, y));
          }
           else if (x.Equals(0) && y.Equals(0))
            {
                switch (playerDirection)
                {
                    case 1:
                        animator.SetInteger(playerState, (int)PLAYERIDLESTATE.right);
                        movement2D.MoveTo(new Vector3(0, 0, 0f));
                        break;
                    case 2:
                        animator.SetInteger(playerState, (int)PLAYERIDLESTATE.left);
                        movement2D.MoveTo(new Vector3(0, 0, 0f));
                        break;
                    case 3:
                        animator.SetInteger(playerState, (int)PLAYERIDLESTATE.up);
                        movement2D.MoveTo(new Vector3(0, 0, 0f));
                        break;
                    case 4:
                        animator.SetInteger(playerState, (int)PLAYERIDLESTATE.down);
                        movement2D.MoveTo(new Vector3(0, 0, 0f));
                        break;
                }
            }
        }
        
    }

    void ChangeZSameAsY() {
        playerPosZ = transform.position.y - GetComponent<Renderer>().bounds.size.y * 0.5f; //플레이어 발 끝 y값으로 z값 정하기
        transform.position = new Vector3(transform.position.x, transform.position.y, playerPosZ);
    }


    void MouseClickForWork()
    {
        if (Input.GetMouseButtonDown(0) && selectedToolId >= 0 && selectedToolId < 5 && gameManager.playerMouseButtonActive) //마우스가 눌리고 선택된 아이템이 도구라면
        {
            playerEnergy--;
            StartCoroutine("PlayerWorkAnimation_co");
            //isLock = true;
        }
        else if (Input.GetMouseButtonDown(0) && selectedToolId >= 5 && gameManager.playerMouseButtonActive) //마우스가 눌렸는데 선택된 아이템이 도구가 아니라면
        {
            playerEnergy--;
            CheckNearResources(); //주변 자원 체크
        }
    }

    IEnumerator PlayerWorkAnimation_co() { //플레이어 일하는 애니메이션 재생
        yield return new WaitForSeconds(0.51f);
        if (Input.mousePosition.y > 125) //인벤토리 창보다 위
        {
            if (selectedToolId.Equals(3)) { //물뿌리개 사용
                //isLock = true;
                switch (playerDirection) {
                    case 1:
                        animator.SetInteger(playerState, (int)PLAYERWATERSTATE.right);
                        toolAnimator.SetInteger(toolName[selectedToolId], (int)PLAYERWORKSTATE.right);
                        break;
                    case 2:
                        animator.SetInteger(playerState, (int)PLAYERWATERSTATE.left);
                        toolAnimator.SetInteger(toolName[selectedToolId], (int)PLAYERWORKSTATE.left);
                        break;
                    case 3:
                        animator.SetInteger(playerState, (int)PLAYERWATERSTATE.up);
                        toolAnimator.SetInteger(toolName[selectedToolId], (int)PLAYERWORKSTATE.up);
                        break;
                    case 4:
                        animator.SetInteger(playerState, (int)PLAYERWATERSTATE.down);
                        toolAnimator.SetInteger(toolName[selectedToolId], (int)PLAYERWORKSTATE.down);
                        break;
                }
            }
            else { // 그 외 도구 사용
                switch (playerDirection) { 
                    case 1:
                        animator.SetInteger(playerState, (int)PLAYERWORKSTATE.right);
                        toolAnimator.SetInteger(toolName[selectedToolId], (int)PLAYERWORKSTATE.right);
                        break;
                    case 2:
                        animator.SetInteger(playerState, (int)PLAYERWORKSTATE.left);
                        toolAnimator.SetInteger(toolName[selectedToolId], (int)PLAYERWORKSTATE.left);
                        break;
                    case 3:
                        animator.SetInteger(playerState, (int)PLAYERWORKSTATE.up);
                        toolAnimator.SetInteger(toolName[selectedToolId], (int)PLAYERWORKSTATE.up);
                        break;
                    case 4:
                        animator.SetInteger(playerState, (int)PLAYERWORKSTATE.down);
                        toolAnimator.SetInteger(toolName[selectedToolId], (int)PLAYERWORKSTATE.down);
                        break;
                }
            }
        }
    } 


    void PlayerIDLE() { //애니메이션 끝나면 그 방향 바라보고 있도록
        toolAnimator.SetInteger(toolName[selectedToolId], 5); //selectedToolId가 4이상이면 null....
        switch (workDirection) 
        {
            case 1: 
                animator.SetInteger(playerState, (int)PLAYERIDLESTATE.right);
                break;
            case 2:
                animator.SetInteger(playerState, (int)PLAYERIDLESTATE.left);
                break;
            case 3:
                animator.SetInteger(playerState, (int)PLAYERIDLESTATE.up);
                break;
            case 4:
                animator.SetInteger(playerState, (int)PLAYERIDLESTATE.down);
                break;
        }
    }

    void CheckNearResources() //플레이어가 마우스를 선택했을 때 주변에 자원이 있는지 체크
    {
        //현재 플레이어의 좌표 구하기
        float playerPosX = transform.position.x;
        float playerPosY = transform.position.y - 0.1f;
        Vector3 playerPos = new Vector3(playerPosX, playerPosY, playerPosY);

        //플레이어의 셀 좌표
        Vector3Int playerPosInt = dirtTileMap.LocalToCell(playerPos); //Vector3Int로 변환
        Debug.Log("현재 플레이어 셀 좌표: " + playerPosInt.x + ", " + playerPosInt.y);
        int playerX = playerPosInt.x - 12;
        int playerY = Mathf.Abs(playerPosInt.y - 31);
        Debug.Log("현재 플레이어 맵 좌표: " + playerY+ ", " + playerX);

        if (playerX >= 0 && playerX < 43 && playerY >= 0 && playerY < 26) {
            switch (playerDirection)
            {
                case 1:
                    PlayerFarming(playerX + 1, playerY);
                    break;
                case 2:
                    PlayerFarming(playerX - 1, playerY);
                    break;
                case 3:
                    PlayerFarming(playerX, playerY - 1);
                    break;
                case 4:
                    PlayerFarming(playerX, playerY + 1);
                    break;
            }
        }
    }

    //파밍 조건
    void PlayerFarming(int posX, int posY) //매개변수는 맵 좌표
    {
        Debug.Log(selectedToolId);
        if (farmMap.farmMap[posY, posX].Equals(0)) 
        {
            if (farmMap.farmResData[posY, posX] > 0 && farmMap.farmResData[posY, posX] < 5) //나뭇가지, 돌, 잡초, 나무
            {
                        
            } else if(farmMap.farmResData[posY, posX].Equals(0) && selectedToolId.Equals(1)) //아무것도 없는 땅이고 잡은 도구가 호미라면
            {
                //땅파기
                farmManager.ChangeHoeDirt(transform.position, playerDirection);

                //땅팠다고 저장
                farmMap.farmResData[posY, posX] = 5;
                //farmManager.RefreshHoeDirt(posY, posX);
            } else if (farmMap.parsnipGrowing[posY, posX].Equals(5)) { //파스닙 열리면
                Debug.Log("수확하기");
                //땅 정보 초기화
                farmMap.parsnipGrowing[posY, posX] = 0; //씨 정보 사라짐
                
                //tool은 empty
                toolAnimator.SetInteger(toolName[selectedToolId], 5);

                //파스닙 얻기
                inventoryManager.AddItem(parsnipItem);

                //파스닙 얻은 자리 seedTileMap 리셋
                farmManager.ResetDirt(transform.position, playerDirection);
            } else if (farmMap.beanGrowing[posY, posX].Equals(11) || farmMap.beanGrowing[posY, posX].Equals(14)) {//완두콩 열리면
                Debug.Log("수확하기");
                //땅 정보 초기화
                farmMap.beanGrowing[posY, posX] = 12; 
                
                //tool은 empty
                toolAnimator.SetInteger(toolName[selectedToolId], 5);

                //완두콩
                inventoryManager.AddItem(beanItem);

                //완두콩 얻은 자리 seedTileMap 바꾸기
                farmManager.ResetBean(transform.position, playerDirection);
            } else if(farmMap.farmResData[posY, posX].Equals(5) && selectedToolId.Equals(3)) {//호미질 된 땅에 물뿌리개 사용했다면
                //물 준 땅으로 바꾸기
                farmManager.ChangeWateringDirt(transform.position, playerDirection);
                //물준 땅으로 변경
                farmMap.farmResData[posY, posX] = 6;
            } else if (selectedToolId.Equals(12) && farmMap.beanGrowing[posY, posX].Equals(0) && (farmMap.farmResData[posY, posX].Equals(5) || farmMap.farmResData[posY, posX].Equals(6))) { //파스닙 씨앗 뿌리기
                //씨앗 땅으로 바꾸기
                farmManager.PlayerSeeding(transform.position, playerDirection);

                //FarmMap의 parsnipGrowing 변경
                farmMap.parsnipGrowing[posY, posX] = 1;

                //씨앗 개수 줄이기
                //InventoryManager로부터 선택된 슬롯을 가져오고 그 슬롯의 자식 객체인 아이템의 count 변수 가져와서 줄이기! 
                SlotItem slotItem = inventoryManager.inventorySlots[inventoryManager.selectedSlot].GetComponentInChildren<SlotItem>();
                slotItem.count -= 1;
                slotItem.RefreshCount();
            } else if (selectedToolId.Equals(14) && farmMap.parsnipGrowing[posY, posX].Equals(0) && (farmMap.farmResData[posY, posX].Equals(5) || farmMap.farmResData[posY, posX].Equals(6))) { //완두콩 작물 심기
                //작물 땅으로 바꾸기
                farmManager.PlayerBeanFarm(transform.position, playerDirection);

                //FarmMap의 parsnipGrowing 변경
                farmMap.beanGrowing[posY, posX] = 1;

                //작물 아이템 개수 줄이기
                //InventoryManager로부터 선택된 슬롯을 가져오고 그 슬롯의 자식 객체인 아이템의 count 변수 가져와서 줄이기! 
                SlotItem slotItem = inventoryManager.inventorySlots[inventoryManager.selectedSlot].GetComponentInChildren<SlotItem>();
                slotItem.count -= 1;
                slotItem.RefreshCount();
            } else if (selectedToolId.Equals(11) && (farmMap.farmResData[posY, posX].Equals(5) || farmMap.farmResData[posY, posX].Equals(6))) { //잔디 스타터
                //잔디 생성

                //farmMap의 farmResData변경
                farmMap.farmResData[posY, posX] = 10; //10은 잔디

                //잔디 스타터 개수 줄이기
                SlotItem slotItem = inventoryManager.inventorySlots[inventoryManager.selectedSlot].GetComponentInChildren<SlotItem>();
                slotItem.count -= 1;
                slotItem.RefreshCount();
            }
            
        }
    }

    //플레이어의 애니메이션 끝에 추가하여 충돌한 물체가 확인할 수 있게 함
    void CheckAnimationEnd() {
        toolAnimator.SetInteger(toolName[selectedToolId], 0);
        isAnimationEnd = true;
        isLock = false;
    }

    //플레이어의 에너지가 0이 되면
    void PlayerEnergy() {
        if (playerEnergy < 0) {
            //집에서 일어나고 다음날 되기?
        }
    }
}
