using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "New JumpAttack", menuName = "Attacks/JumpAttack")]
public class JumpAttack : BaseAttackSkill
{
    public override IEnumerator AttackBehaviour(Transform attacker, Animator animator, Transform target)
    {
        Vector2 startPos = attacker.position;
        Vector2 targetPos = GetAttackPosition(target);
        
        animator.SetTrigger("Attack");
        attacker.transform.DOJump(targetPos, 10f, 1, attackDuration * 0.35f).OnComplete(() =>
        {
            attacker.transform.DOMove(startPos, attackDuration * 0.15f).SetDelay(attackDuration * 0.5f);
        });

        yield return new WaitForSeconds(attackDuration);
    }

    protected override Vector2 GetAttackPosition(Transform target)
    {
        return target.position + target.right * 7.5f;
    }
}
