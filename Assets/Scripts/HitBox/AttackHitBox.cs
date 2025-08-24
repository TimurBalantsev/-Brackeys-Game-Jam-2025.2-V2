using System;
using System.Collections.Generic;
using HitBox;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    [SerializeField] private GameObject attackHitBoxSourceObject;
    private LayerMask damageableLayerMask;
    private AttackHitBoxSource source;

    private List<Damageable> alreadyHitDamageables = new List<Damageable>();
    private List<Damageable> damageablesInRange = new List<Damageable>();

    [SerializeField] private bool allowMultipleHitsPerTarget = false;
    [SerializeField] private float multipleHitsDelay = 0.5f;
    private float currentHitTimer;
    private void Start()
    {
        if (!attackHitBoxSourceObject.TryGetComponent(out source))
            throw new ArgumentException($"{this} couldn't find AttackHitBoxSource on parent {attackHitBoxSourceObject}");
        currentHitTimer = multipleHitsDelay;
    }
    
    private void Update()
    {
        if (!allowMultipleHitsPerTarget) return;
        IncrementTimer();
        if (isAttackOnCooldown()) return;
        if (damageablesInRange.Count == 0) return;
        HandleMultipleHits();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.TryGetComponent(out Damageable target)) return;
        if (!allowMultipleHitsPerTarget)
        {
            Hit(target);
        }
        else if (allowMultipleHitsPerTarget && !damageablesInRange.Contains(target))
        {
            damageablesInRange.Add(target);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.transform.TryGetComponent(out Damageable target)) return;
        if (!allowMultipleHitsPerTarget) return;
        if (!damageablesInRange.Contains(target)) return;
        damageablesInRange.Remove(target);
    }

    public void InitializeLayerMasks(LayerMask layerMask)
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        if (colliders.Length == 0)
            throw new ArgumentException($"{this} has no collider to detect hits with");
        foreach (Collider2D collider2d in colliders)
        {
            collider2d.includeLayers = layerMask;
        }
    }

    private void Hit(Damageable target)
    {
        if (target == null)
        {
            Debug.LogError($"{this} tried to hit a null target");
            return;
        }
        
        if (!allowMultipleHitsPerTarget)
        {
            if (alreadyHitDamageables.Contains(target)) return;
            alreadyHitDamageables.Add(target);
        }
        
        source.DealDamage(target);
    }

    private bool isAttackOnCooldown()
    {
        return currentHitTimer < multipleHitsDelay;
    }

    public void ResetHitsEvent()
    {
        currentHitTimer = multipleHitsDelay;
        alreadyHitDamageables.Clear();
    }

    private void IncrementTimer()
    {
        if (isAttackOnCooldown())
        {
            currentHitTimer += Time.deltaTime;
        }
    }

    private void HandleMultipleHits()
    {
        foreach (Damageable target in damageablesInRange.ToArray()) // to array because unity is weird about modifying collections while iterating
        {
            Hit(target);
        }

        currentHitTimer = 0;
    }
}