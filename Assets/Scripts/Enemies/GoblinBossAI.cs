using System.Collections;
using UnityEngine;

public class GoblinBossAI : MonoBehaviour
{
    public BoxCollider2D bossRoomCollider; // Reference to the collider representing the boss room
    public float moveSpeed = 5f; // Speed at which the goblin moves
    public float attackRange = 2f; // Range at which the goblin will initiate an attack
    public float attackCooldown = 2f; // Cooldown between attacks
    

    public Transform player; // Reference to the player's Transform
    public bool isAttacking = false; // Flag to track if the goblin is currently attacking
    public bool isCooldown = false; // Flag to track if the goblin is on attack cooldown

    void Start()
    {
        // Find the player GameObject in the scene
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Check if the goblin is not currently attacking and not on cooldown
        if (!isAttacking && !isCooldown)
        {
            // Move towards the player
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            // Check if the goblin is within attack range of the player
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                // Start the attack coroutine
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        // Set the attacking flag to true
        isAttacking = true;

        // Move towards the player
        transform.LookAt(player.position);

        // Wait for a short delay to simulate charging at the player
        yield return new WaitForSeconds(0.5f);

        // Trigger the attack animation
        // Assuming you have a method to trigger the attack animation
        // For example, you could use something like dagger.GetComponent<Animator>().SetTrigger("Attack");
        // Replace "Attack" with the name of your attack trigger in the animator controller

        // Wait for the attack animation to finish
        // Replace 1f with the duration of your attack animation
        yield return new WaitForSeconds(1f);

        // Reset the attacking flag
        isAttacking = false;

        // Start the attack cooldown coroutine
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        // Set the cooldown flag to true
        isCooldown = true;

        // Wait for the attack cooldown duration
        yield return new WaitForSeconds(attackCooldown);

        // Reset the cooldown flag
        isCooldown = false;
    }
}
