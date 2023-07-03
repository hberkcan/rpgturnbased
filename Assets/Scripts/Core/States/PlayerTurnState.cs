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
        CharacterHealth target = battleManager.EnemyCharacter.CharacterHealth;

        yield return battleManager.ClickedCharacter.CharacterAttack.AttackBehaviour(target);
        Debug.Log("Player Attacked");

        if (target.IsDead)
        {
            battleManager.SetState(BattleStateType.Won);
        }
        else
        {
            battleManager.SetState(BattleStateType.EnemyTurn);
        }
    }
}
