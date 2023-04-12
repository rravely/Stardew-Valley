using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetting : MonoBehaviour
{
    [SerializeField]private GameObject title;
    [SerializeField]private GameObject loadingUI;
    [SerializeField]private GameObject playerSettingUI;
    [SerializeField]private GameObject playerNameUI;
    [SerializeField]private GameObject playerFarmNameUI;

    // Update is called once per frame
    void Update()
    {
        // 타이틀 로고가 카메라 화면 밖으로 다 나가면
        if (title.transform.position.x > 14.9) {
            loadingUI.SetActive(false); //로딩중 UI 비활성화
            //player 정보 입력 활성화
            playerSettingUI.SetActive(true);
            playerNameUI.SetActive(true);
            playerFarmNameUI.SetActive(true);
        }
    }
}
