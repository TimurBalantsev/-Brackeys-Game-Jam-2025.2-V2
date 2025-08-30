using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private Image preview;
    [SerializeField] private TextMeshProUGUI levelName;

    public void DisplayLevel(LevelSO levelSO)
    {
        preview.sprite = levelSO.preview;
        levelName.text = levelSO.levelName;
    }
}
