using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private BaseAttackSkill attackSkill;
    private CharacterHealth target;

    private CharacterStat characterStat;
    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;
    

    private void Awake()
    {
        characterStat = GetComponent<CharacterStat>();
        animator = GetComponentInChildren<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;

        animatorOverrideController["Rogue_attack_01"] = attackSkill.attackAnimationClip;
        animator.Rebind();
    }

    public Coroutine AttackBehaviour(CharacterHealth target)
    {
        this.target = target;
        return StartCoroutine(attackSkill.AttackBehaviour(this, target));
    }

    public void Hit()
    {
        target.TakeDamage(characterStat.GetStat(StatType.Attack).Value);
    }
}
