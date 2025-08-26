using System;
using HitBox;
using UnityEngine;

public class Scarecrow : MonoBehaviour, Interactable, AttackHitBoxSource
{
    [SerializeField] private string scareMessage;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color highlightColor = Color.yellow;
    [SerializeField] private float damage;
    private Color defaultColor;

    private void Start()
    {
        defaultColor = spriteRenderer.color;
    }

    public void Interact(Player player)
    {
        Debug.Log(scareMessage);
    }

    public void Select(Player player, bool isSelected)
    {
        spriteRenderer.color = isSelected ? highlightColor : defaultColor;
    }

    public float GetDamage()
    {
        return damage;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void DealDamage(Damageable target)
    {
        target.TakeDamage(damage);
    }
}
