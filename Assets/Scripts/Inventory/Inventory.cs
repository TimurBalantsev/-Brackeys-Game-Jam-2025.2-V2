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
    private float currentSlots;
    private List<Item> items;

    [SerializeField] private List<ItemSO> testItems;
    
    
    
    public int MaxSlots => maxSlots;
    public float MaxWeight => maxWeight;
    public bool IsContainer => isContainer;
    public float CurrentWeight => currentWeight;
    public float CurrentSlots => currentSlots;
    public string InventoryName => inventoryName;
    public List<Item> Items => items;
    
    private void Awake()
    {
        items = new List<Item>();
        foreach (ItemSO itemSO in testItems)
        {
            AddItem(itemSO.CreateItem());
        }
    }

    public bool AddItem(Item item)
    {
        if (items.Count >= maxSlots) return false;
        if (currentWeight > maxWeight) return false;
        if (currentWeight + item.weight > maxWeight + maxWeightBuffer) return false;
        if (currentSlots + item.slot > maxSlots) return false;
        
        items.Add(item);
        currentWeight += item.weight;
        currentSlots += item.slot;
        return true;
    }
    
    public bool RemoveItem(Item item)
    {
        if (items.Remove(item))
        {
            currentWeight -= item.weight;
            currentSlots -= item.slot;
            if (Mathf.Abs(currentWeight) < 0.005f) {
                currentWeight = 0f;
            }
            if (Mathf.Abs(currentSlots) < 0.005f) {
                currentSlots = 0f;
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
