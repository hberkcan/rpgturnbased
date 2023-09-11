using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyUnitCreator : MonoBehaviour
{
    [SerializeField] private List<UnitBaseData> enemyUnits;
    [SerializeField] private AnimationCurve healthScaling;
    [SerializeField] private AnimationCurve attackScaling;

    public Unit GetEnemyUnit()
    {
        Unit unit = new Unit(enemyUnits[0].Data);
        UnitData data = unit.GetData();
        int maxHealth = 140;
        int attackPower = 35;
        int level = GameDataManager.Instance.GetUnitDatas().OrderByDescending(unit => unit.CurrentLevel).FirstOrDefault().CurrentLevel;
        unit.SetData(new UnitData("Enemy",
            data.UnitType,
            maxHealth * (int)healthScaling.Evaluate(level - 1),
            maxHealth * (int)healthScaling.Evaluate(level - 1),
            attackPower * (int)attackScaling.Evaluate(level - 1)
            ));

        return unit;
    }

    public Unit GetEnemyUnit(UnitData data)
    {
        Unit unit = new Unit(data);
        return unit;
    }
}
