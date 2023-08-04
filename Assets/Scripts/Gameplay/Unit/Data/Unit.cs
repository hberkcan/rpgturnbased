using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Unit
{
    public string Name;
    public Stat MaxHealth;
    public float CurrentHealth;
    public Stat AttackPower;
    public int Experience;
    public int Level;

    public Unit(string name, Stat maxHealth, float currentHealth, Stat attackPower, int experience, int level)
    {
        Name = name;
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
        AttackPower = attackPower;
        Experience = experience;
        Level = level;
    }
}
