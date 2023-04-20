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
        if (playerControl.playerEnergy >= 0f) {
            transform.localScale = new Vector3(1f, playerControl.playerEnergy / 15f, 1f);
        }
        
    }
}
