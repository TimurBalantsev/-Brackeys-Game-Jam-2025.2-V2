using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BaseManager : MonoBehaviour
{
    [SerializeField] private WeightedLootTableSO weightedLootTable;
    [SerializeField] private int amountMin;
    [SerializeField] private int amountMax;
    
    [SerializeField] private int questRewardPopulationMin;
    [SerializeField] private int questRewardPopulationMax;
    [SerializeField] private int questFailPopulationMin;
    [SerializeField] private int questFailPopulationMax;
    
    [SerializeField] private int eventRewardPopulationMin;
    [SerializeField] private int eventRewardPopulationMax;
    [SerializeField] private int eventFailPopulationMin;
    [SerializeField] private int eventFailPopulationMax;


    [SerializeField] private int population;
    [SerializeField] private Inventory inventory;

    public List<Quest> ActiveQuests = new List<Quest>();
    public List<Quest> ActiveEvents = new List<Quest>();
    
    
    
    public static BaseManager Instance;

    public int Population => population;

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
 
        ItemType itemType = (ItemType)Random.Range(0, Enum.GetValues(typeof(ItemType)).Length); //get random item type
        Debug.Log(itemType.ToString());
        GetNewQuest(itemType);
        foreach (Quest quest in ActiveQuests)
        {
            Debug.Log(quest);
        }
    }

    public Item GetRandomItemByType(ItemType itemType)
    {
        if (!weightedLootTable.ContainsItemType(itemType)) return null;
        Item questItem;
        do
        {
            questItem = weightedLootTable.GetRandomItem();
        } while (questItem.itemType != itemType);

        return questItem;
    }

    public Quest GetRandomEvent(ItemType itemType)
    {
        Item questItem = GetRandomItemByType(itemType);
        if (questItem == null)
        {
            Debug.LogError("itemtype doesnt exist in loot table");
            return null;
        }
        Quest randomEvent = new Quest(Random.Range(amountMin, amountMax), questItem,Random.Range(eventRewardPopulationMin, eventRewardPopulationMax),Random.Range(eventFailPopulationMin, eventFailPopulationMax));
        ActiveEvents.Add(randomEvent);
        return randomEvent;
    }

    public Quest GetNewQuest(ItemType itemType)
    {
        Item item = GetRandomItemByType(itemType);
        if (item == null)
        {
            Debug.LogError("itemtype doesnt exist in loot table");
            return null;
        }
        Quest currentQuest = new Quest(Random.Range(amountMin, amountMax), item,Random.Range(questRewardPopulationMin, questRewardPopulationMax),Random.Range(questFailPopulationMin, questFailPopulationMax));
        ActiveQuests.Add(currentQuest);
        return currentQuest;
    }
    
    public void TransferItems(Inventory inventory)
    {
        foreach (Item item in inventory.Items)
        {
            this.inventory.AddItem(item);
        }
        inventory.Clear();
    }

    public bool TurnInQuest(Quest quest, bool isEvent)
    {
        List<Item> toBeRemoved = new List<Item>();
        int targetItemAmount = quest.Amount;
        foreach (Item item in inventory.Items)
        {
            if (item.parent == quest.Item.parent && targetItemAmount > 0)
            {
                targetItemAmount--;
                toBeRemoved.Add(item);
            }

            if (targetItemAmount <= 0)
            {
                break;
            }
        }

        foreach (Item item in toBeRemoved)
        {
            inventory.RemoveItem(item);
        }

        if (isEvent) ActiveEvents.Remove(quest);
        else ActiveQuests.Remove(quest);

        if (targetItemAmount <= 0)
        {
            population += quest.PopReward;
            Debug.Log("quest completed");
            return true;
        }

        population -= quest.PopConsequence;
        Debug.Log("quest failed");
        return false;
    }
}

