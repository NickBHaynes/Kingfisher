using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInRange : MonoBehaviour
{
    PlayerMovement theplayer;
    private void Start()
    {
        theplayer = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Enemy"))
        {
            theplayer.EnemyFound();
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Enemy"))
        {
            theplayer.EnemyLost();
        }
    }
}
