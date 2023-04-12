using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float Speed = 5f;
    GameObject title;

    void Awake() {
        title = GameObject.FindWithTag("Title"); 
    }

    void Update() {
        //title 로고가 카메라 밖을 벗어나면 이동 멈추기
        if (title.transform.position.x < 15) { 
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }
    }


/*
    IEnumerator ScrollObject_co() {
        Speed = 5f;
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
        yield return new WaitForSecondsRealtime(4);

    }
    */
}
