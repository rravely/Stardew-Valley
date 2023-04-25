using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public float animTime = 2f;
    private float start = 1f;
    private float end = 0f;
    private float time = 0f;


    
    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("FadeInOut_co");
    }

    IEnumerator FadeInOut_co() {
        yield return new WaitForSeconds(animTime);
        time += Time.deltaTime / animTime;

        Color color = spriteRenderer.color;
        color.a = Mathf.Lerp(start, end, time);
        spriteRenderer.color = color;

        color = spriteRenderer.color;
        color.a = Mathf.Lerp(end, start, time);  //FadeIn과는 달리 start, end가 반대
        spriteRenderer.color = color;
    }
}
