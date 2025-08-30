using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;

    public event Action<LevelSO, int> OnLevelLoaded;

    [SerializeField] private LevelSO[] levels;

    private GameObject currentLevelPrefab;
    private int currentStreak;

    public int CurrentStreak => currentStreak;

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
        Fade.Instance.FadeIn(2f, () =>
        {
            SceneManager.LoadScene("Game");
        });
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "Game")
        {
            LoadLevel(levels[UnityEngine.Random.Range(0, levels.Length)], false);
        }
    }

    public void LoadLevel(LevelSO levelSO, bool fadeIn)
    {
        currentStreak++;

        if (fadeIn)
        {
            Fade.Instance.FadeIn(2f, () =>
            {
                SwapLevel(levelSO);
                Fade.Instance.FadeOut(2f);
                OnLevelLoaded?.Invoke(levelSO, currentStreak);
            });
        }
        else
        {
            SwapLevel(levelSO);
            Fade.Instance.FadeOut(2f);
            OnLevelLoaded?.Invoke(levelSO, currentStreak);
        }
    }

    private void SwapLevel(LevelSO levelSO)
    {
        if (currentLevelPrefab != null)
        {
            Destroy(currentLevelPrefab);
        }

        currentLevelPrefab = Instantiate(levelSO.prefab);
    }
}
