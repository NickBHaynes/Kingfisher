using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Enemy Movement")]
    public float speed;
    private Transform target;
    private Base baseTarget;
    private Player playerTarget;
    public float stoppingDistance;
    private bool targetsBase;


    // cached references
    private Rigidbody2D theRb;

    // Start is called before the first frame update
    void Start()
    {
        baseTarget = FindObjectOfType<Base>();
        playerTarget = FindObjectOfType<Player>();
        theRb = GetComponent<Rigidbody2D>();

        targetsBase = Random.value > 0.5f;
        if (targetsBase)
        {
            target = baseTarget.transform;
        }
        else
        {
            target = playerTarget.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        

        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        }

        Vector2 dirToTarget = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        transform.up = dirToTarget;
    }
}
