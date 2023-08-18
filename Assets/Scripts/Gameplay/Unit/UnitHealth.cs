using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitHealth : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;
    public int CurrentHealth => currentHealth;
    public UnityEvent<int> OnTakeDamage;
    public UnityEvent OnDie;

    public void SetHealth(int maxHealth, int currentHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (currentHealth == 0)
        {
            OnDie?.Invoke();
        }

        OnTakeDamage?.Invoke(-damage);
    }

    public float GetPercentage()
    {
        return (float)currentHealth / maxHealth;
    }
}
