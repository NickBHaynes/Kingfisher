using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float hitPoints;
    private float hitsTotal;
    private GameSession theGameSession;

    // Start is called before the first frame update
    void Start()
    {
        theGameSession = FindObjectOfType<GameSession>();
        hitsTotal = hitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float hitsToTake)
    {
        hitPoints -= hitsToTake;
        theGameSession.UpdatePlayerHealthBar(hitPoints, hitsTotal);

        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // deal with death here
        theGameSession.GameOver();
       // Destroy(gameObject);
    }

}
