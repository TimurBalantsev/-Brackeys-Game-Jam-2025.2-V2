using UnityEngine;
using UnityEngine.UI;

public class CarUI : MonoBehaviour
{
    [SerializeField] private GameObject background;

    [SerializeField] private Button closeButton;
    [SerializeField] private Button backToBaseButton;

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
        background.SetActive(true);
        SetPlayerCanMove();
    }

    public void SetPlayerCanMove()
    {
        Player.Instance.canMove = !background.activeSelf;
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
