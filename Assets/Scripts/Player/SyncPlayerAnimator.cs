using UnityEngine;
using Alteruna;

public class SyncPlayerAnimator : AttributesSync
{
    private Animator animator;
    private Alteruna.Avatar avatar;

    private void Start()
    {
        animator = GetComponent<Animator>();
        avatar = GetComponent<Alteruna.Avatar>();

        if (!avatar.IsMe)
        {
            enabled = false;
        }
    }

    private void SetAnimation(string animation)
    {
        // Reset all triggers
        animator.ResetTrigger("IdleUp");
        animator.ResetTrigger("IdleDown");
        animator.ResetTrigger("IdleLeft");
        animator.ResetTrigger("IdleRight");
        animator.ResetTrigger("RunUp");
        animator.ResetTrigger("RunDown");
        animator.ResetTrigger("RunLeft");
        animator.ResetTrigger("RunRight");
        animator.ResetTrigger("AttackUp");
        animator.ResetTrigger("AttackDown");
        animator.ResetTrigger("AttackLeft");
        animator.ResetTrigger("AttackRight");

        // Set the current animation trigger
        animator.SetTrigger(animation);
    }

    [SynchronizableMethod]
    public void SyncSetAnimation(string animation)
    {
        SetAnimation(animation);
    }

    [SynchronizableMethod]
    public void SyncSetAttacking(bool isAttacking)
    {
        animator.SetBool("isAttacking", isAttacking);
    }
}
