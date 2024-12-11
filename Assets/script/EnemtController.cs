using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;             // Reference to the player's transform
    public float attackRange = 2f;       // Range within which the enemy can attack
    public float moveSpeed = 3.5f;       // Speed of the enemy's movement
    public float damageAmount = 34f;     // Damage dealt to the player
    public float attackCooldown = 1f;    // Time between each attack
    public float maxHealth = 100f;       // Enemy's maximum health

    private float currentHealth;         // Enemy's current health
    private float lastAttackTime = 0f;   // Track time for attack cooldown

    private void Start()
    {
        // Set the enemy's health to maximum at the start
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (player == null) return;

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Chase the player if not within attack range
        if (distanceToPlayer > attackRange)
        {
            ChasePlayer();
        }
        else
        {
            // Attack the player if within attack range
            AttackPlayer();
        }
    }

    private void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Make the enemy face the player
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    private void AttackPlayer()
    {
        // Check if enough time has passed since the last attack
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;

            // Assuming the player has a HealthController component with TakeDamage method
            HealthController playerHealth = player.GetComponent<HealthController>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Debug.Log("Enemy dealt " + damageAmount + " damage to the player. Player Health: " + playerHealth.currentPlayerHealth);
            }
            else
            {
                Debug.LogWarning("Player's HealthController component not found.");
            }
        }
    }

    // Method to apply damage to the enemy
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took " + damage + " damage. Current Health: " + currentHealth);

        // Check if the enemy's health has reached zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to handle the enemy's death
    private void Die()
    {
        Debug.Log("Enemy has died.");
        Destroy(gameObject);  // Destroy the enemy GameObject
    }
}
