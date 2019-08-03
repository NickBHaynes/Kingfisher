using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    public float hitPoints;
    private float hitsLeft;


    [Header("Health Bar bits")]
    public Image healthBar;




    // Start is called before the first frame update
    void Start()
    {
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
        }
    }

    private void Die()
    {
        // deal with death here
        FindObjectOfType<GameSession>().GameOver();
      //  Destroy(gameObject);
    }
}
