using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "New BackstabAttack", menuName = "Attacks/BackstabAttack")]
public class BackstabAttack : BaseAttackSkill
{
    public override IEnumerator AttackBehaviour(CharacterAttack attacker, CharacterHealth target)
    {
        Animator animator = attacker.GetComponentInChildren<Animator>();
        Transform tr = attacker.transform;
        Vector2 startPos = tr.position;
        Vector2 targetPos = target.transform.position + target.transform.right * -7.5f;

        Sequence mySequence = DOTween.Sequence();

        //animator.SetBool("IsMoving", true);

        //mySequence.Append(tr.transform.DOMoveX(targetPos, attackDuration * 0.4f))
        //    .AppendCallback(() =>
        //    {
        //        animator.SetTrigger("Attack");
        //        target.TakeDamage(attacker.damage);
        //    })
        //    .Append(tr.transform.DOMoveX(startPos, attackDuration * 0.4f))
        //    .AppendCallback(() =>
        //    {
        //        animator.SetBool("IsMoving", false);
        //    });

        //animator.SetBool("IsMoving", true);
        tr.transform.DOMove(targetPos, attackDuration * 0.15f).OnComplete(() =>
        {
            attacker.transform.Rotate(0f, 180f, 0f);
            animator.SetTrigger("Attack");
            //target.TakeDamage(attacker.damage);

            tr.transform.DOMove(startPos, attackDuration * 0.15f).SetDelay(attackDuration * 0.7f).OnComplete(() =>
            {
                //animator.SetBool("IsMoving", false);
                attacker.transform.Rotate(0f, 180f, 0f);
            });
        });

        yield return new WaitForSeconds(attackDuration);
    }
}