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
    void Start()
    {
        position = this.transform.position;
        Camera.main.GetComponent<GameManager>().influences.Add(this);
    }

    public void Dropped()
    {
        this.transform.position = position;
    }
   
    void OnDisable()
    {
        Camera.main.GetComponent<GameManager>().influences.Remove(this);
    }

}
