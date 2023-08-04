using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleState
{
    protected BattleManager battleManager;

    public BattleState(BattleManager battleManager)
    {
        this.battleManager = battleManager;
    }

    public abstract IEnumerator Execute();
}
