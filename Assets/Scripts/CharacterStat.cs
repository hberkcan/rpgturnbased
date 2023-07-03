using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    [SerializeField] CharacterDataSO characterData;
    private Dictionary<StatType, Stat> stats;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        stats = new Dictionary<StatType, Stat>(characterData.BaseData.Stats.Length);

        foreach (var stat in characterData.BaseData.Stats)
        {
            stats.Add(stat.Type, stat);
        }
    }

    public Stat GetStat(StatType statType)
    {
        return stats[statType];
    }
}
