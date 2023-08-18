using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitData
{
    public string Name;
    public int MaxHealth;
    /*[HideInInspector] */public int CurrentHealth;
    public int AttackPower;
    [HideInInspector] public int Experience;

    public UnitData(string name, int maxHealth, int currentHealth, int attackPower, int experience)
    {
        Name = name;
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
        AttackPower = attackPower;
        Experience = experience;
    }
}
