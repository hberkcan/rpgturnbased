using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyState : BattleState
{
    public ReadyState(BattleManager battleManager) : base(battleManager)
    {
    }

    public override IEnumerator Execute()
    {
        Debug.Log("Ready!..Waiting for Input");
        yield break;
    }
}
