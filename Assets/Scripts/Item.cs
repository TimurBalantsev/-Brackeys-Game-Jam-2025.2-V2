using System;
using UnityEngine;

[Serializable]
public class Item
{
    public string name { get; private set; }
    public string description { get; private set; }
    public ItemType itemType { get; private set; }
    public float weight { get; private set; }
    public float slot { get; private set; }
    public Sprite icon { get; private set; }

    public ItemSO parent;

    public Item(string name, string description, ItemType itemType, float weight, float slot, Sprite icon,
        ItemSO parent)
    {
        this.name = name;
        this.description = description;
        this.itemType = itemType;
        this.weight = weight;
        this.slot = slot;
        this.icon = icon;
        this.parent = parent;
    }

    public Item(ItemSO itemSO)
    {
        this.name = itemSO.itemName;
        this.description = itemSO.description;
        this.itemType = itemSO.itemType;
        this.weight = itemSO.weight;
        this.slot = itemSO.slot;
        this.icon = itemSO.icon;
        this.parent = itemSO;
    }
}
