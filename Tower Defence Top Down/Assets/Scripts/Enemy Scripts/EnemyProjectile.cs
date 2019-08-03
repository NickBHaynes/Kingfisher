using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public float projectileSpeed = 10;
    public float hitsToGive = 1;

    public float shotgunSplitFactor;
    private Vector2 shotGunVector2;

    public float damageFactor = 1;
    private float damage;

    public float lifeTime;
    public GameObject destroyEffect;

    private GameSession theGameSession;

    [Header("Camera Shake variables")]
    public float shakeMagnitude = 15; 
    public float shakeRoughness = 5;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        damage = hitsToGive * damageFactor;
        shotGunVector2 = new Vector2(shotgunSplitFactor, 0);
        theGameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector2.up + shotGunVector2) * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            theGameSession.ShakeCamera(shakeMagnitude, shakeRoughness);
            otherCollider.GetComponent<Player>().TakeDamage(damage);
            DestroyProjectile();
        }
        if (otherCollider.CompareTag("Base"))
        {
            otherCollider.GetComponent<Base>().TakeDamage(damage);
            DestroyProjectile();
        }

        if (otherCollider.CompareTag("Shield"))
        {
            FindObjectOfType<Shield>().ShieldHit();
            DestroyProjectile();
        }

        if (otherCollider.CompareTag("ProjectileWall"))
        {
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        if (destroyEffect != null)
        {
            //add destroy Effect
            var newVFX = Instantiate(destroyEffect, transform.position, Quaternion.identity);
            newVFX.transform.parent = theGameSession.projectileContainer.transform;
        }
        Destroy(gameObject);
    }
}
