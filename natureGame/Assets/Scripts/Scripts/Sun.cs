using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public float distancePerScore = 0.1f; // Distance to move upward per unit of environmentScore
    public float smoothSpeed = 2f; // Speed of smoothing
    public float minYPosition = 0.0f; // Minimum Y position

    private Vector2 initialPosition; // Initial position of the Sun
    private Vector2 targetPosition; // Target position of the Sun
    private float lastScore = 0.0f; // Keep track of the last environment score

    void Start()
    {
        // Store the initial position of the Sun object
        initialPosition = transform.position;
        targetPosition = initialPosition;

        // Log an error if GameManager is not initialized
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance is null. Ensure it is initialized in the scene.");
        }
    }

    void Update()
    {
        // Check for changes in environmentScore
        if (GameManager.Instance != null)
        {
            float currentScore = GameManager.Instance.environmentScore;

            // Update the target position based on the score
            if (currentScore != lastScore)
            {
                UpdateTargetPosition(currentScore);
                lastScore = currentScore;
            }

            SmoothMovement();
        }
    }

    public void SmoothMovement()
    {
        // Smoothly interpolate the position of the Sun object to the target position
        Vector2 currentPosition = transform.position;
        transform.position = Vector2.Lerp(currentPosition, targetPosition, Time.deltaTime * smoothSpeed);
    }

    private void UpdateTargetPosition(float score)
    {
        // Calculate the target Y position to move upward as the score increases
        float targetYPosition = initialPosition.y + score * distancePerScore;

        // Clamp the Y position so it does not go below the minimum
        targetYPosition = Mathf.Max(targetYPosition, minYPosition);

        // Set the target position
        targetPosition = new Vector2(initialPosition.x, targetYPosition);
    }
}
