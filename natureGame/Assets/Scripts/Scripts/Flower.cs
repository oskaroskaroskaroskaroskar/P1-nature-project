using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    bool isSeed = true;
    float waterSucked = 0f;
    float suckingSpeed;
    int inWaterStains = 0;

    float darkness;

    public Animator animator;
    GameManager gameManager;
    
    void Start()
    {
        suckingSpeed = 0.5f;
        FlowerItem.flowerCount++;

        gameManager = Camera.main.GetComponent<GameManager> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSeed && inWaterStains > 0)
        {
            Water();
        } 
        if (!isSeed && gameManager.environmentScore < -3f)
        {
            animator.SetTrigger("Dying");
            Destroy(gameObject,3f);
        }
    }

    void Water ()
    {
        if (waterSucked < 1.0f)
        {
            darkness += suckingSpeed * Time.deltaTime*200;
            waterSucked += suckingSpeed * Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().color = new Color32((byte)(255 - darkness), (byte)(255 - darkness), (byte)(225 - darkness), 255);
        } else
        {
            animator.SetTrigger("Flowering");
            isSeed = false;
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (isSeed && other.tag == "WaterStain")
        {
            inWaterStains++;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (isSeed && other.tag == "WaterStain")
        {
            inWaterStains--;
        }
    }
    void OnDestroy ()
    {
        FlowerItem.flowerCount--;
    }
}
