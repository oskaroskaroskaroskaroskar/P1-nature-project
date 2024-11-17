using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour , EnvironmentInfluence
{
    public float influence { get; set; } = -1f;
    /*{ 
        set { influence = 2f; } 
        get { return influence; }
    }*/
    public Vector3 position;
    public bool inTrashCan = false;



    void Start()
    {
        position = this.transform.position;
        Camera.main.GetComponent<GameManager>().influences.Add(this);
    }

    public void Dropped()
    {   
        if (!inTrashCan)
        {
            this.transform.position = position;
        }
        
    }
   
    void OnDisable()
    {
        Camera.main.GetComponent<GameManager>().influences.Remove(this);
    }
    
    
    void OnDestroy()
    {
        // Notify the TrashManager when this object is destroyed
        if (Picker.Instance != null)
        {
            GameManager.Instance.IncrementDestroyedCount();
        }
    }

}
