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
        UpdateScoreDisplay();
    }

    public void GetInfluence ()
    {
        environmentScore = 0f;
        foreach (EnvironmentInfluence infl in influences)
        {
            environmentScore += infl.influence;

        }
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + environmentScore.ToString("F0");
            Debug.Log("Score Text Updated: " + scoreText.text);
        }
        else
        {
            Debug.Log("Score Text is null.");
        }
    }


}
