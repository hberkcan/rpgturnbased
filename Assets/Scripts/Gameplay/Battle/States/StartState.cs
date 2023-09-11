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
        float wait = 2f;
        battleManager.BattleHUD.StartCountdown(wait);
        yield return new WaitForSeconds(wait);

        if (battleManager.IsPlayerTurn)
            battleManager.SetState(BattleStateType.PlayerTurn);
        else
            battleManager.SetState(BattleStateType.EnemyTurn);
    }
}
