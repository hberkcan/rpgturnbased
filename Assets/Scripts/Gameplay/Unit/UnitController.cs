using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitController : MonoBehaviour
{
    private Unit data;
    private UnitAttack unitAttack;
    private UnitHealth unitHealth;
    private UnitStat unitStat;
    private UnitTargetBehaviour unityTargetBehaviour;
    private UnitAnimationBehaviour unitAnimationBehaviour;
    private List<UnitController> targetUnits;
    public Action<UnitController> Clicked;
    public event Action<UnitController> UnitDiedEvent;

    private void Awake()
    {
        unitAttack = GetComponent<UnitAttack>();
        unitHealth = GetComponent<UnitHealth>();
        unitStat = GetComponent<UnitStat>();
        unityTargetBehaviour = GetComponent<UnitTargetBehaviour>();
        unitAnimationBehaviour = GetComponent<UnitAnimationBehaviour>();
    }

    //UnityEvent
    public void OnClick()
    {
        Clicked?.Invoke(this);
    }

    //UnityEvent
    public void OnLongPress()
    {
        //show stats
    }

    public void Init(Unit unit)
    {
        data = unit;
        unitHealth.SetHealth(data.MaxHealth.GetValue(), data.CurrentHealth);
    }

    public void AssignTargetUnits(List<UnitController> units)
    {
        unityTargetBehaviour.SetTargets(units);
    }

    public void RemoveTargetUnit(UnitController unit)
    {
        unityTargetBehaviour.RemoveTarget(unit);
    }

    public Coroutine StartAttackSequence()
    {
        targetUnits = unityTargetBehaviour.FilterTargetUnits(unitAttack.GetActiveAttackTargetType());
        Transform target = targetUnits[0].transform;
        return unitAttack.AttackBehaviour(target);
    }

    //UnityEvent
    public void AttackHappened()
    {
        if (targetUnits.Count > 0)
        {
            for (int i = 0; i < targetUnits.Count; i++)
            {
                targetUnits[i].ReceiveAttackValue(data.AttackPower.GetValue());
            }
        }
    }

    public void ReceiveAttackValue(float attackPower)
    {
        unitHealth.TakeDamage(attackPower);
    }

    //UnityEvent
    public void UnitHasDied()
    {
        UnitDiedEvent?.Invoke(this);
    }

    public void AddXP(int amount)
    {
        //if (levelSystem.AddXP(amount))
        //{
        //    OnLevelUp?.Invoke(levelSystem.CurrentLevel);
        //}
    }

    public void FlipX()
    {
        transform.Rotate(0f, 180f, 0f);
    }
}
