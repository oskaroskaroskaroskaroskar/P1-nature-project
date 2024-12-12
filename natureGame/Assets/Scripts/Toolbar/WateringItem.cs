using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringItem : PouringItem
{
    public GameObject waterDropPrefab; // Prefab for the oil drop
    public GameObject marker; //Reference to drop-point

    private Coroutine drippingCoroutine;//Co-routine for controlling dripping seeds 
    public GameObject childImage;//Reference to image of watering can
    public Animator animator; //Reference to animator

    public override void OnStart() //Method called from parent in void Start()
    {
        cam = Camera.main;
    }
    public override void SetRenderes() //Overrides method from parent to set iamge reference
    {
        spriteRend = null;
        imageRend = childImage.GetComponent<UnityEngine.UI.Image>() ;
    }

    public override void Pour() //Method called from parent when parent
    {
        marker.GetComponent<WaterMarker>().Pour();
        animator.SetBool("isPouring", true);
        if (animator.GetBool("isPouring")) // Checking if pouring
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

    private IEnumerator DripWater() //Co-routine used for making script run smoothly even though delay is applied
    { 
    
        while (animator.GetBool("isPouring")) // Check both conditions
        {
            CreateWaterDrop();
            yield return new WaitForSeconds(0.5f); //Number of seconds between drops
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
   
    private void OnMouseUp()//Method called whenever mouse if unclicked
    {
        base.OnMouseUp(); // Ensures the existing stop logic from PouringItem is executed
        StopDripping(); // Stop the dripping coroutine when the mouse is released
    }
    public override void NotPouring()//Method called from parent when not pouring
    {

        animator.SetBool("isPouring", false);
        StopDripping();
    }
}
