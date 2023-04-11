using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField] private float Speed = 5f;


    IEnumerator ScrollObject_co() {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
        yield return new WaitForSecondsRealtime(4);

    }
}
