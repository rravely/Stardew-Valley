using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSetting : MonoBehaviour
{
    [SerializeField]private InputField playerName;
    [SerializeField]private InputField playerFarmName;

    private SaveData player;
    public bool[] defaultIsFullArray = new bool[36];
    public int[] defaultItemsIdArray = new int[36];
    public int[] defaultItemsCountArray = new int[36];


    public void OkButtonClick() {
        //입력한 값이 있을 때
        if (playerFarmName != null && playerFarmName != null) {
            //초기 아이템 배열 생성
            for (int i = 0; i < 5; i++) {
                defaultIsFullArray[i] = true;
                defaultItemsIdArray[i] = i;
                defaultItemsCountArray[i] = 1;
            }
            for (int i = 5; i < defaultIsFullArray.Length; i++) {
                defaultIsFullArray[i] = false;
                defaultItemsIdArray[i] = i;
                defaultItemsCountArray[i] = 0;
            }
            //SaveData 생성
            player = new SaveData(playerName.text, playerFarmName.text, defaultIsFullArray, defaultItemsIdArray, defaultItemsCountArray);

            //Json에 저장
            SaveSystem.Save(player, "Default");

            //씬이동
            SceneManager.LoadScene("Farm");
        }
    }

}
