using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Unit
{
    public string Name { get; private set; }
    public string UnitType { get; private set; }
    public Stat MaxHealth { get; private set; }
    public int CurrentHealth { get; set; }
    public Stat AttackPower { get; private set; }
    private LevelSystem levelSystem;
    public int Experience => levelSystem.CurrentExperience;
    public double XpPercentage => levelSystem.XPPercentage;
    public int CurrentLevel => levelSystem.CurrentLevel;
    public bool IsAlive => CurrentHealth > 0;
    public bool IsUnlocked { get; set; }

    public Unit(UnitData data)
    {
        Name = data.Name;
        UnitType = data.UnitType;
        MaxHealth = new Stat(data.MaxHealth);
        CurrentHealth = data.CurrentHealth;
        AttackPower = new Stat(data.AttackPower);
        levelSystem = new LevelSystem();
        levelSystem.AddXP(data.Experience);
        IsUnlocked = data.IsUnlocked;
    }

    public UnitController CreateUnitObject(Vector3 position, Quaternion rotation)
    {
        string path = $"Units/{UnitType}";
        UnitController unitController = UnityEngine.Object.Instantiate(Resources.Load<UnitController>(path), position, rotation);
        unitController.Init(this);
        return unitController;
    }

    public GameObject GetUnitIcon(Transform parent)
    {
        string path = $"Units/Icons/{UnitType}_Icon";
        GameObject icon = UnityEngine.Object.Instantiate(Resources.Load<GameObject>(path), parent);
        return icon;
    }

    public void SetData(UnitData newData)
    {
        Name = newData.Name;
        MaxHealth = new Stat(newData.MaxHealth);
        CurrentHealth = newData.CurrentHealth;
        AttackPower = new Stat(newData.AttackPower);
        levelSystem.Reset();
        levelSystem.AddXP(newData.Experience);
        IsUnlocked = newData.IsUnlocked;
    }

    public UnitData GetData()
    {
        return new UnitData(Name, UnitType, MaxHealth.Value, CurrentHealth, AttackPower.Value, levelSystem.CurrentExperience, IsUnlocked);
    }

    public bool AddXP(int amount)
    {
        return levelSystem.AddXP(amount);
    }

    public void ResetHealth()
    {
        CurrentHealth = MaxHealth.Value;
    }
}
