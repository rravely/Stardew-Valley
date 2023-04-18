using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float cameraSpeed = 5.0f;

    [SerializeField] private GameObject player;

    [Header("HouseEntry")]
    [SerializeField] private Vector2 center;
    [SerializeField] private Vector2 mapSize;

    [Header("House")]
    [SerializeField] private Vector3 houseCenter;

    [Header("Town")]
    [SerializeField] private Vector2 townCenter; 
    [SerializeField] private Vector2 townMapSize;

    [Header("Shop")]
    [SerializeField] private Vector3 shopCenter;



    float height, width, clampX, clampY;

    private void Start()
    {
        height = Camera.main.orthographicSize; //카메라의 화면의 높이
        width = height * Screen.width / Screen.height; //디스플레이 비율에 맞게 카메라 화면의 너비 계산
    }

    void LimitCameraAreaFarm()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * cameraSpeed);
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    void LimitCameraAreaHouse() {
        transform.position = houseCenter;
    }

    void LimitCameraAreaTown() {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * cameraSpeed);
        float lx = townMapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + townCenter.x, lx + townCenter.x);

        float ly = townMapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + townCenter.y, ly + townCenter.y);
        transform.position = new Vector3(clampX, clampY, -10f);
    }

    void LimitCameraAreaShop() {
        transform.position = shopCenter;
    }

    void FixedUpdate()
    {
        if (player.transform.position.y < center.y + mapSize.y && player.transform.position.x < center.x + mapSize.x) { //farm에 위치해 있으면
            LimitCameraAreaFarm();
        }
        else if (player.transform.position.y > center.y + mapSize.y && player.transform.position.x < center.x + mapSize.x ) { //house에 위치해 있으면
            LimitCameraAreaHouse();
        }
        else if (player.transform.position.x > center.x + mapSize.x  && player.transform.position.y < townCenter.y + townMapSize.y ) { //town에 위치해 있으면
            LimitCameraAreaTown();
        }
        else if (player.transform.position.x > center.x + mapSize.x  && player.transform.position.y > townCenter.y + townMapSize.y ) { //shop에 위치
            LimitCameraAreaShop();
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(townCenter, townMapSize * 2);
    }
}
