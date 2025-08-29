using System;
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

    public int Population => population;
    public Quest CurrentQuest => currentQuest;

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

