using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "New BasicAttack", menuName = "Attacks/BasicAttack")]
public class BasicAttack : BaseAttackSkill
{
    public override IEnumerator AttackBehaviour(Transform attacker, Animator animator, Transform target)
    {
        Vector2 startPos = attacker.position;
        Vector2 targetPos = GetAttackPosition(target);
        
        attacker.transform.DOMove(targetPos, attackDuration * 0.15f).OnComplete(() =>
        {
            animator.SetTrigger("Attack");
            attacker.transform.DOMove(startPos, attackDuration * 0.15f).SetDelay(attackDuration * 0.7f);
        });

        yield return new WaitForSeconds(attackDuration);
    }

    protected override Vector2 GetAttackPosition(Transform target)
    {
        return target.position + target.right * 5f;
    }
}
