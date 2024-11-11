using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float environmentScore = 0f;
    
    public ObservableCollection<EnvironmentInfluence> influences;
    public GameManager() : base() {
        this.influences = new ObservableCollection<EnvironmentInfluence>();
        this.influences.CollectionChanged += this.InfluencesChanged;
    }
    void Start()
    {
      
    }

    void Update()
    {
        
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
}
