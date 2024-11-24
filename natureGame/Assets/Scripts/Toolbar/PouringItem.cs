using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class PouringItem : Item
{
    bool clicked = false;
    public List<GameObject> pouringZones = new List<GameObject>();
    Vector3 lastMousePosition; //variable used to detect change in mouseposition
    float pouringTimer = 0f; //used to make delay for pouring
    public List<GameObject> inDropzones = new List<GameObject>();

    public override void OnClick() //method triggered when gameobject is clicked
    {
        clicked = true;
        EnableDropzones();
    }
    public abstract void Pour();
    public virtual void NotPouring() { }
    public virtual void Dropped() { }

    public void Update()
    {
        if (clicked == true)
        {
            // Code to make object follow mouse
            var mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0f);

            // Check if the oilcan is in any pouring zone
            if (inDropzones.Count > 0)
            {
                Pour(); // Continuously pour when in the pouring zone
            }
            else
            {
                NotPouring(); // Stop pouring if not in the pouring zone
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) //method triggered when object enters any collider 
    {
        foreach (GameObject pouringZone in pouringZones) //runs through list of pouring zones
        {
            if (other.gameObject == pouringZone) //checks if triggered collider is attached to pouring zone
            {
                inDropzones.Add(pouringZone);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other) //method triggered when object leaves any collider 
    {
        foreach (GameObject pouringZone in pouringZones) //runs through list of pouring zones
        {
            if (other.gameObject == pouringZone) //checks if triggered collider is attached to pouring zone
            {
                inDropzones.Remove(pouringZone);
            }
        }
    }
    
    public void OnMouseUp() //method triggered when mouse release anywhere
    {
        if (clicked == true)
        {
            DisableDropzones();
            clicked = false;
            ResetPosition();
            NotPouring();
            Dropped();
        }
    }
    void EnableDropzones()
    {
        foreach (GameObject DZ in pouringZones)
        {
            Dropzone dropzone = DZ.GetComponent<Dropzone>();

            // Ensure the Trash component exists before calling Dropped()
            if (dropzone != null)
            {
                dropzone.Activate();
            }
        }
    }
    void DisableDropzones()
    {
        foreach (GameObject DZ in pouringZones)
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
