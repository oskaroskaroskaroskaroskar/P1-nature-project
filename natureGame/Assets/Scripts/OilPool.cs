using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilPool : MonoBehaviour , EnvironmentInfluence
{
    public float influence { get; set; } = 0f;
    void Start () 
    {
        Camera.main.GetComponent<GameManager>().influences.Add(this);
       
    }
}
