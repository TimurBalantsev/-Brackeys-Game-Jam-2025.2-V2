using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private void Start()
    {
        playButton.onClick.AddListener(Play);
    }

    private void Play()
    {
        LoadingManager.Instance.StartGame();
    }
}
