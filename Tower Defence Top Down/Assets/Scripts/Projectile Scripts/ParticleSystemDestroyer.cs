using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemDestroyer : MonoBehaviour
{
    public float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ParticleLifeTimeCo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ParticleLifeTimeCo()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
