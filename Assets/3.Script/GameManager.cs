using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private InventoryManager inventoryManager;
    public List<Item> itemToPickup; //초기 저장할 아이템 끌어다 두기
    public SaveData player; //플레이어의 정보를 저장

    [HideInInspector]public bool playerMouseButtonActive; //플레이어가 일하는 동작을 할것인지 확인
     
    
    private Item tempItem;
    void Awake()
    {
        //플레이어 정보 불러오기
        player = SaveSystem.Load("Default"); 
        Debug.Log("현재 플레이어: " + player.name);
        Debug.Log("플레이어 소지금 " + player.playerMoney);

        playerMouseButtonActive = true;
    }

    void Start()
    {
        inventoryManager = GameObject.FindWithTag("InventoryManager").GetComponent<InventoryManager>();
        //임시로 아이템 생성(시작하면 5개의 도구 생기게)
        for (int i = 0; i < itemToPickup.Count; i++) {
            bool addResult = inventoryManager.AddItem(itemToPickup[i]);
        }
        

    }
}
