using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float cameraSpeed = 5.0f;

    [SerializeField] private GameObject player;

    private void Update()
    {
        Vector2 dir = player.transform.position - this.transform.position;
        Vector2 moveVector = new Vector2(dir.x * cameraSpeed * Time.deltaTime, dir.y * cameraSpeed * Time.deltaTime);
        this.transform.Translate(moveVector);
        if (player.transform.position.x > 5.5) {
            
            player.transform.position = new Vector2(5.5f, transform.position.y);
        }
        else if (player.transform.position.y > 3.1) {
            player.transform.position = new Vector2(transform.position.x, 3.1f);
        }
        //Debug.Log(CameraPosX);
        
    }
}
