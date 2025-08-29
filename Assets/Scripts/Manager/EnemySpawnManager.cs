using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnManager : MonoBehaviour
{
    [Serializable]
    public class EnemySpawnWeight
    {
        public Enemy enemyPrefab;
        public int weight;
    }

    [SerializeField] private List<EnemySpawnWeight> enemySpawnWeights;
    [SerializeField] private Transform spawnPointsContainer;
    private List<Transform> spawnPoints = new List<Transform>();

    private void Start()
    {
        foreach (Transform child in spawnPointsContainer)
        {
            spawnPoints.Add(child);
        }
        foreach (Transform spawnPoint in spawnPoints)
        {
            Vector3 position = spawnPoint.position;
            Enemy enemyPrefab = GetRandomEnemyPrefab();
            Enemy spawnedEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        }
    }

    public int TotalWeight()
    {
        int total = 0;
        foreach (EnemySpawnWeight enemy in enemySpawnWeights)
        {
            total += enemy.weight;
        }

        return total;
    }
    
    public Enemy GetRandomEnemyPrefab()
    {
        int selectedWeight = Random.Range(0, TotalWeight());
        foreach (EnemySpawnWeight enemySpawn in enemySpawnWeights)
        {
            selectedWeight -= enemySpawn.weight;
            if (selectedWeight < 0)
            {
                return enemySpawn.enemyPrefab;
            }
        }

        return null;
    }
    
}
