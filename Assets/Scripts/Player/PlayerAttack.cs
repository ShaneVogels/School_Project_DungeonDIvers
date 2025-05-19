using UnityEngine;
using Alteruna;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 10f;
    public int damageAmount = 20;
    public LayerMask slimeLayer;
    public LayerMask goblinLayer;

    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController script not found on the player.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            playerController.LogFacingDirection(); // Log the facing direction
            Attack();
        }
    }

    void Attack()
    {
        Vector2 attackDirection = GetAttackDirection();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, attackDirection, attackRange, slimeLayer | goblinLayer);

        // Visualize the raycast
        Debug.DrawRay(transform.position, attackDirection * attackRange, Color.red, 1f);

        if (hit.collider != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, hit.collider.transform.position);

            if (distanceToTarget <= attackRange)
            {
                // Calculate direction to the target
                Vector2 directionToTarget = (hit.collider.transform.position - transform.position).normalized;

                // Check if the target is within a reasonable angle of the attack direction
                float angle = Vector2.Angle(attackDirection, directionToTarget);

                if (angle < 45f) // Adjust the angle threshold as needed
                {
                    if (hit.collider.CompareTag("Slime"))
                    {
                        SlimeAI slimeAI = hit.collider.GetComponent<SlimeAI>();
                        if (slimeAI != null)
                        {
                            slimeAI.TakeDamage(damageAmount);
                            Debug.Log("Hit Slime and dealt " + damageAmount + " damage.");
                        }
                    }
                    else if (hit.collider.CompareTag("Goblin"))
                    {
                        GoblinAI goblinAI = hit.collider.GetComponent<GoblinAI>();
                        if (goblinAI != null)
                        {
                            goblinAI.TakeDamage(damageAmount);
                            Debug.Log("Hit Goblin and dealt " + damageAmount + " damage.");
                        }
                    }
                }
                else
                {
                    Debug.Log("Target is not within attack angle.");
                }
            }
            else
            {
                Debug.Log("Target is out of attack range.");
            }
        }
        else
        {
            Debug.Log("No target hit.");
        }
    }

    Vector2 GetAttackDirection()
    {
        // Use the current movement direction if moving, otherwise use last move direction
        if (playerController.IsMoving())
        {
            return playerController.GetCurrentMoveDirection();
        }
        else
        {
            return playerController.GetLastMoveDirection();
        }
    }
}
