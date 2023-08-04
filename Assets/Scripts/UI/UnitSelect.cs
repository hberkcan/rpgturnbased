using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class UnitSelect : MonoBehaviour
{
    public Unit Unit { get; private set; }
    private bool isSelected;
    
    public static event Action<UnitSelect> OnAnyUnitSelected;
    public static event Action<UnitSelect> OnAnyUnitDeselected;
    public UnityEvent SelectEvent;
    public UnityEvent DeselectEvent;

    public void Init(Unit unit)
    {
        Unit = unit;
    }

    public void OnClick()
    {
        isSelected = !isSelected;

        if (isSelected)
        {
            OnAnyUnitSelected?.Invoke(this);
        }
        else
        {
            OnAnyUnitDeselected?.Invoke(this);
        }
    }

    public void Select()
    {
        isSelected = true;
        SelectEvent?.Invoke();
        
    }

    public void Deselect()
    {
        isSelected = false;
        DeselectEvent?.Invoke();
    }

    public void OnLongPress()
    {
        //show stats
    }
}
