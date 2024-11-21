using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkTransparent : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private float targetTransparency = 0.0f; // Target transparency based on the score
    private float currentTransparency = 0.0f; // Current transparency level

    public float transparencyIncrement = 0.08f; // 8% transparency increment per negative score
    public float maxTransparency = 1.0f; // Maximum transparency (fully transparent)
    public float smoothSpeed = 2.0f; // Speed of smooth interpolation for transparency changes

    private float lastScore = 0.0f; // Keep track of the last environment score to detect changes

    void Start()
    {
        // Get the SpriteRenderer component attached to this GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ensure the SpriteRenderer exists
        if (spriteRenderer == null)
        {
            Debug.LogError("DarkTransparent script requires a SpriteRenderer component.");
        }

        // Initialize transparency
        UpdateTransparency();
    }

    void Update()
    {
        // Check for changes in environmentScore
        if (GameManager.Instance != null && spriteRenderer != null)
        {
            float currentScore = GameManager.Instance.environmentScore;

            // Update the target transparency based on the score
            if (currentScore != lastScore)
            {
                AdjustTransparency(currentScore);
                lastScore = currentScore;
            }

            // Smoothly interpolate the current transparency toward the target transparency
            SmoothTransparency();
        }
    }

    private void AdjustTransparency(float environmentScore)
    {
        // Calculate the target transparency based on the score
        targetTransparency = Mathf.Abs(environmentScore) * transparencyIncrement;

        // Clamp the transparency to the maximum allowed value
        targetTransparency = Mathf.Clamp(targetTransparency, 0.0f, maxTransparency);
    }

    private void SmoothTransparency()
    {
        // Smoothly interpolate the current transparency to the target transparency
        currentTransparency = Mathf.Lerp(currentTransparency, targetTransparency, Time.deltaTime * smoothSpeed);

        // Apply the updated transparency
        UpdateTransparency();
    }

    private void UpdateTransparency()
    {
        // Update the alpha channel of the sprite's color
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = currentTransparency;
            spriteRenderer.color = color;
        }
    }
}
