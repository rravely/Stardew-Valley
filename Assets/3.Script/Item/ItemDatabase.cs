using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemDatabase
{
    public static List<Item> items = new List<Item>() {
            new Item(0, "Hoe", "Used to dig up dirt."),
            new Item(1, "Pickaxe", "Used to smash rocks and mine."),
            new Item(2, "Axe", "Used to chop wood."),
            new Item(3, "Wateringcan", "Used to water your crops."),
            new Item(4, "Scythe", "Used to cut down grass and plants."),
            new Item(5, "Wood", "A sturdy, yet flexible plant material with a wide variety of uses."),
            new Item(6, "Stone", "A common material with many uses in crafting and building.")


            };


    public static Item GetItem(int Id) {
        for (int i = 0; i < items.Count; i++) {
            if (items[i].Id.Equals(Id)) {
                return items[i];
            }
        }
        return items[0];
        //return items.Find(item => item.Id == Id);
    }

    public static Item GetItem(string ItemName) {
        return items.Find(item => item.ItemName == ItemName);
    }

}
