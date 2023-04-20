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

    //After player animation 
    public bool isAnimationEnd = false;

    //to compare resources position and player
    FarmMap farmMap;
    FarmManager farmManager;
    [SerializeField]private Tilemap dirtTileMap;

    private SpriteRenderer spriteRenderer; 
    private float playerPosZ; //플레이어와 건물 사이의 z 위치 비교를 위해

    enum PLAYERIDLESTATE{right = 1, left = 2, up = 3, down = 4}
    enum PLAYERWALKSTATE{right = 5, left = 6, up = 7, down = 8}
    enum PLAYERWORKSTATE{right = 9, left = 10, up = 11, down = 12}
    enum PLAYERWATERSTATE{right = 13, left = 14, up = 15, down = 16}
    //enum PLAYERWORKSTATE {axe = 0, hoe = 1, pickaxe = 2, wateringcan = 3}

  
    void Awake()
    {
        movement2D = transform.GetComponent<Movement2D>();
        animator = transform.GetComponent<Animator>();
    }

    void Start()
    {
        ChangeZSameAsY();
        toolAnimator = GameObject.FindWithTag("Tool").GetComponent<Animator>();
        farmMap = GameObject.FindWithTag("Farm").GetComponent<FarmMap>();
        farmManager = GameObject.FindWithTag("Farm").GetComponent<FarmManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDirection();
        ChangeZSameAsY();
        MouseClickForWork();
    }

    void ChangeDirection() {
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

    void ChangeZSameAsY() {
        playerPosZ = transform.position.y - GetComponent<Renderer>().bounds.size.y * 0.5f; //플레이어 발 끝 y값으로 z값 정하기
        transform.position = new Vector3(transform.position.x, transform.position.y, playerPosZ);
    }


    void MouseClickForWork()
    {
        if (Input.GetMouseButtonDown(0) && selectedToolId != -1) //마우스가 눌리고 선택된 아이템이 도구라면
        {
            StartCoroutine("PlayerWorkAnimation_co");
        }
    }

    IEnumerator PlayerWorkAnimation_co() { //플레이어 일하는 애니메이션 재생
        yield return new WaitForSeconds(0.51f);

        if (Input.mousePosition.y > 125) //인벤토리 창보다 위
        {
            if (selectedToolId.Equals(3)) { //물뿌리개 사용
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
        toolAnimator.SetInteger(toolName[selectedToolId], 5);
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
        //int playerX = (int)((transform.position.x - 1.95f) / 0.15f);
        //int playerY = -(int)((transform.position.y - 4.675f) / 0.15f);
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
    void PlayerFarming(int posX, int posY) 
    {
        if (farmMap.farmMap[posY, posX].Equals(0)) 
        {
            if (farmMap.farmResData[posY, posX] > 0 && farmMap.farmResData[posY, posX] < 5) //나뭇가지, 돌, 잡초, 나무
            {
                        
            }
            else if(farmMap.farmResData[posY, posX].Equals(0) && selectedToolId.Equals(1)) //아무것도 없는 땅이고 잡은 도구가 호미라면
            {
                //땅파기
                farmManager.ChangeHoeDirt(transform.position, playerDirection);

                //땅팠다고 저장
                farmMap.farmResData[posY, posX] = 5;
            }
            else if(farmMap.farmResData[posY, posX].Equals(5) && selectedToolId.Equals(3)) //호미질 된 땅에 물뿌리개 사용했다면
            {
                //물 준 땅으로 바꾸기
                //farmManager.ChangeWateringDirt(transform.position, playerDirection);
                //물준 땅으로 변경
                farmMap.farmResData[posY, posX] = 6;
            }
        }
    }

    //씨앗 뿌리기
    void PlayerSeeding(int posX, int posY) {
        if (farmMap.farmResData[posY, posX].Equals(5) || farmMap.farmResData[posY, posX].Equals(6)) //호미질한 땅이거나 물 준 땅이면 씨앗 심기 가능
        {

        }
    }

    //플레이어의 애니메이션 끝에 추가하여 충돌한 물체가 확인할 수 있게 함
    void CheckAnimationEnd() {
        isAnimationEnd = true;
    }
}
