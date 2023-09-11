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
        battleManager.IsPlayerTurn = true;

        while (battleManager.WaitingForInput)
            yield return null;

        yield return battleManager.ClickedUnit.StartAttackSequence();

        battleManager.WaitingForInput = true;

        if (battleManager.IsBattleOn)
        {
            battleManager.SetState(BattleStateType.EnemyTurn);
        }
    }
}
