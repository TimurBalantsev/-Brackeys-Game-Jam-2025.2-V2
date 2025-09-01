using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Bounds cameraBounds;
    private Vector3 targetPosition;

    private Transform followTarget;

    private Camera mainCamera;

    public static MainCamera Instance;

    public Camera GetMainCamera => mainCamera;
    
    

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        if (Instance != null)
        {
            Debug.LogError("More than one main camera in scene");
        }

        Instance = this;
    }

    private void Start()
    {
        Bounds levelBounds = SetBounds.Instance.LevelBounds;

        float height = mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;

        float minX = levelBounds.min.x + width;
        float maxX = levelBounds.max.x - width;

        float minY = levelBounds.min.y + height;
        float maxY = levelBounds.max.y - height;

        cameraBounds = new Bounds();
        cameraBounds.SetMinMax(new Vector3(minX, minY, 0f), new Vector3(maxX, maxY, 0f));

        followTarget = Player.Instance.transform;
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
