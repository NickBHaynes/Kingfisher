using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{

    Transform targetPosition;
    public float hideDistance;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = FindObjectOfType<Base>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        var dir = targetPosition.position - transform.position;

        

        if (dir.magnitude < hideDistance)
        {

            SetChildrenActive(false);

        } else
        {

            SetChildrenActive(true);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }

    }

    void SetChildrenActive(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }

}
