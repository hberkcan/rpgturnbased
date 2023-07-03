using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "New JumpAttack", menuName = "Attacks/JumpAttack")]
public class JumpAttack : BaseAttackSkill
{
    public override IEnumerator AttackBehaviour(CharacterAttack attacker, Animator animator, CharacterHealth target)
    {
        Transform tr = attacker.transform;
        Vector2 startPos = tr.position;
        Vector2 targetPos = GetAttackPosition(target.transform);
        
        animator.SetTrigger("Attack");
        tr.transform.DOJump(targetPos, 10f, 1, attackDuration * 0.35f).OnComplete(() =>
        {
            tr.transform.DOMove(startPos, attackDuration * 0.15f).SetDelay(attackDuration * 0.5f);
        });

        yield return new WaitForSeconds(attackDuration);
    }

    protected override Vector2 GetAttackPosition(Transform target)
    {
        return target.transform.position + target.transform.right * 7.5f;
    }
}
