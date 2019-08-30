using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFaceShip : MonoBehaviour
{

    public Transform shipsToFace;
    [SerializeField] float rotationAdjustment;

    public GameObject bullet;
    public Transform turretShootingPoint;

    public float bulletTimer;
    private float bulletDelay;

    public float bulletLife;
    private float bulletLifeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        bulletDelay = bulletTimer;
        bulletLifeRemaining = bulletLife;
    }

    // Update is called once per frame
    void Update()
    {

        // making the turret face the enemy ships;

        Vector2 dir = shipsToFace.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - rotationAdjustment;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        if (bullet != null && !bullet.activeInHierarchy)
        {
            if (bulletDelay <= 0)
            {
                bullet.transform.position = turretShootingPoint.position;
                bullet.SetActive(true);
                bulletDelay = bulletTimer;
            } else
            {
                bulletDelay -= Time.deltaTime;
            }
        }
        if (bullet.activeInHierarchy)
        {
            if (bulletLifeRemaining <= 0)
            {
                bullet.SetActive(false);
                bulletLifeRemaining = bulletLife;
            }
            else
            {
                bulletLifeRemaining -= Time.deltaTime;
            }
        }



    }

}
