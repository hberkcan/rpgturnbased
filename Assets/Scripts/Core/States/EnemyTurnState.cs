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
        CharacterAttack attacker = battleManager.EnemyGO.GetComponent<CharacterAttack>();
        CharacterHealth target = battleManager.GetRandomPlayerCharacter().GetComponent<CharacterHealth>();

        yield return attacker.AttackBehaviour(target);
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
