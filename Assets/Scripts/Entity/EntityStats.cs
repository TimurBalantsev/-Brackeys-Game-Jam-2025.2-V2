using System;

[Serializable]
public class EntityStats
{
    //TODO: move damage out of here.
    public float maxHealth;
    private float currentHealth;
    public float damage;
    public float speed;
    public HealthBarUI healthBarUI;
    
    public EntityStats(float maxHealth, float damage, float speed)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.damage = damage;
        this.speed = speed;
    }

    public void Initialize()
    {
        currentHealth = maxHealth;
    }
    
    public bool TakeDamage(float damage)
    {
        if (damage <= 0) return false;
        
        currentHealth -= damage;
        healthBarUI?.SetHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            return true;
        }

        return false;
    }
    
    public void Heal(float heal)
    {
        if (heal <= 0) return;
        
        currentHealth += heal;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBarUI.SetHealth(currentHealth, maxHealth);
    }
}
