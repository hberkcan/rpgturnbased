using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Variables<T> : ScriptableObject where T : IEquatable<T>
{
    [SerializeField] private T value;
    public T Value 
    { 
        get { return value; }
        
        set 
        {
            if (EqualityComparer<T>.Default.Equals(value, this.value))
                return;

            this.value = value;
            OnValueChanged?.Invoke(this.value);
        } 
    }

    public event Action<T> OnValueChanged;
}
