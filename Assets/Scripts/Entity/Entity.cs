using HitBox;
using UnityEngine;
namespace Entity
{
    public abstract class Entity : MonoBehaviour, AttackHitBoxSource, Damageable
    {
        [SerializeField] public EntityStats stats;

        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] private bool isSpriteFacingRight = true;
        [SerializeField] protected EntityColorFlashHelper colorFlashHelper;

        private bool isDead = false;

        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public bool IsDead => isDead;

        protected void HandleFlip(Vector3 targetPosition)
        {
            Vector2 delta = targetPosition - transform.position;
            //ts confusing af : check is target is on the right or left then check if your sprite is facing there by default to handle flip.
            if (delta.x > 0) //right
            {
                spriteRenderer.flipX = !isSpriteFacingRight;
            }
            else if (delta.x < 0) //left
            {
                spriteRenderer.flipX = isSpriteFacingRight;
            }
        }


        public Transform GetTransform()
        {
            return transform;
        }

        public float GetDamage()
        {
            return stats.damage;
        }

        public virtual void TakeDamage(float damage)
        {
            if (isDead)
            {
                return;
            }

            colorFlashHelper.FlashColor();
            if (stats.TakeDamage(damage))
            {
                Die();
            }
        }

        public void Heal(float heal)
        {
            stats.Heal(heal);
        }

        public void DealDamage(Damageable target)
        {
            target?.TakeDamage(stats.damage);
        }

        protected virtual void Die()
        {
            isDead = true;
        }
    }
};