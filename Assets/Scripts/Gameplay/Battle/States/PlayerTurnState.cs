using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : BattleState
{
    public PlayerTurnState(BattleManager battleManager) : base(battleManager)
    {
    }

    public override IEnumerator Execute()
    {
        yield return battleManager.ClickedUnit.StartAttackSequence();

        if (battleManager.IsBattleOn)
            battleManager.SetState(BattleStateType.EnemyTurn);
    }
}
