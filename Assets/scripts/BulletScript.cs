using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Transform target;
    private Color bulletColor; // Store the bullet's color
    
    public float speed = 70f;
    public GameObject impactEffect;

    void Update()
    {
        if (target == null) 
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget() 
    {
        GameObject effectsIns = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectsIns, 2f);
        Destroy(gameObject);

        // Check if the collided object has the "Enemy" tag
        if (target.CompareTag("Enemy"))
        {
            // Check if the bullet color matches the enemy color
            if (bulletColor == target.GetComponent<Renderer>().material.color)
            {
                // Destroy the enemy
                Destroy(target.gameObject);
            }
        }
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Set the color of the bullet
    public void SetColor(Color color)
    {
        bulletColor = color;

        // Update the bullet's color via the Renderer component
        GetComponent<Renderer>().material.color = bulletColor;
    }

    public Color GetColor()
    {
        return bulletColor;
    }
}
