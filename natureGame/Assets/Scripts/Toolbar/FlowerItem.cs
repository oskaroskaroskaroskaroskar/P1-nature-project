using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class FlowerItem : PouringItem
{
    public List<GameObject> flowers = new List<GameObject>();
    public static int flowerCount = 0;
    public static int flowerMax;
    bool colorMuted = false;

    public float dropOffset;
    private float pourSpeed;
    float makeFlowerTimer = 0;
    public GameObject childImage;

    private float tiltDegrees = 85;
    private Coroutine drippingCoroutine;
    public Animator animator;
    public GameObject SeedPrefab;

    System.Random random = new System.Random();
    public override void SetRenderes()
    {
        spriteRend = null;
        imageRend = childImage.GetComponent<UnityEngine.UI.Image>();
    }
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
            imageRend.color -= new Color32(150, 150, 150, 0);
        }
        else if (flowerCount < flowerMax && colorMuted)
        {
            colorMuted = false;
            imageRend.color += new Color32(150, 150, 150, 0);
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
            animator.SetBool("isPouring", true);
            if (animator.GetBool("isPouring")) // Check envInfluence before continuing
            {
                // Start dripping water drops if not already started
                if (drippingCoroutine == null)
                {
                    drippingCoroutine = StartCoroutine(DripSeed());
                }


            }
            else
            {
                StopDripping();
            }
        }
        else
        {
            StopDripping();
        }
        

    }

    private IEnumerator DripSeed()
    {

        while (animator.GetBool("isPouring")) // Check both conditions
        {
            CreateSeedDrop();
            yield return new WaitForSeconds(0.6f); // Number of seconds between drops
        }
        StopDripping();
    }

    private void CreateSeedDrop()
    {
        if (SeedPrefab != null)
        {
            // Instantiate the oil drop slightly below the barrel position
            GameObject oilDrop = Instantiate(SeedPrefab, transform.position + Vector3.down * 0.5f + Vector3.right * 0.5f, Quaternion.identity);

            // handle the drop's behavior (e.g., collision with the stain)
            FlowerSeedBehavior dropBehavior = oilDrop.AddComponent<FlowerSeedBehavior>();
            dropBehavior.yFallPos = transform.position.y+dropOffset;
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
    void MakeFlower()
    {
        
        int rInt = random.Next(0, flowers.Count);
        GameObject newFlower = Instantiate(flowers[rInt]);
        newFlower.transform.position = new Vector3(this.transform.position.x+ 0.5f, this.transform.position.y+dropOffset, 0);
    }
    public override void NotPouring()
    {

        animator.SetBool("isPouring", false);
        StopDripping();
    }
    public override void Dropped()
    {
        colorMuted = false;
    }
}
