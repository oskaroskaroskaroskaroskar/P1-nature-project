using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSeedBehavior : MonoBehaviour
{
    public float yFallPos;

    private void Update()
    {
        // Check if the drop reaches the stain's y-position
        if (transform.position.y <= yFallPos)
        {
            // Destroy the drop
            Destroy(gameObject);
        }
    }
}
