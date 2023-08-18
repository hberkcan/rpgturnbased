using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationBehaviour : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public  void TakeDamage()
    {
        animator.SetTrigger("Hit");
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }

    public void DieInstant()
    {
        animator.Play("Rogue_death_01", -1, 1);
    }
}
