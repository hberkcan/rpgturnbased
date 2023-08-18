using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMessagingSystem;

public class LoseState : BattleState
{
    public LoseState(BattleManager battleManager) : base(battleManager)
    {
    }

    public override IEnumerator Execute()
    {
        yield return new WaitForSeconds(1f);
        battleManager.BattleHUD.BattleLost();
    }
}
