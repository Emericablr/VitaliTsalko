using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider healthBar; 
    Health health; 

    private void Start()
    {
        health = GetComponent<Health>();

        health.onHealthChange.AddListener(UpdateUI);
        health.onMaxHealthIncrease.AddListener(UpdateMaxHealth);

        UpdateMaxHealth();
        UpdateUI();
    }

    void UpdateUI()
    {
        healthBar.value = health.health;
    }

    void UpdateMaxHealth()
    {
        healthBar.maxValue = health.maxHealth;
    }
}
