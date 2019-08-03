using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{

    public float timeBetweenShots = 0.5f;
    private float timeLeftToShoot;
    public GameObject projectile;
    public Transform projectileSpawnPoint;
    private GameSession theGameSession;
    public float damageFactor = 1f;
         


    // Start is called before the first frame update
    void Start()
    {
        theGameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeftToShoot <= 0)
        {
            var newProjectileOne = Instantiate(projectile, projectileSpawnPoint.position, transform.rotation);
            newProjectileOne.transform.parent = theGameSession.projectileContainer.transform;

            if (newProjectileOne.GetComponent<EnemyProjectile>() != null)
            {
                newProjectileOne.GetComponent<EnemyProjectile>().damageFactor = damageFactor;
            }

            timeLeftToShoot = timeBetweenShots;
        }
        timeLeftToShoot -= Time.deltaTime;
    }
}
