using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "plane")
        {
            var plane = other.gameObject.GetComponent<PlaneMovement>();
            plane.rings -= 1;
            Destroy(gameObject);
        }
    }
}
