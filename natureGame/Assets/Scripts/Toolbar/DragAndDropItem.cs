using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DragAndDropItem : Item
{
    public GameObject dropObject; //object dropped when mouserelease in dropzone
    bool clicked = false;
    protected bool inDropZone = false;
    public List<GameObject> dropzones = new List<GameObject>(); //list of posible zones to drop item

    
    public override void OnClick() //method triggered when gameobject is clicked
    {
        clicked = true;
        EnableDropzones();
    }
   
    void Update()
    {
        if (clicked == true)
        {
            //code to make object follow mouse:
            var mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            gameobject.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0f);

        } 
    }

    void OnTriggerEnter2D(Collider2D other) //method triggered when object enters any collider 
    {
        foreach (GameObject dropzone in dropzones) //runs through list of dropzones
        {
            if (other.gameObject==dropzone) //checks if triggered collider is attached to dropzone
            {
                inDropZone = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) //method triggered when object leaves any collider
    {
        foreach (GameObject dropzone in dropzones) //runs through list of dropzones
        {
            if (other.gameObject == dropzone) //checks if triggered collider is attached to dropzone
            {
                inDropZone = false;
            }
        }
    }

    private void OnMouseUp() //method triggered when mouse release anywhere
    {
        if (clicked == true)
        {
            DisableDropzones();
            clicked = false;
            Released();
        }
    }

    protected virtual void Released() //method called when item is relaesed/dropped
    {
        if (inDropZone == true)
        {
            InstObject();
        }
        ResetPosition();
    }

    private void InstObject () //method called if item is in dropzone and released
    {
        //code to instatiate(=create) dropped object:
        GameObject obj = Instantiate(dropObject);
        obj.transform.position = this.transform.position;
       
    }
    void EnableDropzones ()
    {
        foreach (GameObject DZ in dropzones)
        {
            Dropzone dropzone = DZ.GetComponent<Dropzone>();

            // Ensure the Trash component exists before calling Dropped()
            if (dropzone != null)
            {
                dropzone.Activate();
            }
        }
    }   
    void DisableDropzones ()
    {
        foreach (GameObject DZ in dropzones)
        {
            Dropzone dropzone = DZ.GetComponent<Dropzone>();

            // Ensure the Trash component exists before calling Dropped()
            if (dropzone != null)
            {
                dropzone.DeActivate();
            }
        }
    }
}
