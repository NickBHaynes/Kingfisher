using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float hitsLeft;
    private float hitsRemaining;
    // Start is called before the first frame update
    void Start()
    {
        ResetHits();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetHits()
    {
        hitsRemaining = hitsLeft;
    }

    public void ShieldHit()
    {
        if (hitsRemaining >= 2)
        {
            hitsRemaining--;
        } else
        {
            FindObjectOfType<PlayerMovement>().ActivateShield(false);
        }
    }
}
