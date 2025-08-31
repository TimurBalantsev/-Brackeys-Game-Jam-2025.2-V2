using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }

    [SerializeField] private Button tryAgainButton;
    [SerializeField] private Button menuButton;

    [SerializeField] private TextMeshProUGUI reasonText;

    [SerializeField] private GameObject background;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;

            tryAgainButton.onClick.AddListener(Restart);
            tryAgainButton.onClick.AddListener(Menu);
        }
        else
        {
            Debug.LogError("Multiple GameOverUIs in scene");
        }
    }

    private void Restart()
    {
        gameObject.SetActive(false);
        LoadingManager.Instance.RestartGame();
    }

    private void Menu()
    {
        gameObject.SetActive(false);
        menuButton.onClick.AddListener(LoadingManager.Instance.LoadMenu);
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
