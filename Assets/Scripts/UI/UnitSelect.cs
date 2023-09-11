using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using MyMessagingSystem;

public class UnitSelect : MonoBehaviour, IClick, ILongPress, ILongPressEnd
{
    public Unit Unit { get; private set; }
    private bool isSelected;
    public bool IsSelected => isSelected;
    public static event Action<UnitSelect> OnAnyUnitSelectClicked;
    private SelectableUIBehaviour behaviour;

    private void Awake()
    {
        behaviour = GetComponent<SelectableUIBehaviour>();
    }

    public void Init(Unit unit)
    {
        Unit = unit;

        if (Unit.IsUnlocked)
            behaviour.Unlock();
        else
            behaviour.Lock();
    }

    public void Select()
    {
        isSelected = true;
        behaviour.OnSelect();
        
    }

    public void Deselect()
    {
        isSelected = false;
        behaviour.OnDeselect();
    }

    public void OnClick()
    {
        OnAnyUnitSelectClicked?.Invoke(this);
    }

    public void OnLongPress()
    {
        MessagingSystem.Instance.Dispatch(new StatDisplayEvent(Unit, transform.position));
    }

    public void OnLongPressEnd()
    {
        MessagingSystem.Instance.Dispatch(new HideStatDisplayEvent());
    }
}
