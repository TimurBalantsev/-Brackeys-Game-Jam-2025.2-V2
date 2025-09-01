using System;

[Serializable]
public class EntityStats
{
    //TODO: move damage out of here.
    public float maxHealth;
    public float currentHealth;
    public float damage;
    public float speed;
    public OldHealthBarUI healthBarUI;
    
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

        if (healthBarUI != null) healthBarUI.SetHealth(this.currentHealth, this.maxHealth);
    }
    
    public bool TakeDamage(float damage)
    {
        if (damage <= 0) return false;
        
        currentHealth -= damage;

        if (healthBarUI != null) healthBarUI.SetHealth(this.currentHealth, this.maxHealth);

        if (currentHealth <= 0)
        {
            return true;
        }

        return false;
    }

    public void SetMaxHealth(float newMaxHealth)
    {
        float normalizedHealth = currentHealth / maxHealth;
        this.maxHealth = newMaxHealth;
        currentHealth = maxHealth * normalizedHealth;

        if (healthBarUI != null) healthBarUI.SetHealth(this.currentHealth, this.maxHealth);
    }
    
    public void Heal(float heal)
    {
        if (heal <= 0) return;
        
        currentHealth += heal;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (healthBarUI != null) healthBarUI.SetHealth(this.currentHealth, this.maxHealth);
    }
}
