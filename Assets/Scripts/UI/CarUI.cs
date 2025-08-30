using UnityEngine;
using UnityEngine.UI;

public class CarUI : MonoBehaviour
{
    [SerializeField] private GameObject background;

    [SerializeField] private Button closeButton;
    [SerializeField] private Button backToBaseButton;

    [SerializeField] private LevelDisplay[] levelDisplays;

    private void Start()
    {
        closeButton.onClick.AddListener(Close);
        backToBaseButton.onClick.AddListener(BackToBase);

        Car.Instance.OnInteract += Instance_OnInteract;
    }

    private void OnDestroy()
    {
        Car.Instance.OnInteract -= Instance_OnInteract;
    }

    private void Instance_OnInteract()
    {
        Refresh();
        background.SetActive(true);
        SetPlayerCanMove();
    }

    public void SetPlayerCanMove()
    {
        Player.Instance.canMove = !background.activeSelf;
    }

    private void Refresh()
    {
        for (int i = 0; i < LoadingManager.Instance.NextLevelOptions.Length; i++)
        {
            LevelSO levelSO = LoadingManager.Instance.NextLevelOptions[i];

            levelDisplays[i].DisplayLevel(levelSO);
            levelDisplays[i].GetComponent<Button>().onClick.AddListener(() => LoadingManager.Instance.LoadLevel(levelSO, true));
        }
    }

    private void Close()
    {
        background.SetActive(false);
        SetPlayerCanMove();
    }

    private void BackToBase()
    {
        LoadingManager.Instance.BackToBase();
    }
}
