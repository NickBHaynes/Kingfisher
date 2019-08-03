using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    public float hitPoints;
    private float hitsLeft;
    public float pointsForKill;

    public GameObject[] enemyDrops;
    public GameObject deathVFX;

    public int enemyDropChance;

    [Header("Health Bar bits")]
    public Image healthBar;
    private GameSession theGameSession;

    // Audio Cache
    private AudioManager theAMan;
    public string impactSound;
    public string deathSound;


    // Start is called before the first frame update
    void Start()
    {
        theGameSession = FindObjectOfType<GameSession>();
        theAMan = FindObjectOfType<AudioManager>();

        hitsLeft = hitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float hitsToTake)
    {
        hitsLeft -= hitsToTake;
        healthBar.fillAmount = hitsLeft / hitPoints;

        if (hitsLeft <= 0)
        {
            Die();
        } else
        {
            if (impactSound != null)
            {
                theAMan.PlaySound(impactSound);
            }
           
        }

    }

    private void Die()
    {
        // deal with death here
        FindObjectOfType<EnemySpawner>().EnemyKilled();
        theGameSession.pointsEarntInLevel += pointsForKill;
        DropCoin();

        if (deathSound != null)
        {
            theAMan.PlaySound(deathSound);
        }

        if (deathVFX != null)
        {
            var newDeathVFX = Instantiate(deathVFX, transform.position, Quaternion.identity);
            newDeathVFX.transform.parent = theGameSession.projectileContainer.transform;
        }

        Destroy(gameObject);
    }

    // Drop coin, also drops pick ups
    public void DropCoin()
    {
        var chance = Random.Range(0, enemyDropChance);
        if (chance >= (enemyDropChance * 2/3))
        {
            var pickUpToDrop = Random.Range(0, enemyDrops.Length);

            // the pick up which is dropped is chosen at random from a pick up array which can
            // be populated in the inspector
            var pickUp = Instantiate(enemyDrops[pickUpToDrop], transform.position, Quaternion.identity);
            pickUp.transform.parent = theGameSession.pickUpContainer.transform;
        }
    }

   
}
