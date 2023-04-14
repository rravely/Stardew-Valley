using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InventoryItem
{
    public bool isFull;
    public Item item;
}
[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public string itemDescription;
    public Sprite icon;
    public bool stackable = true;


    public Item(int id, string itemName, string itemDescription)
    {
        this.id = id;
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.icon = Resources.Load<Sprite>("4.Sprite/Item/" + itemName);
        //itemCount = 1;
    }

    public Item(Item item)
    {
        this.id = item.id;
        this.itemName = item.itemName;
        this.itemDescription = item.itemDescription;
        this.icon = Resources.Load<Sprite>("Assets/4.Sprite/Item/" + item.itemName);
        //item.itemCount = 1;
    }
    
}
