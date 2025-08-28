using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Weighted Loot Table", menuName = "Loot Tables")]
public class WeightedLootTableSO : ScriptableObject
{
    [Serializable]
    public class LootTableInfo
    {
        public ItemSO itemSO;
        public int weight;
    }

    [SerializeField] private List<LootTableInfo> lootTable;

    public int TotalWeight()
    {
        int totalWeight = 0;
        foreach (LootTableInfo lootTableInfo in lootTable)
        {
            totalWeight += lootTableInfo.weight;
        }

        return totalWeight;
    }

    public Item GetRandomItem()
    {
        int selectedWeight = Random.Range(0, TotalWeight());
        foreach (LootTableInfo lootTableInfo in lootTable)
        {
            selectedWeight -= lootTableInfo.weight;
            if (selectedWeight < 0)
            {
                return lootTableInfo.itemSO.CreateItem();
            }
        }

        return null;
    }
}
