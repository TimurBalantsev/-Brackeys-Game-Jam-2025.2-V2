using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        Player.Instance.OnDamage += Instance_OnDamage;
    }

    private void OnDestroy()
    {
        Player.Instance.OnDamage -= Instance_OnDamage;
    }

    private void Instance_OnDamage(EntityStats stats)
    {
        SetHealth(stats.currentHealth, stats.maxHealth);
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        slider.value = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
    }
}
