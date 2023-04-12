using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float cameraSpeed = 5.0f;

    [SerializeField] private GameObject player;
    Vector2 tempVector;

    private void Update()
    {
        Vector2 dir = player.transform.position - this.transform.position;
        //Vector2 moveVector = new Vector2(dir.x * cameraSpeed * Time.deltaTime, dir.y * cameraSpeed * Time.deltaTime);
        //this.transform.Translate(moveVector);
        if (player.transform.position.x > 5.6 && player.transform.position.y > 3.2) {
            Vector2 moveVector = new Vector2(0f, 0f);
            this.transform.Translate(moveVector);
        }
        else if (player.transform.position.x > 5.6) {
            Vector2 moveVector = new Vector2(0f, dir.y * cameraSpeed * Time.deltaTime);
            this.transform.Translate(moveVector);
        }
        else if (player.transform.position.y > 3.2) {
            Vector2 moveVector = new Vector2(dir.x * cameraSpeed * Time.deltaTime, 0f);
            this.transform.Translate(moveVector);
        }
        else {
            Vector2 moveVector = new Vector2(dir.x * cameraSpeed * Time.deltaTime, dir.y * cameraSpeed * Time.deltaTime);
            this.transform.Translate(moveVector);
        }
        //Debug.Log(CameraPosX);
        
    }
}
