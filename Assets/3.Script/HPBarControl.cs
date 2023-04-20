using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarControl : MonoBehaviour
{
    private PlayerControl playerControl;

    void Start() {
        playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
    }

    void Update() {
        transform.localScale = new Vector3(0f, -playerControl.playerEnergy / 15f, 0f);
    }
}
