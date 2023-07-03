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
        CharacterHealth target = battleManager.GetRandomPlayerCharacter().CharacterHealth;

        yield return battleManager.EnemyCharacter.CharacterAttack.AttackBehaviour(target);
        Debug.Log("Enemy Attacked");

        if (target.IsDead)
        {
            battleManager.SetState(BattleStateType.Lose);
        }
        else
        {
            battleManager.SetState(BattleStateType.Ready);
        }
    }
}
