using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInRange : MonoBehaviour
{
    PlayerFiring playerFiring;
    private void Start()
    {
        playerFiring = FindObjectOfType<PlayerFiring>();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Enemy"))
        {
            playerFiring.EnemyFound();
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Enemy"))
        {
            playerFiring.EnemyLost();
        }
    }
}
