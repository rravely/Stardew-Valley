using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAndFarming : MonoBehaviour
{   
    //플레이어
    private GameObject player;
    private PlayerControl playerControl;
    public Camera camera;
    private float deltaX, deltaY, slope;



    void Start() {
        player = GameObject.FindWithTag("Player");
        playerControl = player.GetComponent<PlayerControl>();
    }

    void Update() {
        MouseClick();
    }

    void OnCollisionStay2D(Collision2D collision) {
        switch (playerControl.playerDirection)
        { //플레이어가 바라보는 방향이
            case 1: //right
                if (player.transform.position.x < transform.position.x) //플레이어의 위치가 이 오브젝트보다 왼쪽에 있으면
                {
                    
                }
                break;
            }
        
        
    }

    void MouseClick() {
        if (Input.GetMouseButtonDown(0) && playerControl.selectedToolId != -1) //마우스가 눌리고 선택된 아이템이 도구라면
        {
            //방향 구하기
            deltaX = camera.ScreenToWorldPoint(Input.mousePosition).x - player.transform.position.x;
            deltaY = camera.ScreenToWorldPoint(Input.mousePosition).y - player.transform.position.y;
            slope = deltaY / deltaX;
            
            if ((deltaX > 0 && slope > 0 && slope < 1) || (deltaX > 0 && slope < 0 && slope > -1)) //right
            {
                //그 자리에 서서 플레이어 오른쪽 애니메이션
                //오른쪽 방향의 오브젝트와 충돌중이면 그 오브젝트 파괴하고 아이템 얻기
                Debug.Log("right");
            }
            else if ((deltaX < 0 && slope > 0 && slope < 1) || (deltaX < 0 && slope < 0 && slope > -1)) //left
            {
                Debug.Log("left");
            }
            else if (deltaY > 0)
            {
                Debug.Log("Up");
            }
            else 
            {
                Debug.Log("Down");
            }

            
            Debug.Log("click");
        }
    }
}
