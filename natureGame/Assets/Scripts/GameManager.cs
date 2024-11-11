using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float environmentScore = 0f; // Holds the current environmental score
    public Text scoreText; // Reference to the Unity UI Text component for displaying score
    public ObservableCollection<EnvironmentInfluence> influences;

    public GameManager() : base() {
        this.influences = new ObservableCollection<EnvironmentInfluence>();
        this.influences.CollectionChanged += this.InfluencesChanged;
    }

    void InfluencesChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        GetInfluence();
    }

    public void GetInfluence ()
    {
        environmentScore = 0f;
        foreach (EnvironmentInfluence infl in influences)
        {
            environmentScore += infl.influence;

        }
    }

     // Method to directly update the score
    public void UpdateScore(float scoreChange)
    {
        environmentScore += scoreChange; // Update score by the specified amount
        UpdateScoreDisplay(); // Refresh the score display in the UI
        Debug.Log("Environment Score Updated: " + environmentScore); // Log updated score
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + environmentScore.ToString("F0"); // Display score as an integer
        }
    }
    
}
