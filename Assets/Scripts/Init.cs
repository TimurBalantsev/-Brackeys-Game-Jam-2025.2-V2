using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene("Menu");
    }
}
