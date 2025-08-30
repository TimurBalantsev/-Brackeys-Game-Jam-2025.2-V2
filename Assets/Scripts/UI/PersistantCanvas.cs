using UnityEngine;
using UnityEngine.UI;

public class PersistantCanvas : MonoBehaviour
{
    public static PersistantCanvas Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogError("Multiple persistant canvases in scene");
        }
    }

    public void SetBlocking(bool blocking)
    {
        GetComponent<GraphicRaycaster>().enabled = blocking;
    }
}
