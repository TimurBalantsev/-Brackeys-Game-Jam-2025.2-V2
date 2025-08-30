using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;

    public event Action<LevelSO, int> OnLevelLoaded;


    [SerializeField] private LevelSO[] levels;
    [SerializeField] private int nextLevelOptionsAmount;

    private LevelSO currentLevel;
    private LevelSO[] nextLevelOptions; 
    private int currentStreak;
    public int CurrentStreak => currentStreak;
    public LevelSO CurrentLevel => currentLevel;
    public LevelSO[] NextLevelOptions => nextLevelOptions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }
        else
        {
            Debug.LogError("Multiple LoadingManagers in scene");
        }
    }

    public void BackToBase()
    {
        currentStreak = 0;
        Fade.Instance.FadeIn(2f, () =>
        {
            BaseManager.Instance.TransferItems(Player.Instance.Inventory);
        });
    }

    public void StartGame()
    {
        currentStreak = 0;
        LoadLevel(levels[UnityEngine.Random.Range(0, levels.Length)], true);
    }

    public void LoadMenu()
    {
        Fade.Instance.FadeIn(2f, () =>
        {
            SceneManager.LoadScene("Menu");
        });
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "Game")
        {
            InstantiateLevel(currentLevel);
        }
        if (scene.name == "Menu")
        {
            Fade.Instance.FadeOut(2f);
        }
    }

    public void LoadLevel(LevelSO levelSO, bool fade)
    {
        Time.timeScale = 0f;

        currentStreak++;

        if (fade)
        {
            Fade.Instance.FadeIn(2f, () =>
            {
                SceneManager.LoadScene("Game");
                currentLevel = levelSO;
            });
        }
        else
        {
            SceneManager.LoadScene("Game");
            currentLevel = levelSO;
        }
    }

    private void ChooseNextLevelOptions()
    {
        // Exclude current level
        List<LevelSO> availableLevels = levels.Where(l => l != currentLevel).ToList();

        nextLevelOptions = availableLevels
            .OrderBy(x => UnityEngine.Random.value)
            .Take(nextLevelOptionsAmount)
            .ToArray();
    }

    private void InstantiateLevel(LevelSO levelSO)
    {
        Instantiate(levelSO.prefab);

        Time.timeScale = 1f;
        Fade.Instance.FadeOut(2f);

        ChooseNextLevelOptions();

        OnLevelLoaded?.Invoke(levelSO, currentStreak);
    }
}
