using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;

    public event Action<LevelSO, int> OnLevelLoaded;
    public event Action OnLoadBase;

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

    public void LoadBase()
    {
        Time.timeScale = 0f;

        Fade.Instance.FadeIn(2f, () =>
        {
            if (Car.Instance != null && Player.Instance != null)
            {
                BaseManager.Instance.TransferItems(Car.Instance.Inventory);
                BaseManager.Instance.TransferItems(Player.Instance.Inventory);
                BaseManager.Instance.TryTurnInAll();
            }

            OnLoadBase?.Invoke();
        });
    }

    public void StartGame()
    {
        BaseManager.Instance.GenerateDefaultQuests();
        LoadBase();
    }

    public void LoadLevelAfterBase()
    {
        currentStreak = 0;
        LoadLevel(levels[UnityEngine.Random.Range(0, levels.Length)], false);
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
            InstantiateCurrentLevel();
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

    public void InstantiateCurrentLevel()
    {
        Instantiate(currentLevel.prefab);

        Time.timeScale = 1f;
        Fade.Instance.FadeOut(2f);

        ChooseNextLevelOptions();

        OnLevelLoaded?.Invoke(currentLevel, currentStreak);
    }
}
