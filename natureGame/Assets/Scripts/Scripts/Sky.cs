using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    public float distancePerScore = 0.5f; // Distance to move downward per unit of environmentScore
    public float smoothSpeed = 2f; // Speed of smooth movement
    public float minYPosition = 3.98f; // Minimum Y position for the sky
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    public List<Sprite> skySprites; // List of sprites for the sky

    private Vector3 initialPosition; // Initial position of the sky
    private Vector3 targetPosition; // Target position for smooth movement
    private int lastSpriteIndex = -1; // Track the last applied sprite index

    void Start()
    {
        // Store the initial position of the sky
        initialPosition = transform.position;
        targetPosition = initialPosition;

        // Subscribe to environment score changes
        GameManager.Instance.influences.CollectionChanged += OnEnvironmentScoreChanged;
    }

    private void OnEnvironmentScoreChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        UpdateTargetPositionAndSprite(GameManager.Instance.environmentScore);
    }

    private void UpdateTargetPositionAndSprite(float environmentScore)
    {
        // Calculate the target Y position based on the score
        float newYPosition = initialPosition.y - environmentScore * distancePerScore;

        // Clamp the Y position to stop at the minimum value
        newYPosition = Mathf.Max(newYPosition, minYPosition);

        // Set the target position for smooth movement
        targetPosition = new Vector3(transform.position.x, newYPosition, transform.position.z);

        // Update the sprite based on the score
        UpdateSprite(Mathf.Abs((int)environmentScore)); // Use the absolute value of the score
    }

    private void UpdateSprite(int score)
    {
        // Determine the sprite index based on the score
        int spriteIndex = Mathf.Clamp(score, 0, skySprites.Count - 1);

        // Only update the sprite if the index has changed
        if (spriteIndex != lastSpriteIndex)
        {
            spriteRenderer.sprite = skySprites[spriteIndex];
            lastSpriteIndex = spriteIndex;
        }
    }

    void Update()
    {
        // Smoothly interpolate the Y position of the sky toward the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
    }

    private void OnDestroy()
    {
        // Unsubscribe when this object is destroyed to avoid memory leaks
        if (GameManager.Instance != null)
        {
            GameManager.Instance.influences.CollectionChanged -= OnEnvironmentScoreChanged;
        }
    }
}
