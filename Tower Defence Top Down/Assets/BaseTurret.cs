using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    Enemy closestEnemy;
    bool canFire;
    [SerializeField] float timeBetweenShots;
    private float timeLeft;

    public GameObject projectile;
    public Transform projectileSpawnPoint;
    public GameObject firingVFX;

    private GameSession theGameSession;

    // Start is called before the first frame update
    void Start()
    {
        theGameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();

        if (closestEnemy != null)
        {
            FaceEnemy();
            if (canFire)
            {
                Fire();
                canFire = false;
            }
        }

        if (timeLeft <= 0)
        {
            canFire = true;
            timeLeft = timeBetweenShots;
        }
        timeLeft -= Time.deltaTime;

    }

    public void FindClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        closestEnemy = null;
        Enemy[] allCurrentEnemies = FindObjectsOfType<Enemy>();
        if (allCurrentEnemies.Length != 0)
        {
            foreach (var currentEnemy in allCurrentEnemies)
            {
                float distanceToEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;
                if (distanceToEnemy < distanceToClosestEnemy)
                {
                    distanceToClosestEnemy = distanceToEnemy;
                    closestEnemy = currentEnemy;
                }
            }
        }
        
    }

    private void FaceEnemy()
    {
        Vector2 dirToTarget = new Vector2(closestEnemy.transform.position.x - transform.position.x, closestEnemy.transform.position.y - transform.position.y);
        transform.up = dirToTarget;
    }

    private void Fire()
    {
        if (firingVFX != null)
        {
            var newVFX = Instantiate(firingVFX, projectileSpawnPoint.position, transform.rotation);
            newVFX.transform.parent = transform;
        }

        var newProjectile = Instantiate(projectile, projectileSpawnPoint.position, transform.rotation);
        newProjectile.transform.parent = theGameSession.projectileContainer.transform;
    }

}
