using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform followTarget;

    public static Bounds levelBounds;

    public static Bounds cameraBounds;
    private Vector3 targetPosition;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void Start()
    {
        float height = mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;

        float minX = levelBounds.min.x + width;
        float maxX = levelBounds.max.x - width;

        float minY = levelBounds.min.y + height;
        float maxY = levelBounds.max.y - height;

        cameraBounds = new Bounds();
        cameraBounds.SetMinMax(new Vector3(minX, minY, 0f), new Vector3(maxX, maxY, 0f));
    }

    private void LateUpdate()
    {
        targetPosition = followTarget.position;   
        targetPosition = GetCameraBounds();

        transform.position = targetPosition;
    }

    private Vector3 GetCameraBounds()
    {
        return new Vector3(Mathf.Clamp(targetPosition.x, cameraBounds.min.x, cameraBounds.max.x), Mathf.Clamp(targetPosition.y, cameraBounds.min.y, cameraBounds.max.y), transform.position.z);
    }
}
