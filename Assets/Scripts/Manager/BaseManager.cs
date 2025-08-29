using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BaseManager : MonoBehaviour
{
    [SerializeField] private WeightedLootTableSO weightedLootTable;
    [SerializeField] private int amountMin;
    [SerializeField] private int amountMax;
    [SerializeField] private int questPopulationMin;
    [SerializeField] private int questPopulationMax;

    [SerializeField] private int population;
    [SerializeField] private Inventory inventory;
    public static BaseManager Instance;
    private Quest currentQuest;
    [SerializeField] private Quest forceQuest;
    private Quest randomEvent;

    public int Population => population;
    public Quest CurrentQuest => currentQuest;
    public Quest RandomEvent => randomEvent;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one base manager in scene");
        }

        Instance = this;
    }

    private void Start()
    {
        currentQuest = forceQuest;
        if (forceQuest == null)
        {
            GetNewQuest();
        }
    }

    public bool LaunchRandomEvent()
    {
        randomEvent = new Quest(Random.Range(amountMin, amountMax), weightedLootTable.GetRandomItem(),Random.Range(questPopulationMin, questPopulationMax),Random.Range(questPopulationMin, questPopulationMax));
        int targetItemAmount = currentQuest.Amount;
        List<Item> toBeRemoved = new List<Item>();
        foreach (Item item in inventory.Items)
        {
            if (item.parent == currentQuest.Item.parent && targetItemAmount > 0)
            {
                targetItemAmount--;
                toBeRemoved.Add(item);
            }
        }

        if (targetItemAmount <= 0)
        {
            Debug.Log("Event completed");
            foreach (Item item in toBeRemoved)
            {
                inventory.RemoveItem(item);
            }

            this.population += randomEvent.PopReward;
            return true;
        }
        else
        {
            Debug.Log("Event Failed");
            this.population -= randomEvent.PopConsequence;
            return false;
        }
    }

    public Quest GetNewQuest()
    {
        currentQuest = new Quest(Random.Range(amountMin, amountMax), weightedLootTable.GetRandomItem(),Random.Range(questPopulationMin, questPopulationMax),Random.Range(questPopulationMin, questPopulationMax));
        return currentQuest;
    }
    
    public void TransferItems(Inventory inventory)
    {
        int targetItemAmount = currentQuest.Amount;
        foreach (Item item in inventory.Items)
        {
            if (item.parent == currentQuest.Item.parent && targetItemAmount > 0)
            {
                targetItemAmount--;
            }
            else
            {
                this.inventory.AddItem(item);
            }
        }

        if (targetItemAmount <= 0)
        {
            population += currentQuest.PopReward;
            Debug.Log("quest completed");
        }
        else
        {
            population -= currentQuest.PopConsequence;
            Debug.Log("quest failed");
        }
        inventory.Clear();
    }
}

