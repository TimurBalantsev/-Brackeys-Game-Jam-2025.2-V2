using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private float baseDifficultyScale = 1.0f;
    [SerializeField] private float baseAmountScale = 1.0f;
    [SerializeField] private float incrementPerLevel = 0.5f;
    [SerializeField] private float amountScalePerLevel = 0.4f;

    private List<Transform> spawnPoints = new List<Transform>();

    private void Start()
    {
        float difficultyScale = baseDifficultyScale + (LoadingManager.Instance.CurrentStreak - 1) * incrementPerLevel;
        int amountScale = Mathf.FloorToInt(baseAmountScale + ((LoadingManager.Instance.CurrentStreak - 1) * amountScalePerLevel));

        Debug.Log($"scaled difficulty: {difficultyScale} scaled amount: {amountScale}");

        foreach (Transform child in spawnPointsContainer)
        {
            spawnPoints.Add(child);
        }
        foreach (Transform spawnPoint in spawnPoints)
        {
            Vector3 position = spawnPoint.position;

            for (int i = 0; i < amountScale; i++)
            {
                Enemy enemyPrefab = GetRandomEnemyPrefab();
                Enemy spawnedEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);
                spawnedEnemy.stats.damage *= difficultyScale;

                spawnedEnemy.stats.SetMaxHealth(spawnedEnemy.stats.maxHealth * difficultyScale);
            }
        }
    }

    public void IncrementDifficultyScale()
    {
        baseDifficultyScale += incrementPerLevel;
    }

    public void SetDifficultyScale(float difficultyScale)
    {
        this.baseDifficultyScale = difficultyScale;
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
