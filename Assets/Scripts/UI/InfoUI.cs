using System.Collections;
using TMPro;
using UnityEngine;

public class InfoUI : MonoBehaviour
{
    [SerializeField] private float difficultyStarWaitTime;

    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Transform diffcultyStarsContainer;
    [SerializeField] private GameObject difficultyStarPrefab;

    private void OnEnable()
    {
        LoadingManager.Instance.OnLevelLoaded += Instance_OnLevelLoaded;
    }

    private void OnDisable()
    {
        LoadingManager.Instance.OnLevelLoaded -= Instance_OnLevelLoaded;
    }

    private void Instance_OnLevelLoaded(LevelSO levelSO, int currentStreak)
    {
        infoText.text = levelSO.levelName;
        GetComponent<Animator>().enabled = true;
        StartCoroutine(ShowDifficultyStars(currentStreak));
    }

    private IEnumerator ShowDifficultyStars(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(difficultyStarWaitTime);
            Instantiate(difficultyStarPrefab, diffcultyStarsContainer);
        }
    }
}
