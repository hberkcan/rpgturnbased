using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : BattleState
{
    public LoseState(BattleManager battleManager) : base(battleManager)
    {
    }

    public override IEnumerator Execute()
    {
        yield break;
    }
}
