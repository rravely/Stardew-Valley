using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SaveData player; //플레이어의 정보를 저장
    private Item tempItem;
    void Awake()
    {
        //플레이어 정보 불러오기
        player = SaveSystem.Load("Default"); 
        Debug.Log("현재 플레이어: " + player.name);

        //아이템 정보 배열을 아이템 리스트로 바꾸기
        for (int i = 0; i < player.itemsIdArray.Length; i++)
        {
            tempItem = ItemDatabase.GetItem(player.itemsIdArray[i]); //해당 Id에 맞는 아이템을 정보를 가져오고
            tempItem.ItemCount = player.itemsCountArray[i]; //그 아이템의 수량을 저장
            if (player.saveInventory == null) { //saveInventory가 생성되지 않았다면 생성하고 저장
                player.saveInventory = new List<Item>();
            }
            player.saveInventory.Add(tempItem);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
