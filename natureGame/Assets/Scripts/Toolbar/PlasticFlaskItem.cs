using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticFlaskItem : DragAndDropItem
{   

    void Start()
    {
        ResetPosition();
        cam = Camera.main;
        listPosition = new Vector3(0,0,0);
    }


  
}
