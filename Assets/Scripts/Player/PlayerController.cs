using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Alteruna;
using System.Security.Cryptography;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;

    private Vector2 movement;
    private Alteruna.Avatar avatar;
    private Vector2 lastMoveDirection;
    private bool isAttacking = false;

    
    private void Start()
    {
        anim = GetComponent<Animator>();
        avatar = GetComponent<Alteruna.Avatar>();

        if (!avatar.IsMe)
        {
            return;
        }
    }

    void Update()
    {
        if (!avatar.IsMe)
        {
            return;
        }

        ProcessInputs();
        HandleAttacks();
        Animate();
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }

    // Player Controls
    void ProcessInputs()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movement.x = horizontal;
        movement.y = vertical;

        if (movement != Vector2.zero)
        {
            lastMoveDirection = movement;
        }

        movement.Normalize();
    }

    void Animate()
    {
        if (isAttacking) return;

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

    void HandleAttacks()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }
        
    IEnumerator Attack()
    {
        isAttacking = true;

        if (movement.magnitude > 0)
        {
            // Player is moving, use current movement direction for attack
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                if (movement.x > 0)
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
                if (movement.y > 0)
                {
                    SetAnimation("AttackUp");
                }
                else
                {
                    SetAnimation("AttackDown");
                }
            }
        }
        else
        {
            // Player not moving, use last move direction for attack
            if (Mathf.Abs(lastMoveDirection.x) > Mathf.Abs(lastMoveDirection.y))
            {
                // Last moved horizontally
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

        // Waits for 0.2 seconds
        yield return new WaitForSeconds(0.2f);

        isAttacking = false;
    }

    public Vector2 GetLastMoveDirection()
    {
        return lastMoveDirection;
    }

    public Vector2 GetCurrentMoveDirection()
    {
        return movement;
    }

    public bool IsMoving()
    {
        return movement != Vector2.zero;
    }

    public string GetFacingDirection()
    {
        if (Mathf.Abs(lastMoveDirection.x) > Mathf.Abs(lastMoveDirection.y))
        {
            if (lastMoveDirection.x > 0)
            {
                return "Right";
            }
            else
            {
                return "Left";
            }
        }
        else
        {
            if (lastMoveDirection.y > 0)
            {
                return "Up";
            }
            else
            {
                return "Down";
            }
        }
    }

    public void LogFacingDirection()
    {
        string direction = GetFacingDirection();
        Debug.LogWarning("Player is facing: " + direction);
    }
}
