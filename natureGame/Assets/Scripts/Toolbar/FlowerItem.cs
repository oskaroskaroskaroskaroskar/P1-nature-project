using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerItem : PouringItem
{
    public List<GameObject> flowers = new List<GameObject>();
    public static int flowerCount = 0;
    public static int flowerMax;
    bool colorMuted = false;

    public float dropOffset;
    private float pourSpeed;
    float makeFlowerTimer = 0;

    private float tiltDegrees = 85;
    private Coroutine drippingCoroutine;

    System.Random random = new System.Random();

    public override void OnStart()
    {
        flowerMax = 10;
        dropOffset = -1.7f;
        pourSpeed = 3f;
        cam = Camera.main;
    }
    void Update ()
    {
        base.Update();

        if (flowerCount >= flowerMax && !colorMuted)
        {
            colorMuted = true;
            image.color -= new Color32(150, 150, 150, 0);
        }
        else if (flowerCount < flowerMax && colorMuted)
        {
            colorMuted = false;
            image.color += new Color32(150, 150, 150, 0);
        }
       
    }

    public override void Pour()
    {
        if (flowerCount<flowerMax)
        {
            if (makeFlowerTimer<2f) //number of seconds till next flower
            {
                makeFlowerTimer += Time.deltaTime*pourSpeed;
                
            }
            else {
                MakeFlower();
                makeFlowerTimer = 0;
            }
        }
        else
        {
            StopDripping();
        }
        
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
    }*/

    private void StopDripping()
    {
        /*if (drippingCoroutine != null)
        {
            StopCoroutine(drippingCoroutine);
            drippingCoroutine = null;
        }
        */
    }

    private void OnMouseUp()
    {
        base.OnMouseUp(); // Ensures the existing stop logic from PouringItem is executed
        StopDripping(); // Stop the dripping coroutine when the mouse is released
    }
    void MakeFlower()
    {
        
        int rInt = random.Next(0, flowers.Count);
        GameObject newFlower = Instantiate(flowers[rInt]);
        newFlower.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+dropOffset, 0);
    }
    public override void NotPouring()
    {
        StopDripping();
    }
    public override void Dropped()
    {
        colorMuted = false;
    }
}
