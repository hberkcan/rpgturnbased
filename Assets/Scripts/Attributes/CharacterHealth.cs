using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterHealth : MonoBehaviour
{
    private float health;

    public event Action OnTakeDamage;
    public event Action OnDie;

    public bool IsDead { get; private set; }

    private CharacterStat characterStat;

    private void Awake()
    {
        characterStat = GetComponent<CharacterStat>();
    }

    private void Start()
    {
        health = characterStat.GetStat(StatType.Health).Value;
    }

    public bool TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Max(health, 0);

        if (health == 0)
        {
            OnDie?.Invoke();
            IsDead = true;
            GetComponentInChildren<Animator>().SetTrigger("Die");
            return true;
        }
        else
        {
            OnTakeDamage?.Invoke();
            IsDead = false;
            GetComponentInChildren<Animator>().SetTrigger("Hit");
            return false;
        }
    }

    public float GetPercentage()
    {
        return health / characterStat.GetStat(StatType.Health).Value;
    }
}
