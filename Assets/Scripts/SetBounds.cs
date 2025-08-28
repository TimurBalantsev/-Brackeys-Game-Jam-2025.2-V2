using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(EdgeCollider2D))]
[RequireComponent(typeof(Tilemap))]
public class SetBounds : MonoBehaviour
{
    private void Awake()
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.CompressBounds();
        Bounds bounds = tilemap.localBounds;

        MainCamera.levelBounds = bounds;

        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();
        Vector2 bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        Vector2 topLeft = new Vector2(bounds.min.x, bounds.max.y);
        Vector2 topRight = new Vector2(bounds.max.x, bounds.max.y);
        Vector2 bottomRight = new Vector2(bounds.max.x, bounds.min.y);

        edgeCollider.points = new Vector2[]
        {
            bottomLeft,
            topLeft,
            topRight,
            bottomRight,
            bottomLeft
        };
    }
}
