using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    private void Awake() 
    {
        BuildDatabase();
    }

    public Item GetItem(int Id) {
        return items.Find(item => item.Id == Id);
    }

    public Item GetItem(string ItemName) {
        return items.Find(item => item.ItemName == ItemName);
    }

    void BuildDatabase() {
        items = new List<Item>() {
            new Item(0, "Hoe", "Used to dig up dirt."),
            new Item(1, "Pickaxe", "Used to smash rocks and mine."),
            new Item(2, "Axe", "Used to chop wood."),
            new Item(3, "Wateringcan", "Used to water your crops."),
            new Item(4, "Scythe", "Used to cut down grass and plants.")
            
            
            };

    }
}
