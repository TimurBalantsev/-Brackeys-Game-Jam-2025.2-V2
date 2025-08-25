using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int maxSlots;
    [SerializeField] private float maxWeight;
    [SerializeField] private float maxWeightBuffer;
    [SerializeField] private bool isContainer;
    private float currentWeight;
    List<Item> items;
    
    private void Awake()
    {
        items = new List<Item>(maxSlots);
    }

    public bool AddItem(Item item)
    {
        if (items.Count >= maxSlots) return false;
        if (currentWeight > maxWeight) return false;
        if (currentWeight + item.Weight > maxWeight + maxWeightBuffer) return false;
        
        items.Add(item);
        currentWeight += item.Weight;
        return true;
    }
    
    public bool RemoveItem(Item item)
    {
        if (items.Remove(item))
        {
            currentWeight -= item.Weight;
            return true;
        }
        return false;
    }
    
    public bool IsOverWeight()
    {
        return currentWeight > maxWeight;
    }
}
