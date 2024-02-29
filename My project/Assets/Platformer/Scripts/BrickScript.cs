using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject brickParticle;
    void OnDestroy()
    {
        // Play particle effect!
        Instantiate(brickParticle, transform.position, transform.rotation);
    }
}
