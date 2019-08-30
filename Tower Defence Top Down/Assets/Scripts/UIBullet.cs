using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIBullet : MonoBehaviour
{
    public Transform shipPos;
    public float projectileDistance;
    public RectTransform theTransform;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        theTransform.position = Vector3.MoveTowards(transform.position, shipPos.position, projectileDistance);
    }
}
