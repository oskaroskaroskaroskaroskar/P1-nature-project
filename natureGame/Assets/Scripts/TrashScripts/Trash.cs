using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trash : MonoBehaviour , EnvironmentInfluence
{
    public float influence { get; set; } = -1f;
    public Vector3 position;
    public bool inTrashCan = false;
   


    void Start()
    {
        position = this.transform.position;
        Camera.main.GetComponent<GameManager>().influences.Add(this);

        bool b = new System.Random().Next(2) == 0;
        if (b) 
        { 
            this.transform.Rotate(0,0,180f);
        }

        
        PlasticFlaskItem.count++;
    }
    void Update()
    {
        float yPosition=this.transform.position.y;
        if (yPosition>0)
        {
            yPosition = 0;
        }
        float scale = 0.4f - yPosition/2.5f;
        this.transform.localScale = new Vector3(scale, scale,1);
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
        PlasticFlaskItem.count--;
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
