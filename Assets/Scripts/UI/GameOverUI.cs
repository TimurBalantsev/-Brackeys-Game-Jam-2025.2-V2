using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI reasonText;

    [SerializeField] private GameObject background;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple GameOverUIs in scene");
        }
    }

    public void Show(string reason, bool fadeIn)
    {
        if (fadeIn)
        {
            Fade.Instance.FadeIn(2f, () =>
            {
                reasonText.text = reason;
                background.SetActive(true);
            });
        }
        else
        {
            reasonText.text = reason;
            background.SetActive(true);
        }
    }
}
