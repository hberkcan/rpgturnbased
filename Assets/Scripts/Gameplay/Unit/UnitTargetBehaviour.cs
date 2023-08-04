using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTargetBehaviour : MonoBehaviour
{
    private List<UnitController> targetUnits = new();

    public void SetTargets(List<UnitController> targetUnits)
    {
        this.targetUnits = targetUnits;
    }

    public void RemoveTarget(UnitController removedUnit)
    {
        targetUnits.Remove(removedUnit);
    }

    public List<UnitController> FilterTargetUnits(TargetType targetType)
    {

        List<UnitController> filteredUnits = new List<UnitController>();

        if (targetUnits.Count <= 0)
        {
            return filteredUnits;
        }

        switch (targetType)
        {
            case TargetType.RandomTarget:
                filteredUnits.Add(GetRandomTargetUnit());
                break;

            case TargetType.AllTargets:
                filteredUnits = GetAllTargetUnits();
                break;

        }

        return filteredUnits;

    }

    private UnitController GetRandomTargetUnit()
    {
        int randomUnit = Random.Range(0, targetUnits.Count);
        return targetUnits[randomUnit];
    }

    private List<UnitController> GetAllTargetUnits()
    {
        return targetUnits;
    }
}
