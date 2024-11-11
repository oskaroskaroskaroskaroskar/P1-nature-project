using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticFlaskItem : DragAndDropItem
    {   
     private GameManager gameManager;
     public float scoreDeduction = -5f; // Points deducted for dropping plastic bottle in drop zone

    public override void OnStart()
    {
        ResetPosition();
        cam = Camera.main;
        gameManager = Camera.main.GetComponent<GameManager>();
    }

    protected override void Released()
    {
        if (inDropZone) // Deduct points only if not yet deducted
        {
            gameManager.UpdateScore(scoreDeduction); // Deduct points for dropping in drop zone
        }
        base.Released(); // Handle the rest in DragAndDropItem, including cloning
    }

}
