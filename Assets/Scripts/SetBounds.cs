using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(EdgeCollider2D))]
[RequireComponent(typeof(Tilemap))]
public class SetBounds : MonoBehaviour
{
    public static SetBounds Instance;

    public Bounds LevelBounds { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple SetBounds Instances in scene");
        }

        Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.CompressBounds();
        Bounds bounds = tilemap.localBounds;

        LevelBounds = bounds;

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
