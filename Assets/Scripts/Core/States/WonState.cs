using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonState : BattleState
{
    public WonState(BattleManager battleManager) : base(battleManager) 
    {
    }

    public override IEnumerator Execute()
    {
        yield break;
    }
}
