using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Item item;
    private Image itemIcon;
    private Text itemName;
    private Text itemCost;

    void Start() {
        itemIcon = transform.GetChild(0).GetComponent<Image>();
        itemName = transform.GetChild(1).GetComponent<Text>();
        itemCost = transform.GetChild(2).GetComponent<Text>();

        itemIcon.sprite = item.icon;
        itemName.text = item.itemName;
        itemCost.text = item.cost.ToString();
    }

    
}
