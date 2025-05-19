using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoblinAI : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the goblin moves
    public float jumpAttackDistance = 5f; // Distance for the jump attack
    public float detectionTime = 2f; // Time the player needs to be in sight for the goblin to attack
    public float stopDistance = 1.5f; // Distance to stop before reaching the player
    public float jumpSpeed = 10f; // Speed of the jump attack
    public float maxHealth = 100f; // Maximum health of the goblin
    public int attackDamage = 10; // Damage dealt by the goblin's attack
    public GameObject itemPrefab; // Prefab of the item to drop upon death

    private float currentHealth; // Current health of the goblin
    private Transform player;
    private bool playerInRange = false;
    private bool isTrackingPlayer = true;
    private float detectionTimer = 0f;
    private bool isAttacking = false;
    public LayerMask wallLayer; // Layer for walls
    public LayerMask goblinLayer; // Layer for goblin itself
    private Animator animator;
    private Slider healthSlider; // Reference to the health bar slider

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; // Initialize the goblin's health
        healthSlider = GetComponentInChildren<Slider>(); // Get the health bar slider component

        // Debug log to check if the slider is assigned
        if (healthSlider != null)
        {
            Debug.Log("Health slider assigned successfully.");
            healthSlider.maxValue = maxHealth; // Ensure the slider's max value matches the goblin's max health
            healthSlider.minValue = 0; // Ensure the slider's min value is 0
            healthSlider.wholeNumbers = true; // Ensure the slider uses whole numbers
        }
        else
        {
            Debug.LogError("Health slider not found. Please check the hierarchy and assignment.");
        }

        UpdateHealthBar(); // Initialize the health bar
    }

    void Update()
    {
        if (playerInRange && player != null && !isAttacking)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            int layerMask = goblinLayer; // Change applied here // Correctly converting the LayerMask to an int
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distanceToPlayer, layerMask);

            Debug.DrawRay(transform.position, direction * distanceToPlayer, Color.red);

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                detectionTimer += Time.deltaTime;
                if (detectionTimer >= detectionTime && distanceToPlayer <= jumpAttackDistance)
                {
                    Attack();
                }
                else if (isTrackingPlayer && distanceToPlayer > stopDistance)
                {
                    animator.SetBool("isWalking", true);
                    animator.SetTrigger("NotAttack");
                    transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
                }
                else
                {
                    animator.SetBool("isWalking", false);
                }
            }
            else
            {
                detectionTimer = 0f;
                animator.SetBool("isWalking", false);
                Debug.Log("Player is not in line of sight.");
                Debug.Log(hit.collider.name);
            }
        }
        else
        {
            detectionTimer = 0f;
            animator.SetBool("isWalking", false);
            Debug.Log("Player not in range or not assigned.");
        }
    }

    public void Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack"); // Trigger the attack animation
        // Add a delay to simulate attack animation time
        StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        yield return new WaitForSeconds(0.5f); // Wait for the attack animation to complete
        DealDamage();
        isAttacking = false;
    }

    public void DealDamage()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= stopDistance)
        {
            // Assuming the player has a method TakeDamage
            player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            Debug.Log("Player took damage: " + attackDamage);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            player = other.transform;
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger");
            playerInRange = false;
            player = null;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth; // Directly set the slider's value to currentHealth
            Debug.Log("Health bar updated. Current health: " + currentHealth);
        }
    }

    private void Die()
    {
        // Implement death logic here, e.g., playing death animation, destroying the goblin, etc.
        DropItem(); // Drop item on death
        Destroy(gameObject);
    }

    private void DropItem()
    {
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
            Debug.Log("Item dropped at position: " + transform.position);
        }
        else
        {
            Debug.LogError("Item prefab is not assigned.");
        }
    }
}
