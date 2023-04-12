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
    private List<Item> defaultItem;


    public void OkButtonClick() {
        //입력한 값이 있을 때
        if (playerFarmName != null && playerFarmName != null) {
            //초기 아이템 리스트 생성
            defaultItem = new List<Item>();
            //SaveData 생성
            player = new SaveData(playerName.text, playerFarmName.text, defaultItem);
            player.itemsIdArray = new int[5] {0, 1, 2, 3, 4};
            player.itemsCountArray = new int[5] {1, 1, 1, 1, 1};

            //Json에 저장
            SaveSystem.Save(player, "Default");

            //씬이동
            SceneManager.LoadScene("Farm");
        }
    }

}
