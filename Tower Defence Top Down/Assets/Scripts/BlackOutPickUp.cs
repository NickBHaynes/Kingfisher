using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackOutPickUp : MonoBehaviour
{
    public float lifeTime = 5f;
    public float blackoutTime = 15f;
    public string pickUpSound;

    private PlayerMovement thePlayerMovement;
    // Start is called before the first frame update
    void Start()
    {
        thePlayerMovement = FindObjectOfType<PlayerMovement>();
        StartCoroutine(PowerUpLifeTimeCo());
    }

    private IEnumerator PowerUpLifeTimeCo()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {

        // look into using a struct for this??
        if (otherCollider.CompareTag("Player"))
        {
            thePlayerMovement.ActivateBlackout(blackoutTime);
            if (pickUpSound != null)
            {
                FindObjectOfType<AudioManager>().PlaySound(pickUpSound);
            }
        }

        Destroy(gameObject);

    }
}
