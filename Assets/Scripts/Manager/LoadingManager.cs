using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;

    public event Action<LevelSO, int> OnLevelLoaded;

    [SerializeField] private LevelSO[] levels;

    private LevelSO currentLevel;
    private int currentStreak;
    public int CurrentStreak => currentStreak;
    public LevelSO CurrentLevel => currentLevel;

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
    }

    public void StartGame()
    {
        LoadLevel(levels[0], true);
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

    private void InstantiateLevel(LevelSO levelSO)
    {
        Instantiate(levelSO.prefab);
        Fade.Instance.FadeOut(2f);

        OnLevelLoaded?.Invoke(levelSO, currentStreak);
    }
}
