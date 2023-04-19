using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Movement2D movement2D; //플레이어 이동
    private Animator animator;
    private string playerState = "player_state";
    [HideInInspector]public int playerDirection = 0; //플레이어가 움직이는 방향
    [HideInInspector]public int workDirection = 0; //플레이어 일하는 방향
    [HideInInspector]public int selectedToolId = -1; //플레이어가 선택한 도구 (-1은 아직 선택 x)

    //tool control
    private Animator toolAnimator;
    private string[] toolName = new string[5] {"Axe", "Hoe", "Pickaxe", "Wateringcan", "Scythe"};

    private SpriteRenderer spriteRenderer; 
    private float playerPosZ; //플레이어와 건물 사이의 z 위치 비교를 위해
    private float deltaX, deltaY, slope;
    public Camera cam;
    

    enum PLAYERIDLESTATE{right = 1, left = 2, up = 3, down = 4}
    enum PLAYERWALKSTATE{right = 5, left = 6, up = 7, down = 8}
    enum PLAYERWORKSTATE{right = 9, left = 10, up = 11, down = 12}
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
            animator.SetInteger(playerState, (int)PLAYERWALKSTATE.right);
            playerDirection = 1;
            movement2D.MoveTo(new Vector3(x, 0, 0f));
            
        }
        else if (x < 0) // left
        {
            animator.SetInteger(playerState, (int)PLAYERWALKSTATE.left);
            playerDirection = 2;
            movement2D.MoveTo(new Vector3(x, 0, 0f));
        }
        else if (y > 0) //up
        {
            animator.SetInteger(playerState, (int)PLAYERWALKSTATE.up);
            playerDirection = 3;
            movement2D.MoveTo(new Vector3(0, y, y));
        }
        else if (y < 0) //down
        {
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

    IEnumerator PlayerWorkAnimation_co() {
        yield return new WaitForSeconds(0.51f);
        //방향 구하기
        deltaX = cam.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        deltaY = cam.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        slope = deltaY / deltaX;

        if (Input.mousePosition.y > 114) //인벤토리 창보다 위
        {
                if ((deltaX > 0 && slope > 0 && slope < 1) || (deltaX > 0 && slope < 0 && slope > -1)) //right
            {
                workDirection = 1;
                animator.SetInteger(playerState, (int)PLAYERWORKSTATE.right);
                toolAnimator.SetInteger(toolName[selectedToolId], (int)PLAYERWORKSTATE.right);

            }
            else if ((deltaX < 0 && slope > 0 && slope < 1) || (deltaX < 0 && slope < 0 && slope > -1)) //left
            {
                workDirection = 2;
                animator.SetInteger(playerState, (int)PLAYERWORKSTATE.left);
                toolAnimator.SetInteger(toolName[selectedToolId], (int)PLAYERWORKSTATE.left);
            }
            else if (deltaY >= 0) //up
            {
                workDirection = 3;
                animator.SetInteger(playerState, (int)PLAYERWORKSTATE.up);
                toolAnimator.SetInteger(toolName[selectedToolId], (int)PLAYERWORKSTATE.up);
            }
            else //down
            {
                workDirection = 4;
                animator.SetInteger(playerState, (int)PLAYERWORKSTATE.down);
                toolAnimator.SetInteger(toolName[selectedToolId], (int)PLAYERWORKSTATE.down);
            }
        }
    } 


    void PlayerIDLE() {
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

}
