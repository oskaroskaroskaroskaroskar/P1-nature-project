using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringItem : PouringItem
{
    public GameObject waterDropPrefab; // Prefab for the oil drop
    public GameObject Marker;
    private float pourSpeed;
    private float envInfluence;
    private float tiltDegrees = 85;
    private Coroutine drippingCoroutine;

    public override void OnStart()
    {
        pourSpeed = 0.2f;
        envInfluence = -10f;
        cam = Camera.main;
    }

    public override void Pour()
    {
        Marker.GetComponent<WaterMarker>().Pour();
        /*if (oilStain.transform.localScale.x < 1 && envInfluence < 0) // Check envInfluence before continuing
        {
            // Start dripping oil drops if not already started
            if (drippingCoroutine == null)
            {
                drippingCoroutine = StartCoroutine(DripOil());
            }

            // Simulate the barrel tilting while pouring
            Vector3 targetRotation = new Vector3(0, 0, tiltDegrees);
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, targetRotation, Time.deltaTime);

            // Increase the size of the oil stain over time
            oilStain.transform.localScale += new Vector3(Time.deltaTime * pourSpeed, Time.deltaTime * pourSpeed, 0);
            oilStain.GetComponent<OilPool>().influence += Time.deltaTime * pourSpeed * envInfluence;

            // Update the environment influence in the game manager
            Camera.main.GetComponent<GameManager>().GetInfluence();
        }
        else
        {
            StopDripping();
        }
        */
    }

    /*private IEnumerator DripOil()
    {
        while (oilStain.transform.localScale.x < 1 && envInfluence < 0) // Check both conditions
        {
            CreateOilDrop();
            yield return new WaitForSeconds(1f); // Wait 1 second between drops
        }
        StopDripping();
    }

    private void CreateOilDrop()
    {
        if (oilDropPrefab != null)
        {
            // Instantiate the oil drop slightly below the barrel position
            GameObject oilDrop = Instantiate(oilDropPrefab, transform.position + Vector3.down * 0.5f, Quaternion.identity);

            // handle the drop's behavior (e.g., collision with the stain)
            OilDropBehavior dropBehavior = oilDrop.AddComponent<OilDropBehavior>();
            dropBehavior.oilStain = oilStain;
        }
        else
        {
            Debug.LogWarning("OilDropPrefab is not assigned!");
        }
    }
    */
    private void StopDripping()
    {
        /*if (drippingCoroutine != null)
        {
            StopCoroutine(drippingCoroutine);
            drippingCoroutine = null;
        }*/
    }

    private void OnMouseUp()
    {
        base.OnMouseUp(); // Ensures the existing stop logic from PouringItem is executed
        StopDripping(); // Stop the dripping coroutine when the mouse is released
    }
    public override void NotPouring()
    {
        StopDripping();
    }
}
