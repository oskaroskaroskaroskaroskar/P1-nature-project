using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringItem : PouringItem
{
    public GameObject waterDropPrefab; // Prefab for the oil drop
    public GameObject marker;

    private float pourSpeed;
    private float envInfluence;
    private float tiltDegrees = 85;
    private Coroutine drippingCoroutine;

    public Animator animator;

    public override void OnStart()
    {
        pourSpeed = 0.2f;
        envInfluence = -10f;
        cam = Camera.main;
    }

    public override void Pour()
    {
        marker.GetComponent<WaterMarker>().Pour();
        animator.SetBool("isPouring", true);
        Debug.Log("startPouring");
        if (animator.GetBool("isPouring")) // Check envInfluence before continuing
        {
            // Start dripping water drops if not already started
            if (drippingCoroutine == null)
            {
                drippingCoroutine = StartCoroutine(DripWater());
            }


        }
        else
        {
            StopDripping();
        }
    }

    private IEnumerator DripWater()
    { 
    
        while (animator.GetBool("isPouring")) // Check both conditions
        {
            CreateWaterDrop();
            yield return new WaitForSeconds(0.5f); // Wait 1 second between drops
        }
        StopDripping();
    }

    private void CreateWaterDrop()
    {
        if (waterDropPrefab != null)
        {
            // Instantiate the oil drop slightly below the barrel position
            GameObject oilDrop = Instantiate(waterDropPrefab, transform.position + Vector3.down * 0.5f+Vector3.right*0.5f, Quaternion.identity);

            // handle the drop's behavior (e.g., collision with the stain)
            OilDropBehavior dropBehavior = oilDrop.AddComponent<OilDropBehavior>();
            dropBehavior.oilStain = marker;
        }
        else
        {
            Debug.LogWarning("OilDropPrefab is not assigned!");
        }
    }
    private void StopDripping()
    {
        if (drippingCoroutine != null)
        {
            StopCoroutine(drippingCoroutine);
            drippingCoroutine = null;
        }
    }
   
    private void OnMouseUp()
    {
        base.OnMouseUp(); // Ensures the existing stop logic from PouringItem is executed
        StopDripping(); // Stop the dripping coroutine when the mouse is released
    }
    public override void NotPouring()
    {

        Debug.Log("stopPouring");
        animator.SetBool("isPouring", false);
        StopDripping();
    }
}
