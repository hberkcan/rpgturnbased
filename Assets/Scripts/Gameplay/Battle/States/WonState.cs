using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMessagingSystem;

public class WonState : BattleState
{
    public WonState(BattleManager battleManager) : base(battleManager) 
    {
    }

    public override IEnumerator Execute()
    {
        yield return new WaitForSeconds(1f);
        battleManager.BattleHUD.BattleWon();
        MessagingSystem.Instance.Dispatch(new BattleWonEvent());
    }
}
