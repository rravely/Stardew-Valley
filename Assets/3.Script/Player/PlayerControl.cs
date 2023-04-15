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
    [HideInInspector] public int selectedToolId = -1; //플레이어가 선택한 도구 (-1은 아직 선택 x)
    private SpriteRenderer spriteRenderer; 
    private float playerPosZ; //플레이어와 건물 사이의 z 위치 비교를 위해
    private float deltaX, deltaY, slope;
    public Camera camera;
    

    enum PLAYERIDLESTATE{right = 1, left = 2, up = 3, down = 4}
    enum PLAYERWALKSTATE{right = 5, left = 6, up = 7, down = 8}
    enum PLAYERWORKSTATE {
        axe = 0,
        hoe = 1,
        pickaxe = 2,
        wateringcan = 3
    }

  
    void Awake()
    {
        movement2D = transform.GetComponent<Movement2D>();
        animator = transform.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        ChangeZSameAsY();
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
            //animator.SetInteger(animationState, (int)PLAYERWALKSTATE.idle);
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
            //방향 구하기
            deltaX = camera.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            deltaY = camera.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
            slope = deltaY / deltaX;

            if ((deltaX > 0 && slope > 0 && slope < 1) || (deltaX > 0 && slope < 0 && slope > -1)) //right
            {
                workDirection = 1;
            }
            else if ((deltaX < 0 && slope > 0 && slope < 1) || (deltaX < 0 && slope < 0 && slope > -1)) //left
            {
                workDirection = 2;
            }
            else if (deltaY > 0) //up
            {
                workDirection = 3;
            }
            else //down
            {
                workDirection = 4;
            }
        }
    }



}
