using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public float coinValue;
    public GameObject coinPickUpVFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            FindObjectOfType<GameSession>().PickUpCoins(coinValue);

            if (coinPickUpVFX != null)
            {
                Instantiate(coinPickUpVFX, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
