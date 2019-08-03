using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomMovement : MonoBehaviour
{

    private bool playerFound;
    private Vector2 targetPosition;
    private Player thePlayer;
    private Transform thePlayerTarget;

    // setting the four corners of the map whithin wich the char can move
    [Header("Movement Boundries")]
    private GameSession theGameSession;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    [Header("Movement Variables")]
    public float speed;
    public float stoppingDistance;

    // Start is called before the first frame update
    void Start()
    {
        theGameSession = FindObjectOfType<GameSession>();
        minX = theGameSession.minX;
        minY = theGameSession.minY;
        maxX = theGameSession.maxX;
        maxY = theGameSession.maxY;

        targetPosition = GetRandomPosition();
        thePlayer = FindObjectOfType<Player>();
        thePlayerTarget = thePlayer.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //checking to see if the char should be following the player or traveling randomly
        if (!playerFound)
        {
            // moving the char towards its target position
            if ((Vector2)transform.position != targetPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

                // making the char face its target
                Vector2 dirToTarget = new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y);
                transform.up = dirToTarget;
            }
            else
            {
                // setting a new random position once the char has reached its target
                targetPosition = GetRandomPosition();
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, thePlayerTarget.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, thePlayerTarget.position, speed * Time.fixedDeltaTime);
            }

            Vector2 dirToTarget = new Vector2(thePlayerTarget.position.x - transform.position.x, thePlayerTarget.position.y - transform.position.y);
            transform.up = dirToTarget;
        }
    }

    Vector2 GetRandomPosition()
    {
        // setting a random location in the map
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector2(randomX, randomY);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // detecting if the char comes within range of the player
        if (otherCollider.CompareTag("Player"))
        {
            // targeting the player not random location
            playerFound = true;
        }
    }
}
