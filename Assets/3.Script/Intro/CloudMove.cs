using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    [SerializeField]public float Speed = 0.2f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > -12 && transform.position.x < 15) {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
        }
        else {
            Reposition();
        }
        
    }

    void Reposition() {
        Vector2 offset = new Vector2(25, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}
