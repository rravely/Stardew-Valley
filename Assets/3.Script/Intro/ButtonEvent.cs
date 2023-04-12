using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    private Scroll scroll;
    public void NewButtonMove(float speed) {
        gameObject.AddComponent<Scroll>(); //Scroll 컴포넌트를 추가
        scroll = GetComponent<Scroll>(); //Scroll 컴포넌트 가져오기
        scroll.Speed = speed;
        scroll.enabled = true;
    }

    public void NewButtonVisible() {
        gameObject.SetActive(true);
    }
}
