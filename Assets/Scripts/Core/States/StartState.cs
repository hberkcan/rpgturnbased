using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : BattleState
{
    public StartState(BattleManager battleManager) : base(battleManager)
    {
    }

    public override IEnumerator Execute()
    {
        Debug.Log("Battle Started");
        yield return new WaitForSeconds(2f);
        battleManager.SetState(BattleStateType.Ready);
    }
}
