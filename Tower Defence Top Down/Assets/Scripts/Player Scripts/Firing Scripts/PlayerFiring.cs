﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFiring : MonoBehaviour
{
    [Header("Player Shooting")]
    public float timeBetweenShots = 0.5f;
    private float shootingTimer;
    public GameObject projectile;
    public Transform projectileSpawnPoint;
    public GameObject firingVFX;
    Coroutine firingCo;
    public string firingSound;

    private int enemiesInRange;
    private bool enemyInRange;

    // cached references
    private GameSession theGameSession;

    // Start is called before the first frame update
    void Start()
    {
        theGameSession = FindObjectOfType<GameSession>();
        Fire();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesInRange >= 1)
        {
            enemyInRange = true;
        }
        else
        {
            enemyInRange = false;
        }

    }

    private void Fire()
    {
        firingCo = StartCoroutine(FireContinuoslyCo());
    }

    IEnumerator FireContinuoslyCo()
    {
        while (true)
        {
            FireRoutine();
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    private void FireRoutine()
    {
        if (enemyInRange)
        {
            if (firingVFX != null)
            {
                var newVFX = Instantiate(firingVFX, projectileSpawnPoint.position, transform.rotation);
                newVFX.transform.parent = transform;
            }

            var newProjectile = Instantiate(projectile, projectileSpawnPoint.position, transform.rotation);
            
                newProjectile.transform.parent = theGameSession.projectileContainer.transform;
           
            if (firingSound != null)
            {
                FindObjectOfType<AudioManager>().PlaySound(firingSound);
            }
        }
    }

    public void EnemyFound()
    {
        enemiesInRange += 1;
    }

    public void EnemyLost()
    {
        enemiesInRange -= 1;
    }
}
