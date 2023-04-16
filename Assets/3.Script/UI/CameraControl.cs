using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float cameraSpeed = 5.0f;

    [SerializeField] private GameObject player;

    [Header("Farm")]
    [SerializeField] private Vector2 center;
    [SerializeField] private Vector2 mapSize;

    [Header("House")]
    [SerializeField] private Vector3 houseCenter;


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

    void FixedUpdate()
    {
        if (player.transform.position.y < center.y + mapSize.y) { //farm에 위치해 있으면
            LimitCameraAreaFarm();
        }
        else if (player.transform.position.y > center.y + mapSize.y) {
            LimitCameraAreaHouse();
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
