using UnityEngine;

public class DestroySelfOnTimeout : MonoBehaviour
{
    public float maxTime;
    private float aliveTime;
    
    private void Start()
    {
        aliveTime = 0f;
    }
    
    private void Update()
    {
        aliveTime += Time.deltaTime;
        if (aliveTime >= maxTime)
        {
            Destroy(gameObject);
        }
    }
}
