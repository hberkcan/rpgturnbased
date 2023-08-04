using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType { RandomTarget, AllTargets}
public abstract class BaseAttackSkill : ScriptableObject
{
    [SerializeField] protected TargetType targetType;
    public TargetType TargetType => targetType;
    [SerializeField] protected float eventMarker;
    public float EventMarker => eventMarker;
    [SerializeField] protected float attackDuration;
    [SerializeField] protected AnimationClip attackAnimationClip;
    public abstract IEnumerator AttackBehaviour(Transform attacker, Animator attackerAnimator, Transform target);

    public AnimationClip GetAnimationClip()
    {
        return attackAnimationClip;
    }

    protected abstract Vector2 GetAttackPosition(Transform target);
}
