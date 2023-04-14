using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]private InventoryManager inventoryManager;
    public SaveData player; //플레이어의 정보를 저장
    
    public InventoryItem[] inventory = new InventoryItem[36];
    private Item tempItem;
    void Awake()
    {
        //플레이어 정보 불러오기
        player = SaveSystem.Load("Default"); 
        for (int i = 0; i < player.isFullArray.Length; i++) {
            inventory[i].isFull = player.isFullArray[i];
            //inventory[i].item = ItemDatabase.GetItem(player.itemsIdArray[i]);
            //inventory[i].item.itemCount = player.itemsCountArray[i];
        }
        Debug.Log("현재 플레이어: " + player.name);
    }

    void Start()
    {
        //임시로 아이템 생성
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
