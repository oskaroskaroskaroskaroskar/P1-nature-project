using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticFlaskItem : DragAndDropItem
{   

    void Start()
    {
        cam = Camera.main;
        name = "PlasticFlask";
        Dropzones.Add(new DropZone(100f, 100f, 0f, 0f));
    
    }

    void Update()
    {
        if(clicked == true)
        {
            var mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            gameobject.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y,0f);
           
        }
    }
}
