using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitData
{
    public string Name;
    public string UnitType;
    public int MaxHealth;
    [HideInInspector] public int CurrentHealth;
    public int AttackPower;
    [HideInInspector] public int Experience;
    public bool IsUnlocked;

    public UnitData(string name, string unitType, int maxHealth, int currentHealth, int attackPower, int experience = 0, bool isUnlocked = true)
    {
        Name = name;
        UnitType = unitType;
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
        AttackPower = attackPower;
        Experience = experience;
        IsUnlocked = isUnlocked;
    }
}
