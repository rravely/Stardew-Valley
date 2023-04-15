using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Movement2D movement2D; //플레이어 이동
    private Animator animator;
    string animationState = "player_walk";
    int lastWalkingState = 0; //이전에 걸어간 상태
    private SpriteRenderer spriteRenderer; 
    private float playerPosZ; //플레이어와 건물 사이의 z 위치 비교를 위해
    [SerializeField] private Sprite[] playerSprite = new Sprite[4]; //방향에 따라 가만히 있는 sprite
    private int playerDirection = 0; //플레이어가 움직이는 방향
    [HideInInspector] public int selectedToolId = -1; //플레이어가 선택한 도구 (-1은 아직 선택 x)
    

    enum PLAYERWALKSTATE{
        idle = 0,
        right = 1,
        left = 2,
        up = 3,
        down = 4
    }
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
    }

    void ChangeDirection() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x > 0) //right
        {
            animator.SetInteger(animationState, (int)PLAYERWALKSTATE.right);
            lastWalkingState = 1;
            movement2D.MoveTo(new Vector3(x, 0, 0f));
            
        }
        else if (x < 0)
        {
            animator.SetInteger(animationState, (int)PLAYERWALKSTATE.left);
            lastWalkingState = 2;
            movement2D.MoveTo(new Vector3(x, 0, 0f));
        }
        else if (y > 0) //up
        {
            animator.SetInteger(animationState, (int)PLAYERWALKSTATE.up);
            lastWalkingState = 3;
            movement2D.MoveTo(new Vector3(0, y, y));
        }
        else if (y < 0)
        {
            animator.SetInteger(animationState, (int)PLAYERWALKSTATE.down);
            lastWalkingState = 4;
            movement2D.MoveTo(new Vector3(0, y, y));
        }
        else if (x.Equals(0) && y.Equals(0))
        {
            //animator.SetInteger(animationState, (int)PLAYERWALKSTATE.idle);
            switch (lastWalkingState)
            {
                case 1:
                    spriteRenderer.sprite = playerSprite[0];
                    movement2D.MoveTo(new Vector3(0, 0, 0f));
                    break;
                case 2:
                    spriteRenderer.sprite = playerSprite[1];
                    movement2D.MoveTo(new Vector3(0, 0, 0f));
                    break;
                case 3:
                    spriteRenderer.sprite = playerSprite[2];
                    movement2D.MoveTo(new Vector3(0, 0, 0f));
                    break;
                case 4:
                    spriteRenderer.sprite = playerSprite[3];
                    movement2D.MoveTo(new Vector3(0, 0, 0f));
                    break;
            }
        }
    }

    void ChangeZSameAsY() {
        playerPosZ = transform.position.y;
        transform.position = new Vector3(transform.position.x, transform.position.y, playerPosZ);
    }

    void Working(int selectedToolId) {
        switch (selectedToolId) {
            case 0: // 도끼
                break;
            case 1: //괭이
                break;
            case 2: //곡괭이
                break;
            case 3: //물뿌리개
                break;
            case 4: //낫
                break;
        }
    }



}
