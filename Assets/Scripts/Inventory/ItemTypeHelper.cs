
using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemTypeHelper : MonoBehaviour
{
    [Serializable]
    public class ItemTypeToIcon
    {
        public ItemType type;
        public Sprite typeIcon;
        public Color typeColor;
    }
    public List<ItemTypeToIcon> itemTypeToIcons;
    public static ItemTypeHelper Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of ItemTypeHelper found!");
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public Sprite GetItemTypeSprite(ItemType itemType)
    {
        ItemTypeToIcon itemTypeToIcon = itemTypeToIcons.Find(x => x.type == itemType);
        if (itemTypeToIcon == null)
        {
            Debug.LogError("ItemType not found: " + itemType);
            return null;
        }
        return itemTypeToIcon.typeIcon;
    }
    
    public Color GetItemTypeColor(ItemType itemType)
    {
        ItemTypeToIcon itemTypeToIcon = itemTypeToIcons.Find(x => x.type == itemType);
        if (itemTypeToIcon == null)
        {
            Debug.LogError("ItemType not found: " + itemType);
            return Color.white;
        }
        return itemTypeToIcon.typeColor;
    }   
}