using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float speed;
    public float rotationSpeed = 5f; // Adjust the rotation speed as needed

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (player != null) // Check if the player reference is valid
        {
            // Calculate the direction from the enemy to the player
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Rotate the enemy to face the player
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Move the enemy in the direction of the player
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    // Detect collisions with the player
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Call the GameOver method in the GameManager
            gameManager.GameOver();
        }
    }
}
