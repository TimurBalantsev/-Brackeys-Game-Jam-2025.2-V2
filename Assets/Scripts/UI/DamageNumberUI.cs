using TMPro;
using UnityEngine;

public class DamageNumberUI : MonoBehaviour
{
    private float timeAliveMax;
    private float currentTimeAlive;
    private bool isInitialized = false;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private Vector3 endPosition;
    private Vector3 startPosition;
    private Vector3 endRotation;

    public void Initialize(float timeAliveMax, Color color, Vector3 endPosition, Vector3 endRotation, float damage)
    {
        this.timeAliveMax = timeAliveMax;
        this.currentTimeAlive = 0f;
        this.startPosition = transform.position;
        this.endPosition = endPosition;
        this.endRotation = endRotation;
        textMeshPro.text = $"{damage}";
        textMeshPro.color = color;
        isInitialized = true;
    }
    
    void Update()
    {
        if (!isInitialized) return;

        currentTimeAlive += Time.deltaTime;
        if (currentTimeAlive >= timeAliveMax)
        {
            Destroy(gameObject);
            return;
        }

        float t = currentTimeAlive / timeAliveMax;
        transform.position = Vector3.Lerp(startPosition, endPosition, t);
        transform.rotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, endRotation, t));
        
        // Scale the text over time
        float scale = Mathf.Lerp(1f, 0f, t);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
