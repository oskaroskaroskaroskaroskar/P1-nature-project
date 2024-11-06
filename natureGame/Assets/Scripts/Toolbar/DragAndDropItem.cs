using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DragAndDropItem : Item
{
    public GameObject dropObject;
    bool clicked = false;
    bool inDropZone = false;
    public List<GameObject> dropzones = new List<GameObject>();
    public override void OnClick()
    {
        clicked = true;

    
    }
    void Update()
    {
        if (clicked == true)
        {
            var mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            gameobject.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0f);

        } 
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        foreach (GameObject dropzone in dropzones)
        {
            if (other.gameObject==dropzone)
            {
                inDropZone = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        foreach (GameObject dropzone in dropzones)
        {
            if (other.gameObject == dropzone)
            {
                inDropZone = false;
            }
        }
    }
    private void OnMouseUp()
    {
        if (clicked == true)
        {
            clicked = false;
            Released();
        }
    }
    void Released ()
    {
        if (inDropZone == true)
        {
            InstObject();
        }
        ResetPosition();
    }
    void InstObject ()
    {
        GameObject obj = Instantiate(dropObject);
        obj.transform.position = this.transform.position;
       
    }
   
}
