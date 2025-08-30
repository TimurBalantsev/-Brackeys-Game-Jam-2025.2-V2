using UnityEngine;
using UnityEngine.UI;

public class CarUI : MonoBehaviour
{
    [SerializeField] private Button backToBaseButton;

    private void Start()
    {
        backToBaseButton.onClick.AddListener(BackToBase);
    }

    private void BackToBase()
    {
        LoadingManager.Instance.BackToBase();
    }
}
