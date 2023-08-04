using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Data", menuName = "Unit Data")]
public class UnitDataSO : ScriptableObject
{
    public UnitController UnitPrefab;
    public GameObject UnitIcon;

    public Unit BaseData;
}
