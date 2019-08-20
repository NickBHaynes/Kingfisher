using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    public float speed;
    public float lookSpeed;
    public VariableJoystick movementJoystick;
    public DynamicJoystick lookDirectionJoystick;


    // cached references
    private Rigidbody2D theRb;
    private Vector2 moveVelocity;
    private Vector2 lookVelocity;
    private Player thePlayer;
    private Base theBase;
    private GameSession theGameSession;

    // Power Up Cached Data
    public bool powerUpActive; // serialized for debugging
    public GameObject theShield;

    // cached coroutine data to be populated by data from the power up script.
    private GameObject newExplosiveProjectile;
    private float newSpeedChangePercentage, newHealthPointsToAdd, newTimeActive, newBaseHealthToAdd, newPlayerHealthToRemove, newBaseHealthToRemove;
    private string debuffName;


    private float blackoutTimeActive;



    // Start is called before the first frame update
    void Start()
    {
        theRb = GetComponent<Rigidbody2D>();
        thePlayer = FindObjectOfType<Player>();
        theBase = FindObjectOfType<Base>();
        theGameSession = FindObjectOfType<GameSession>();
        movementJoystick = FindObjectOfType<VariableJoystick>();
        lookDirectionJoystick = FindObjectOfType<DynamicJoystick>();

     //   Fire();
    }

    // Update is called once per frame
    void Update()
    {

        if (movementJoystick != null && lookDirectionJoystick != null)
        {
            Vector2 moveInput = new Vector2(movementJoystick.Horizontal, movementJoystick.Vertical);
            moveVelocity = moveInput * speed;

        }
        Vector3 playerDirection = Vector3.right * lookDirectionJoystick.Horizontal + Vector3.up * lookDirectionJoystick.Vertical;
        if (lookDirectionJoystick.Horizontal != 0 && lookDirectionJoystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, playerDirection);
        }

        

    }

    private void FixedUpdate()
    {
        theRb.MovePosition(theRb.position + moveVelocity * Time.fixedDeltaTime);

    }

   


    // called from the individual power up script
    public IEnumerator ActivatePowerUpCo()
    {
        Debug.Log(debuffName);
        theGameSession.StartFlashingDebugLable(debuffName);

        speed *= newSpeedChangePercentage;
        thePlayer.hitPoints += newHealthPointsToAdd;
        theBase.hitPoints -= newBaseHealthToAdd;
        theBase.hitPoints += newBaseHealthToRemove;
        thePlayer.hitPoints -= newPlayerHealthToRemove;
        thePlayer.TakeDamage(0);
        theBase.TakeDamage(0);
        powerUpActive = true;

        yield return new WaitForSecondsRealtime(newTimeActive);
        speed /= newSpeedChangePercentage;
        theGameSession.StopFlashingDebugLable();
        theGameSession.debuffLabelShowing = true;
        powerUpActive = false;


    }

    public IEnumerator ActivateBlackOutCo()
    {
        theGameSession.blackoutSprite.SetActive(true);
        powerUpActive = true;
        theGameSession.StartFlashingDebugLable(debuffName);

        yield return new WaitForSecondsRealtime(blackoutTimeActive);

        theGameSession.blackoutSprite.SetActive(false);
        theGameSession.StopFlashingDebugLable();
        theGameSession.debuffLabelShowing = true;
        powerUpActive = false;

    }

    public void ActivateBlackout(float timeActive)
    {
        blackoutTimeActive = timeActive;
        debuffName = "Blackout";
        StartCoroutine(ActivateBlackOutCo());

    }

    public void ActivateShield(bool activate)
    {
        if (theShield != null)
        {
            theShield.SetActive(activate);
            theShield.GetComponent<Shield>().ResetHits();
        }
        else
        {
            Debug.LogError("Shield Game Object Missing");
        }
    }

    public void ActivatePowerUpDataInput(GameObject expolosiveProjectile, float speedChangePercentage,
    float healthPointsToAdd, float timeActive, string newDebuffName, float baseHealthToAdd, float playerHealthToRemove, float baseHealthToRemove)
    {
        debuffName = newDebuffName;
        newExplosiveProjectile = expolosiveProjectile;
        newSpeedChangePercentage = speedChangePercentage;
        newHealthPointsToAdd = healthPointsToAdd;
        newTimeActive = timeActive;
        newBaseHealthToAdd = baseHealthToAdd;
        newBaseHealthToRemove = baseHealthToRemove;
        newPlayerHealthToRemove = playerHealthToRemove;

        StartCoroutine(ActivatePowerUpCo());
    }
}

