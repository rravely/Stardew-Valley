using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFromCamera : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    private float centerX, centerY;

    // Update is called once per frame
    void Update()
    {
        centerX = mainCamera.transform.position.x;
        centerY = mainCamera.transform.position.y;
        transform.position = new Vector2(centerX, centerY - 1.65f);
    }
}
