using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterEnemyProjectile : MonoBehaviour
{
    public float projectileSpeed = 10;
    public float hitsToGive = 1;

    public float shotgunSplitFactor;

    public float damageFactor = 1;
    private float damage;

    public float lifeTime;
    public GameObject destroyEffect;

    private TransporterLocation[] randomLocationPoints;
    private int randomLocationNum;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        damage = hitsToGive * damageFactor;

        randomLocationPoints = FindObjectsOfType<TransporterLocation>();
        randomLocationNum = Random.Range(0, randomLocationPoints.Length);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector2.up) * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (randomLocationPoints[randomLocationNum] != null)
        {
            if (otherCollider.CompareTag("Player"))
            {
                otherCollider.transform.position = randomLocationPoints[randomLocationNum].transform.position;
                DestroyProjectile();
            }
            if (otherCollider.CompareTag("Base"))
            {
                otherCollider.transform.position = randomLocationPoints[randomLocationNum].transform.position; 
                DestroyProjectile();
            }

            if (otherCollider.CompareTag("ProjectileWall"))
            {
                DestroyProjectile();
            }
        }
    }

    private void DestroyProjectile()
    {
        if (destroyEffect != null)
        {
            //add destroy Effect
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
