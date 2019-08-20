using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float projectileSpeed = 10;
    public float hitsToGive = 1;

    public float lifeTime;
    public GameObject destroyEffect;

    public float shotgunSplitFactor;
    private Vector2 shotGunVector2;

    // Start is called before the first frame update
    void Start()
    {
        // destroys the projectile after a certain time
        Invoke("DestroyProjectile", lifeTime);
        shotGunVector2 = new Vector2(shotgunSplitFactor, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector2.up + shotGunVector2) * projectileSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // actions if hitting an enemy
        if (otherCollider.CompareTag("Enemy"))
        {
            otherCollider.GetComponent<Enemy>().TakeDamage(hitsToGive);
            DestroyProjectile();
        }

        // actions if hitting a friendly base
        if (otherCollider.CompareTag("Base"))
        {
            DestroyProjectile();
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
