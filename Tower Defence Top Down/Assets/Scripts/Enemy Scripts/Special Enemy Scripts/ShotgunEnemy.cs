using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunEnemy : MonoBehaviour
{
    public float timeBetweenShots = 0.5f;
    private float timeLeftToShoot;
    public GameObject projectile;
    public Transform projectileSpawnPoint;
    private GameSession theGameSession;
    public float damageFactor = 1f;
    public float shotgunSplit;



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
            var newProjectile = Instantiate(projectile, projectileSpawnPoint.position, transform.rotation);
            newProjectile.transform.parent = theGameSession.projectileContainer.transform;
            newProjectile.GetComponent<EnemyProjectile>().damageFactor = damageFactor;
            timeLeftToShoot = timeBetweenShots;


            var newProjectileTwo = Instantiate(projectile, projectileSpawnPoint.position, transform.rotation);
            newProjectileTwo.transform.parent = theGameSession.projectileContainer.transform;
            newProjectileTwo.GetComponent<EnemyProjectile>().damageFactor = damageFactor;
            newProjectileTwo.GetComponent<EnemyProjectile>().shotgunSplitFactor = shotgunSplit;


            var newProjectileThree = Instantiate(projectile, projectileSpawnPoint.position, transform.rotation);
            newProjectileThree.transform.parent = theGameSession.projectileContainer.transform;
            newProjectileThree.GetComponent<EnemyProjectile>().damageFactor = damageFactor;
            newProjectileThree.GetComponent<EnemyProjectile>().shotgunSplitFactor = -shotgunSplit;

        }
        timeLeftToShoot -= Time.deltaTime;
    }
}
