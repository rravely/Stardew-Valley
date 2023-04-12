using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int Id;
    public string ItemName;
    public string ItemDescription;
    public int ItemCount;
    public Sprite Icon;

    public Item(int Id, string ItemName, string ItemDescription)
    {
        this.Id = Id;
        this.ItemName = ItemName;
        this.ItemDescription = ItemDescription;
        this.Icon = Resources.Load<Sprite>("4.Sprites/Item/tool/" + ItemName);
        ItemCount = 1;
    }

    public Item(Item item)
    {
        this.Id = item.Id;
        this.ItemName = item.ItemName;
        this.ItemDescription = item.ItemDescription;
        this.Icon = Resources.Load<Sprite>("4.Sprites/Item/tool/" + item.ItemName);
        item.ItemCount = 1;
    }
}
