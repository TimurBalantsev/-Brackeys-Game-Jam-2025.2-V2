using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tiles/Interactable Tile")]
public class InteractableTile : TileBase
{
    // Test

    public string interactionName;
    public Sprite sprite;
    public GameObject prefab;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = sprite;
        tileData.color = Color.white;
        tileData.colliderType = Tile.ColliderType.Sprite;
    }
}