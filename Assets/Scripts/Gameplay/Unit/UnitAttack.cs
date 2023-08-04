using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitAttack : MonoBehaviour
{
    [SerializeField] private BaseAttackSkill attackSkill;
    public readonly TargetType targetType;
    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;
    private AnimationEvent evt;

    public UnityEvent AttackHappened;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;
        animatorOverrideController["Rogue_attack_01"] = attackSkill.GetAnimationClip();
        animator.Rebind();

        evt = new AnimationEvent
        {
            time = attackSkill.EventMarker,
            functionName = "Hit"
        };

        AnimationClip clip = attackSkill.GetAnimationClip();
        if (clip.events.Length == 0)
            clip.AddEvent(evt);
    }

    public TargetType GetActiveAttackTargetType()
    {
        return attackSkill.TargetType;
    }

    public Coroutine AttackBehaviour(Transform target)
    {
        return StartCoroutine(attackSkill.AttackBehaviour(transform, animator, target));
    }

    public void ApplyDamage()
    {
        AttackHappened?.Invoke();
    }

    //public AnimationClip FindAnimation(Animator animator, string name)
    //{
    //    foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
    //    {
    //        if (clip.name == name)
    //        {
    //            return clip;
    //        }
    //    }

    //    return null;
    //}
}
