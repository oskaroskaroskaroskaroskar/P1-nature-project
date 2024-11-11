using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour , EnvironmentInfluence
{
    public float influence { get; set; } = 2f;
     private Vector3 initialPosition; // Store the initial position
    private GameManager gameManager;
    
    void Start()
    {
        initialPosition = this.transform.position;
        Camera.main.GetComponent<GameManager>();
    }

    public void Dropped(bool inTrashCan)
    {
        if(inTrashCan)
        {
            gameManager.UpdateScore(influence);
            Destroy(gameObject);
        }
        else
        {
            this.transform.position = initialPosition; // Reset to original position
        }
    }
}
