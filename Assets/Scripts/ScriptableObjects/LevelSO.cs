using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "LevelSO")]
public class LevelSO : ScriptableObject
{
    public string levelName;
    public Sprite preview;
    public GameObject prefab;
}
