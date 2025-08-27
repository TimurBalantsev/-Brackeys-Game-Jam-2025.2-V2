using UnityEngine;
using UnityEngine.Tilemaps;

public class SetCameraBounds : MonoBehaviour
{
    private void Awake()
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.CompressBounds();
        Bounds bounds = tilemap.localBounds;
        MainCamera.levelBounds = bounds;
    }
}
