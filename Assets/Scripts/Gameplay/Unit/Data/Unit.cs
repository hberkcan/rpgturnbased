using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Unit
{
    private UnitBaseData baseData;
    [SerializeField]private UnitData currentData;
    private LevelSystem levelSystem;

    public string Name => currentData.Name;
    public Stat MaxHealth { get; private set; }
    public int CurrentHealth 
    { 
        get { return currentData.CurrentHealth; } 
        set 
        { 
            currentData.CurrentHealth = value;
            if (Name == "Rogue6")
                Debug.Log(currentData.CurrentHealth);
        } 
    }
    public Stat AttackPower { get; private set; }
    public int Experience => levelSystem.CurrentExperience;
    public int CurrentLevel => levelSystem.CurrentLevel;
    public UnitController UnitPrefab => baseData.UnitPrefab;
    public GameObject UnitIcon => baseData.UnitIcon;

    public Unit(UnitBaseData baseData)
    {

        this.baseData = baseData;
        currentData = new UnitData(baseData.Data.Name, baseData.Data.MaxHealth, baseData.Data.MaxHealth, baseData.Data.AttackPower, baseData.Data.Experience);
        MaxHealth = new Stat(currentData.MaxHealth);
        AttackPower = new Stat(currentData.AttackPower);
        levelSystem = new LevelSystem();
        levelSystem.AddXP(currentData.Experience);
    }

    public void SetData(UnitData savedData)
    {
        currentData = new UnitData(savedData.Name, savedData.MaxHealth, savedData.CurrentHealth, savedData.AttackPower, savedData.Experience);
        MaxHealth = new Stat(currentData.MaxHealth);
        AttackPower = new Stat(currentData.AttackPower);
        levelSystem.Reset();
        levelSystem.AddXP(currentData.Experience);
    }

    public UnitData GetData()
    {
        return new UnitData(currentData.Name, MaxHealth.Value, currentData.CurrentHealth, AttackPower.Value, levelSystem.CurrentExperience);
    }

    public bool AddXP(int amount)
    {
        MaxHealth.IncrementValue(10);
        AttackPower.IncrementValue(10);
        return levelSystem.AddXP(amount);
    }
}
