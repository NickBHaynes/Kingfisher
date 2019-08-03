using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Player thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3 (thePlayer.transform.position.x, thePlayer.transform.position.y, transform.position.z);
    }
}
