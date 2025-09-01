using UnityEngine;

public class InGameBaseInfoUIController : MonoBehaviour
{
    public static InGameBaseInfoUIController Instance;

    [SerializeField] private GameObject container;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Base Info UI controller dupilcates in scene");
        }
        Instance = this;
    }

    public void ToggleInfo()
    {
        if (container.activeSelf)
        {
            container.SetActive(false);
        }
        else
        {
            container.SetActive(true);
        }
    }
}
