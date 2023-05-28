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
        CharacterAttack attacker = battleManager.ClickedHero.GetComponentInChildren<CharacterAttack>();
        CharacterHealth target = battleManager.EnemyGO.GetComponent<CharacterHealth>();

        yield return attacker.AttackBehaviour(target);
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
