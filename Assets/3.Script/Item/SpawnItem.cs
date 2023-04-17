using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    private InventoryManager inventoryManager;
    [SerializeField]private Item item;

    void Start() {
        inventoryManager = GameObject.FindWithTag("InventoryManager").GetComponent<InventoryManager>();

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //플레이어가 가까이 있으면 플레이어 인벤토리에 추가
        if (collider.transform.CompareTag("Player")) {
            inventoryManager.AddItem(item);
            Destroy(gameObject);
        }
    }
}
