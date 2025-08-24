using HitBox;
using UnityEngine;

public interface AttackHitBoxSource
{
    public float GetDamage();
    public Transform GetTransform();
    public void DealDamage(Damageable target);
}