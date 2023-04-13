using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float cameraSpeed = 5.0f;

    [SerializeField] private GameObject player;

    [SerializeField] private Vector2 center;
    [SerializeField] private Vector2 mapSize;

    float height, width, clampX, clampY;

    private void Start()
    {
        height = Camera.main.orthographicSize; //카메라의 화면의 높이
        width = height * Screen.width / Screen.height; //디스플레이 비율에 맞게 카메라 화면의 너비 계산
    }

    void LimitCameraArea()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * cameraSpeed);
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    void FixedUpdate()
    {
        LimitCameraArea();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
}
