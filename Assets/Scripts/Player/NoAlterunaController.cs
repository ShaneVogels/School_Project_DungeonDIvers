using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoAlterunaController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float attackDuration = 0.5f;  // Duration of attack animation

    private Vector2 movement;
    private Vector2 lastMoveDirection;
    private bool isAttacking = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isAttacking)
        {
            ProcessInputs();
            Animate();
        }
    }

    void FixedUpdate()
    {
        if (!isAttacking)
        {
            rb.velocity = movement * moveSpeed;
        }
    }

    // Player Controls
    void ProcessInputs()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal == 0 && vertical == 0 && (movement.x != 0 || movement.y != 0))
        {
            lastMoveDirection = movement;
        }

        movement.x = horizontal;
        movement.y = vertical;

        movement.Normalize();
    }

    // Animation Controls
    void Animate()
    {
        if (movement.magnitude > 0)
        {
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                if (movement.x > 0)
                {
                    SetAnimation("RunRight");
                }
                else
                {
                    SetAnimation("RunLeft");
                }
            }
            else
            {
                if (movement.y > 0)
                {
                    SetAnimation("RunUp");
                }
                else
                {
                    SetAnimation("RunDown");
                }
            }
        }
        else
        {
            if (Mathf.Abs(lastMoveDirection.x) > Mathf.Abs(lastMoveDirection.y))
            {
                if (lastMoveDirection.x > 0)
                {
                    SetAnimation("IdleRight");
                }
                else
                {
                    SetAnimation("IdleLeft");
                }
            }
            else
            {
                if (lastMoveDirection.y > 0)
                {
                    SetAnimation("IdleUp");
                }
                else
                {
                    SetAnimation("IdleDown");
                }
            }
        }
    }

    void SetAnimation(string animation)
    {
        anim.ResetTrigger("IdleUp");
        anim.ResetTrigger("IdleDown");
        anim.ResetTrigger("IdleLeft");
        anim.ResetTrigger("IdleRight");
        anim.ResetTrigger("RunUp");
        anim.ResetTrigger("RunDown");
        anim.ResetTrigger("RunLeft");
        anim.ResetTrigger("RunRight");
        anim.ResetTrigger("AttackUp");
        anim.ResetTrigger("AttackDown");
        anim.ResetTrigger("AttackLeft");
        anim.ResetTrigger("AttackRight");

        anim.SetTrigger(animation);
    }

    public void StartAttack()
    {
        isAttacking = true;

        if (Mathf.Abs(lastMoveDirection.x) > Mathf.Abs(lastMoveDirection.y))
        {
            if (lastMoveDirection.x > 0)
            {
                SetAnimation("AttackRight");
            }
            else
            {
                SetAnimation("AttackLeft");
            }
        }
        else
        {
            if (lastMoveDirection.y > 0)
            {
                SetAnimation("AttackUp");
            }
            else
            {
                SetAnimation("AttackDown");
            }
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    public Vector2 GetLastMoveDirection()
    {
        return lastMoveDirection;
    }

    public float GetAttackDuration()
    {
        return attackDuration;
    }
}
