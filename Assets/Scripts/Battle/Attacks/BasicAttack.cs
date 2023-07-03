using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "New BasicAttack", menuName = "Attacks/BasicAttack")]
public class BasicAttack : BaseAttackSkill
{
    public override IEnumerator AttackBehaviour(CharacterAttack attacker, Animator animator, CharacterHealth target)
    {
        Transform tr = attacker.transform;
        Vector2 startPos = tr.position;
        Vector2 targetPos = GetAttackPosition(target.transform);
        
        tr.transform.DOMove(targetPos, attackDuration * 0.15f).OnComplete(() =>
        {
            animator.SetTrigger("Attack");
            tr.transform.DOMove(startPos, attackDuration * 0.15f).SetDelay(attackDuration * 0.7f);
        });

        yield return new WaitForSeconds(attackDuration);
    }

    protected override Vector2 GetAttackPosition(Transform target)
    {
        return target.transform.position + target.transform.right * 5f;
    }
}
