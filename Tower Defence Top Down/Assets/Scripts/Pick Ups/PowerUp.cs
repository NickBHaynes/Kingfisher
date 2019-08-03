using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public string debuffName;
    public float timeActive = 30f;
    public float playerHealthToAdd;
    public float playerHealthToRemove;
    public float baseHealthToAdd;
    public float baseHealthToRemove;
    public float speedChangePercentage = 1;
    public GameObject expolosiveProjectile;
    public float lifeTime = 5f;
    public string pickUpSound;

    // cached references
    private PlayerMovement thePlayerMovement;
    private Player thePlayer;
    private Base theBase;

    // Start is called before the first frame update
    void Start()
    {
        thePlayerMovement = FindObjectOfType<PlayerMovement>();
       
        StartCoroutine(PowerUpLifeTimeCo());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator PowerUpLifeTimeCo()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    // triggering the individual power up by their name

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // making sure only one power up is active at a time
        if (!thePlayerMovement.powerUpActive)
        {
            // look into using a struct for this??
            if (otherCollider.CompareTag("Player"))
            {
                FindObjectOfType<AudioManager>().PlaySound(pickUpSound);
                thePlayerMovement.ActivatePowerUpDataInput(expolosiveProjectile, speedChangePercentage,
                playerHealthToAdd, timeActive, debuffName, baseHealthToAdd, playerHealthToRemove, baseHealthToRemove);
            }

            Destroy(gameObject);
        }
    }
}
