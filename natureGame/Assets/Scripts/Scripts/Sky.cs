using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    public float distancePerScore = 0.5f; // Distance to move downward per unit of environmentScore
    public float smoothSpeed = 2f; // Speed of smooth movement
    public float minYPosition = 3.98f; // Minimum Y position for the sky
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer

    private Vector3 initialPosition; // Initial position of the sky
    private Vector3 targetPosition; // Target position for smooth movement

    void Start()
    {
        // Store the initial position of the sky
        initialPosition = transform.position;
        targetPosition = initialPosition;

        // Subscribe to environment score changes
        GameManager.Instance.influences.CollectionChanged += OnEnvironmentScoreChanged;

        // Initialize alpha
        SetAlpha(0f); // Fully opaque at the start
    }

    private void OnEnvironmentScoreChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        UpdateTargetPosition(GameManager.Instance.environmentScore);
        UpdateAlpha(GameManager.Instance.environmentScore);
    }

    private void UpdateTargetPosition(float environmentScore)
    {
        // Calculate the target Y position based on the score
        float newYPosition = initialPosition.y - environmentScore * distancePerScore;

        // Clamp the Y position to stop at the minimum value
        newYPosition = Mathf.Max(newYPosition, minYPosition);

        // Set the target position for smooth movement
        targetPosition = new Vector3(transform.position.x, newYPosition, transform.position.z);
    }

    private void UpdateAlpha(float environmentScore)
    {
        // Adjust alpha based on the negative environmentScore
        float alpha = Mathf.Clamp01(1f + (environmentScore / 10f)); // Reduce alpha every -10 points
        SetAlpha(alpha);
    }

    private void SetAlpha(float alpha)
    {
        // Set the alpha value of the SpriteRenderer's color
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
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
