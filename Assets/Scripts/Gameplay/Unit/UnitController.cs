using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using MyMessagingSystem;

public class UnitController : MonoBehaviour, IClick, ILongPress, ILongPressEnd
{
    [SerializeField] private Unit data;
    private UnitAttack unitAttack;
    private UnitHealth unitHealth;
    private UnitTargetBehaviour unityTargetBehaviour;
    private UnitAnimationBehaviour unitAnimationBehaviour;
    private HealthDisplay healthDisplay;
    private List<UnitController> targetUnits;
    public Action<UnitController> Clicked;
    public event Action<UnitController> UnitDiedEvent;
    public UnityEvent<int> OnLevelUp;

    private void Awake()
    {
        unitAttack = GetComponent<UnitAttack>();
        unitHealth = GetComponent<UnitHealth>();
        unityTargetBehaviour = GetComponent<UnitTargetBehaviour>();
        unitAnimationBehaviour = GetComponent<UnitAnimationBehaviour>();
        healthDisplay = GetComponent<HealthDisplay>();
    }

    public void OnClick()
    {
        Clicked?.Invoke(this);
    }

    public void OnLongPress()
    {
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
        MessagingSystem.Instance.Dispatch(new StatDisplayEvent(data, pos));
    }

    public void OnLongPressEnd()
    {
        MessagingSystem.Instance.Dispatch(new HideStatDisplayEvent());
    }

    public void Init(Unit unit)
    {
        data = unit;

        if(data.CurrentHealth <= 0)
        {
            healthDisplay.SetActive(false);
            unitAnimationBehaviour.DieInstant();
        }

        unitHealth.SetHealth(data.MaxHealth.Value, data.CurrentHealth);
        healthDisplay.UpdateDisplay(unitHealth.GetPercentage());
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
                targetUnits[i].ReceiveAttackValue(data.AttackPower.Value);
            }
        }
    }

    public void ReceiveAttackValue(int attackPower)
    {
        unitHealth.TakeDamage(attackPower);
        data.CurrentHealth = unitHealth.CurrentHealth;
        healthDisplay.UpdateDisplay(unitHealth.GetPercentage());
    }

    //UnityEvent
    public void UnitHasDied()
    {
        UnitDiedEvent?.Invoke(this);
    }

    public void AddXP()
    {
        int xp = 2;
        int statIncrement = 10;

        if (data.AddXP(xp))
        {
            OnLevelUp?.Invoke(data.CurrentLevel);
            data.MaxHealth.IncrementValue(statIncrement);
            data.AttackPower.IncrementValue(statIncrement);
        }
    }

    public void FlipX()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    public UnitData GetData()
    {
        return data.GetData();
    }
}
