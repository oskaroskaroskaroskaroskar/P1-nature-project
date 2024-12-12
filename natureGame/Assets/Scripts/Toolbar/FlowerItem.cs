using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class FlowerItem : PouringItem
{
    public List<GameObject> flowers = new List<GameObject>(); //List of flower prefabs
    public static int flowerCount = 0; //Number of current flowers
    public static int flowerMax; //Maximum number of flowers
    bool colorMuted = false; //Bool set if flowermax is reached

    public float dropOffset; //Offset in y-cordinate for seed
    private float pourSpeed; //Speed which the seeds are made
    float makeFlowerTimer = 0; //Timer reset each time a flower is set
    public GameObject childImage; //Reference to image of sack

    private Coroutine drippingCoroutine; //Co-routine for controlling dripping seeds 
    public Animator animator; //Reference to animator
    public GameObject SeedPrefab; //Prefab of flower-seed

    System.Random random = new System.Random(); //Variable for creating random numbers
    public override void SetRenderes() //Override of pouringitem method. Used to create reference of rendere or image
    {
        spriteRend = null;
        imageRend = childImage.GetComponent<UnityEngine.UI.Image>();
    }
    public override void OnStart() //method called from parent in void Start() 
    {
        flowerMax = 10;
        dropOffset = -1.7f;
        pourSpeed = 3f;
        cam = Camera.main;
    }
    void Update ()
    {
        base.Update();

        if (flowerCount >= flowerMax && !colorMuted) //Checks if max number flowers and image not muted
        {
            colorMuted = true;
            imageRend.color -= new Color32(150, 150, 150, 0); //Mutes color
        }
        else if (flowerCount < flowerMax && colorMuted)
        {
            colorMuted = false;
            imageRend.color += new Color32(150, 150, 150, 0); //Unmutes color
        }
       
    }

    public override void Pour() //Method called from parent when pouring
    {
        if (flowerCount<flowerMax)
        {
            if (makeFlowerTimer<2f) //time till next flower
            {
                makeFlowerTimer += Time.deltaTime*pourSpeed; //Enhancing timer
                
            }
            else {
                MakeFlower(); 
                makeFlowerTimer = 0; //Resets timer
            }
            animator.SetBool("isPouring", true); //Sets animation
            if (animator.GetBool("isPouring")) // Checking if pouring
            {
                // Start dripping seeds if not already started
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

    private IEnumerator DripSeed() //Co-routine used for making script run smoothly even though delay is applied
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


    private void OnMouseUp() //Method called whenever mouse if unclicked
    {
        base.OnMouseUp(); // Ensures the existing stop logic from PouringItem is executed
        StopDripping(); // Stop the dripping coroutine when the mouse is released
    }
    void MakeFlower() //Method for making a flower
    {
        
        int rInt = random.Next(0, flowers.Count); //Gets a random integer representing the different colors of flowers
        GameObject newFlower = Instantiate(flowers[rInt]);
        newFlower.transform.position = new Vector3(this.transform.position.x+ 0.5f, this.transform.position.y+dropOffset, 0); //Fixing position
    }
    public override void NotPouring() //Method called from parent when not pouring
    {

        animator.SetBool("isPouring", false);
        StopDripping();
    }
    public override void Dropped() //method called from parent when item stops being used
    {
        colorMuted = false;
    }
}
