using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [Header("Attributes")]
    public float range = 15f;
    public Transform target;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed = 10f;
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private GameManager gameManager;
    private Renderer turretRenderer;
    private Color turretColor;
    private bool isClicked = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        turretRenderer = GetComponent<Renderer>();
        turretColor = RandomTurretColor();
        turretRenderer.material.color = turretColor;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && !isClicked)
        {
            ChangeTurretColor();
        }
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletScript bullet = bulletGO.GetComponent<BulletScript>();

        if (bullet != null)
        {
            bullet.Seek(target);
            bullet.SetColor(turretColor);
        }
    }

    void ChangeTurretColor()
    {
        Color newColor = RandomTurretColor();
        turretRenderer.material.color = newColor;
        turretColor = newColor;
        isClicked = true;
    }

    Color RandomTurretColor()
    {
        Color[] allowedColors = new Color[] { Color.red, Color.blue, Color.green };
        return allowedColors[Random.Range(0, allowedColors.Length)];
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
