using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    [SerializeField] CharacterData characterData;
    private Dictionary<StatType, Stat> stats;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        stats = new Dictionary<StatType, Stat>(characterData.character.Stats.Length);

        foreach (var stat in characterData.character.Stats)
        {
            stats.Add(stat.Type, stat);
        }
    }

    public Stat GetStat(StatType statType)
    {
        return stats[statType];
    }
}
