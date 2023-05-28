using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationState { Idle, Run, Attack, Hit, Die}
public class AnimationHandler : MonoBehaviour
{
    private Animator animator;

    private readonly int Idle = Animator.StringToHash("Idle");
    private readonly int Run = Animator.StringToHash("Run");
    private readonly int Attack = Animator.StringToHash("Attack");
    private readonly int Hit = Animator.StringToHash("Hit");
    private readonly int Die = Animator.StringToHash("Die");

    private Dictionary<AnimationState, int> animationStates;

    private readonly float transitionDuration = 0.25f;
    private readonly int layer = 0;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        animationStates = new Dictionary<AnimationState, int>(5)
        {
            { AnimationState.Idle, Idle },
            { AnimationState.Run, Run },
            { AnimationState.Attack, Attack },
            { AnimationState.Hit, Hit },
            { AnimationState.Die, Die }
        };
    }

    public void SetAnimationState(AnimationState animationState)
    {
        animator.CrossFade(animationStates[animationState], transitionDuration, layer);
    }
}
