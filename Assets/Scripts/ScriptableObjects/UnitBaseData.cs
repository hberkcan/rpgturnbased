using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Data", menuName = "Unit Data")]
public class UnitBaseData : ScriptableObject
{
    public UnitController UnitPrefab;
    public GameObject UnitIcon;

    public UnitData Data;
}
