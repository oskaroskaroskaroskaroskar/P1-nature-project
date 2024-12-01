using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour
{
     public Sprite[] backgroundSprites; // Array of background images
     public Image backgroundImage; // Image component of the background

    // Start is called before the first frame update
    void Start()
    {
         // Get the Image component
        backgroundImage = GetComponent<Image>();
        
         // Set the initial sprite if available
        if (backgroundSprites.Length > 0)
        {
            backgroundImage.sprite = backgroundSprites[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update the background based on environmentScore
         if (GameManager.Instance != null)
         {
            float currentScore = GameManager.Instance.environmentScore;

            if (currentScore < -8) // Negative score is high than x: switch to "dark" background
            {
                if (backgroundSprites.Length > 1) // Check if dark background exists
                {
                    backgroundImage.sprite = backgroundSprites[1];
                }
            }
            else // Positive score is lower than x: switch to "light" background
            {
                if (backgroundSprites.Length > 0) // Check if light background exists
                {
                    backgroundImage.sprite = backgroundSprites[0];
                }
            }
         }
    }
}
