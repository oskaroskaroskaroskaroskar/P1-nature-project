using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticFlaskItem : DragAndDropItem
{   

    void Start()
    {
        cam = Camera.main;
        name = "PlasticFlask";

        dropzones.Add(GameObject.Find("DZ plastic flask"));
      
    }

  
}
