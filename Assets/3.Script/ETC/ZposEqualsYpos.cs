using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZposEqualsYpos : MonoBehaviour
{
    void Start()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = y - GetComponent<Renderer>().bounds.size.y * 0.5f; //오브젝트의 height를 고려해서
        transform.position = new Vector3(x, y, z);
    }
}
