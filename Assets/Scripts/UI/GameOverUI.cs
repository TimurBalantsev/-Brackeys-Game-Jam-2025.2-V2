using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private Button menuButton;

    [SerializeField] private GameObject background;

    private void Start()
    {
        Player.Instance.OnDie += Instance_OnDie;
    }

    private void OnDestroy()
    {
        Player.Instance.OnDie -= Instance_OnDie;
    }

    private void Instance_OnDie()
    {
        background.SetActive(true);
    }

    private void Awake()
    {
        tryAgainButton.onClick.AddListener(LoadingManager.Instance.StartGame);
        menuButton.onClick.AddListener(LoadingManager.Instance.LoadMenu);
    }
}
