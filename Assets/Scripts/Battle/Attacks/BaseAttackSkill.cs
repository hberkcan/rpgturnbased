using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttackSkill : ScriptableObject
{
    [SerializeField] protected float attackDuration;
    [SerializeField] protected AnimationClip attackAnimationClip;
    public abstract IEnumerator AttackBehaviour(CharacterAttack attacker, Animator attackerAnimator, CharacterHealth target);

    public AnimationClip GetAnimationClip()
    {
        return attackAnimationClip;
    }

    protected abstract Vector2 GetAttackPosition(Transform target);
}
