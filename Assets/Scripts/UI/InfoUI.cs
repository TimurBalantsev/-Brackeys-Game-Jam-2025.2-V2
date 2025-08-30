using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{
    [SerializeField] private float difficultyStarStartDelay = 2f;
    [SerializeField] private float difficultyStarDelay = 0.5f;
    [SerializeField] private float fadeOutDelay = 2f;
    [SerializeField] private float fadeOutDuration = 1f;

    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Transform diffcultyStarsContainer;
    [SerializeField] private GameObject difficultyStarPrefab;

    [SerializeField] private AudioClipSO levelStartSound;
    [SerializeField] private AudioClipSO difficultyStarImpactSound;

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
        StartCoroutine(ShowInfo(levelSO, currentStreak));

    }

    private IEnumerator ShowInfo(LevelSO levelSO, int currentStreak)
    {
        infoText.text = levelSO.levelName;
        infoText.gameObject.SetActive(true);

        AudioManager.Instance.PlayTempSoundAt(Player.Instance.transform.position, levelStartSound.GetRandomAudioClipReference());

        yield return new WaitForSeconds(difficultyStarStartDelay);

        StartCoroutine(ShowDifficultyStars(currentStreak));
    }

    private IEnumerator ShowDifficultyStars(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(difficultyStarPrefab, diffcultyStarsContainer);
            AudioManager.Instance.PlayTempSoundAt(Player.Instance.transform.position, difficultyStarImpactSound.GetRandomAudioClipReference());
            yield return new WaitForSeconds(difficultyStarDelay);
        }

        yield return new WaitForSeconds(fadeOutDelay);
        yield return StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        Transform[] stars = new Transform[diffcultyStarsContainer.childCount];
        for (int i = 0; i < stars.Length; i++)
            stars[i] = diffcultyStarsContainer.GetChild(i);

        Color[] starColors = new Color[stars.Length];
        for (int i = 0; i < stars.Length; i++)
        {
            var renderer = stars[i].GetComponent<Image>();
            if (renderer != null)
                starColors[i] = renderer.color;
        }

        Color infoOriginalColor = infoText.color;

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);

            infoText.color = new Color(infoOriginalColor.r, infoOriginalColor.g, infoOriginalColor.b, alpha);

            for (int i = 0; i < stars.Length; i++)
            {
                var renderer = stars[i].GetComponent<Image>();
                if (renderer != null)
                    renderer.color = new Color(starColors[i].r, starColors[i].g, starColors[i].b, alpha);
            }

            yield return null;
        }

        infoText.gameObject.SetActive(false);
        for (int i = 0; i < stars.Length; i++)
            Destroy(stars[i].gameObject);
    }
}
