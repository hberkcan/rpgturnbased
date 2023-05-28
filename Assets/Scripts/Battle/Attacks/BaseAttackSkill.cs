using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseAttackSkill : ScriptableObject, IAttack
{
    [SerializeField] protected float attackDuration;
    public AnimationClip attackAnimationClip;
    public abstract IEnumerator AttackBehaviour(CharacterAttack attacker, CharacterHealth target);
}
