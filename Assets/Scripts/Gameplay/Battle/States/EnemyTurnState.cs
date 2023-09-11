using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : BattleState
{
    public EnemyTurnState(BattleManager battleManager) : base(battleManager) 
    {
    }

    public override IEnumerator Execute()
    {
        battleManager.IsPlayerTurn = false;

        yield return battleManager.GetRandomEnemyUnit().StartAttackSequence();

        if (battleManager.IsBattleOn)
            battleManager.SetState(BattleStateType.PlayerTurn);
    }
}
