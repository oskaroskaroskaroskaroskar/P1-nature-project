using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public float distancePerScore = 0.1f; // Distance to move upward per unit of environmentScore
    public float smoothSpeed = 2f; // Speed of smoothing
    private Vector2 initialPosition; // 2D position
    private Vector2 targetPosition; // Target position
    private float lastScore = 0.0f; // Keep track of the last environment score to detect changes

    void Start()
    {
        // Store the initial position of the sun
        initialPosition = transform.position;
        targetPosition = initialPosition;

        // Subscribe to environment score changes
        GameManager.Instance.influences.CollectionChanged += OnEnvironmentScoreChanged;
    }

    private void OnEnvironmentScoreChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        // Update the target position based on the environment score
        targetPosition = initialPosition + new Vector2(0, -GameManager.Instance.environmentScore * distancePerScore);
    }

    void Update()
    {
        // Smoothly interpolate the Y position of the sun
        Vector2 currentPosition = transform.position; // Current 2D position
        transform.position = Vector2.Lerp(currentPosition, targetPosition, Time.deltaTime * smoothSpeed);
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
