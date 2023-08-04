using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitHealth : MonoBehaviour
{
    private float maxHealth;
    private float currentHealth;
    public UnityEvent<float> OnTakeDamage;
    public UnityEvent OnDie;

    public void SetHealth(float maxHealth, float currentHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
    }

    public void TakeDamage(float damage)
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
        return currentHealth / maxHealth;
    }
}
