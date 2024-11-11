using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class PouringItem : Item
{
    bool clicked = false;
    bool inPourZone = false;
    public List<GameObject> pouringZones = new List<GameObject>();
    Vector3 lastMousePosition; //variable used to detect change in mouseposition
    float pouringTimer = 0f; //used to make delay for pouring

    public override void OnClick() //method triggered when gameobject is clicked
    {
        clicked = true;
    }
    public abstract void Pour();

    void Update()
    {
        if (clicked == true)
        {
            //code to make object follow mouse:
            var mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            gameobject.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0f);

            if (lastMousePosition != mouseWorldPosition) // checks if mouse is moving
            {
                lastMousePosition = mouseWorldPosition;
                pouringTimer = 0f;

            }
            else if (inPourZone == true) // else if mouse is still, checks if mouse is also in pouring zone
            {
                if (pouringTimer > 0.3) //amount of seconds before pouring starts when holding still
                {
                    Pour();
                }
                else
                {
                    pouringTimer += Time.deltaTime;
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other) //method triggered when object enters any collider 
    {
        foreach (GameObject pouringZone in pouringZones) //runs through list of pouring zones
        {
            if (other.gameObject == pouringZone) //checks if triggered collider is attached to pouring zone
            {
                inPourZone = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other) //method triggered when object leaves any collider 
    {
        foreach (GameObject pouringZone in pouringZones) //runs through list of pouring zones
        {
            if (other.gameObject == pouringZone) //checks if triggered collider is attached to pouring zone
            {
                inPourZone = false;
            }
        }
    }
    private void OnMouseUp() //method triggered when mouse release anywhere
    {
        if (clicked == true)
        {
            clicked = false;
            ResetPosition();
        }
    }
   
}
