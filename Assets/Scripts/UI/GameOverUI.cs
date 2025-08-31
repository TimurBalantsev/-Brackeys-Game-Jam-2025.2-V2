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
        BaseManager.Instance.OnNoPopulation += Instance_OnNoPopulation;
    }

    private void OnDestroy()
    {
        Player.Instance.OnDie -= Instance_OnDie;
        BaseManager.Instance.OnNoPopulation -= Instance_OnNoPopulation;
    }

    private void Instance_OnNoPopulation()
    {
        background.SetActive(true);
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
