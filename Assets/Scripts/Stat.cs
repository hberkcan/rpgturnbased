using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType { Health, Attack}

[System.Serializable]
public class Stat
{
    public StatType Type;
    public float Value;
}
