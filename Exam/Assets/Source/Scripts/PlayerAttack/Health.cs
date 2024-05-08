using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable 
{

    public int health 
    {
        get
        {
            return _health;
        }

        set
        {
            _health = value;
            onHealthChange.Invoke(); 
        }
    }

    private int _health;
    public int maxHealth = 100;


    const int DEATH_HEALTH_AMOUNT = 0; 


    [Header("Events")] 
    [Space] 
    public UnityEvent onHealthChange; 
    public UnityEvent onHealthIncrease; 
    public UnityEvent onMaxHealthIncrease; 
    public UnityEvent onHealthDecrease; 
    public UnityEvent onDeath; 

    private void Start()
    {
        health = maxHealth;
    }

    public void RestoreHealth()
    {
        health = maxHealth; 
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount; 
        onMaxHealthIncrease.Invoke(); 
    }

    public void Hit(int damage)
    {
        DecreaseHealth(damage); 
    }

    void DecreaseHealth(int amount)
    {
        if (health <= 0) 
            return; 

        health -= amount; 
        CheckDeath(); 
        onHealthDecrease.Invoke(); 
        Debug.Log(gameObject.name + " health = " + health); 
    }

    void IncreaseHealth(int amount)
    {
        health += amount; 
        Mathf.Clamp(health, 0, maxHealth); 
        onHealthIncrease.Invoke(); 
    }

    void CheckDeath()
    {
        if (health <= DEATH_HEALTH_AMOUNT) 
        {
            onDeath.Invoke(); 
        }
    }


}
