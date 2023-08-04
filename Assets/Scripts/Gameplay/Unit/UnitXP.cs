using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitXP : MonoBehaviour
{
    private LevelSystem levelSystem;

    public UnityEvent<int> OnLevelUp;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        levelSystem = new LevelSystem();
    }

    public void AddXP(int amount)
    {
        if (levelSystem.AddXP(amount))
        {
            OnLevelUp?.Invoke(levelSystem.CurrentLevel);
        }
    }

    public void SetLevel(int level)
    {
        levelSystem.SetLevel(level);
    }
}
