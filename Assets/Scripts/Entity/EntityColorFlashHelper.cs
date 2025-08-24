using System;
using UnityEngine;

namespace Entity
{
public class EntityColorFlashHelper : MonoBehaviour
{
    [SerializeField] private Entity parent;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    [SerializeField] private Color flashColor;
    [SerializeField] private float duration;
    private float timer;
    private bool isFlashing = false;

    private void Start()
    {
        spriteRenderer = parent.SpriteRenderer;
        originalColor = spriteRenderer.color;
    }
    
    public void FlashColor()
    {
        timer = 0f;
        isFlashing = true;
    }

    private void Update()
    {
        if (!isFlashing) return;
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            spriteRenderer.color = originalColor;
            isFlashing = false;
            return;
        }

        float t = timer / duration;
        spriteRenderer.color = flashColor;
        spriteRenderer.color = Color.Lerp(flashColor, originalColor, t);
    }
}
}

