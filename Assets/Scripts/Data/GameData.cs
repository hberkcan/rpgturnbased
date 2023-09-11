using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public UnitData[] UnitDatas;
    public List<string> SelectedUnits;
    public bool IsBattleActive;
    public int BattleCount;
}
