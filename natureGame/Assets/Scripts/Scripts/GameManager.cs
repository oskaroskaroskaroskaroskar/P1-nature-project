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
    
    
    public static GameManager Instance; // Singleton instance
    private int destroyedCount = 0; // Counter for destroyed objects
    public static float touchYOffset = 0.7f;

    public GameObject trashItem;
    public GameManager() : base() {
        this.influences = new ObservableCollection<EnvironmentInfluence>();
        this.influences.CollectionChanged += this.InfluencesChanged;
    }
    private void Start()
    {
        if (trashItem!=null)
        {
            List<GameObject> dropZones = new List<GameObject>();
            dropZones = trashItem.GetComponent<GarbageItem>().dropzones;
            float dropArea = 0;
            foreach (GameObject dropZone in dropZones)
            {
               dropArea += dropZone.transform.localScale.x*dropZone.transform.localScale.y;
            }

        }
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
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + environmentScore.ToString("F0");
            //Debug.Log("Score Text Updated: " + scoreText.text);
        }
        else
        {
            Debug.Log("Score Text is null.");
        }
    }



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementDestroyedCount()
    {
        destroyedCount++;

        // Here you can trigger events, such as updating a trash can image
        if (destroyedCount % 1 == 0)
        {
            FindObjectOfType<TrashAnim>().AddItemToTrash();
        }
    }

    public int GetDestroyedCount()
    {
        return destroyedCount;
    }

}
