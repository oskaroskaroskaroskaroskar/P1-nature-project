using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilDropBehavior : MonoBehaviour
{
    public GameObject oilStain; // Reference to the oil stain object

    private void Update()
    {
        // Check if the drop reaches the stain's y-position
        if (transform.position.y <= oilStain.transform.position.y)
        {
            // Destroy the drop
            Destroy(gameObject);
        }
    }
}
