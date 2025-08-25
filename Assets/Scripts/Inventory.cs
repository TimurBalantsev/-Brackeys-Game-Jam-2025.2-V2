using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int maxSlots;
    [SerializeField] private float maxWeight;
    [SerializeField] private float maxWeightBuffer;
    [SerializeField] private bool isContainer;
    [SerializeField] private string inventoryName;
    private float currentWeight;
    private List<Item> items;

    [SerializeField] private List<ItemSO> testItems;
    
    
    
    public int MaxSlots => maxSlots;
    public float MaxWeight => maxWeight;
    public bool IsContainer => isContainer;
    public float CurrentWeight => currentWeight;
    public int CurrentSlots => items.Count;
    public string InventoryName => inventoryName;
    public List<Item> Items => items;
    
    private void Awake()
    {
        items = new List<Item>(maxSlots);
        foreach (ItemSO itemSO in testItems)
        {
            items.Add(itemSO.CreateItem());
        }
    }

    public bool AddItem(Item item)
    {
        if (items.Count >= maxSlots) return false;
        if (currentWeight > maxWeight) return false;
        if (currentWeight + item.weight > maxWeight + maxWeightBuffer) return false;
        
        items.Add(item);
        currentWeight += item.weight;
        return true;
    }
    
    public bool RemoveItem(Item item)
    {
        if (items.Remove(item))
        {
            currentWeight -= item.weight;
            if (Mathf.Abs(currentWeight) < 0.005f) {
                currentWeight = 0f;
            }
            return true;
        }
        return false;
    }
    
    public bool IsOverWeight()
    {
        return currentWeight > maxWeight;
    }
}
