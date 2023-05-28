using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterXP : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    private LevelSystem levelSystem;

    public event Action OnLevelUp;

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
            OnLevelUp?.Invoke();
        }
    }

    public void SetLevel(int level)
    {
        levelSystem.SetLevel(level);
    }
}
