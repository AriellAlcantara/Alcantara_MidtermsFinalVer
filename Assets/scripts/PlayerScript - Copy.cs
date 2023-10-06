using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Call the GameOver method in the GameManager
            gameManager.GameOver();
        }
    }
}
