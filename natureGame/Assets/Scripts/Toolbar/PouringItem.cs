using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class PouringItem : Item
{
    public bool clicked = false; //Bool for controlling if activated
    public List<GameObject> pouringZones = new List<GameObject>(); //List of dropzone to be registered as its own
    Vector3 lastMousePosition; //variable used to detect change in mouseposition
    public List<GameObject> inDropzones = new List<GameObject>(); //List of own dropzones which item is currently hovering

    public bool fullOpacity = true; //Bool for controlling if opacity is full
    public SpriteRenderer spriteRend; //Reference to sprite renderer
    public UnityEngine.UI.Image imageRend; //Reference to UI-image 

    void Awake()
    {
        SetRenderes();
    }
    public virtual void SetRenderes () //Setting references to renderer and image
    {
        spriteRend = gameObject.GetComponent<SpriteRenderer>();
        imageRend = gameObject.GetComponent<UnityEngine.UI.Image>();
    }


    public override void OnClick() //method triggered when gameobject is clicked
    {
        clicked = true;
    }
    public abstract void Pour(); //Method to be implemented in child
    public virtual void NotPouring() { } //Mehtod to be optionally implemented in child
    void OnDrop() 
    {
        fullOpacity = true;
        if (spriteRend != null)
        {
            spriteRend.color = new Color32(255, 255, 255, 255);
        }
        else if (imageRend != null)
        {
            imageRend.color = new Color32(255, 255, 255, 255); 
        }
        Dropped();
    }
    public virtual void Dropped() { } //Mehtod to be optionally implemented in child

    public void Update()
    {
        if (clicked == true) //Only called if item is active
        {
            // Code to make object follow mouse
            var mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y + GameManager.touchYOffset, 0f);

            if (inDropzones.Count > 0) // Check if the pouring item is in any pouring zone
            {
                Pour(); // Continuously pour when in the pouring zone
                if (!fullOpacity)
                {
                    fullOpacity = true;
                    if (spriteRend != null)
                    {
                        spriteRend.color += new Color32(0, 0, 0, 80);
                    }
                    else if (imageRend != null)
                    {
                        imageRend.color += new Color32(0, 0, 0, 80);
                    }
                }
            }
            else
            {
                NotPouring(); // Stop pouring if not in the pouring zone
                if (fullOpacity)
                {
                    fullOpacity = false;
                    if (spriteRend != null)
                    {
                        spriteRend.color -= new Color32(0, 0, 0, 80);
                    }
                    else if (imageRend != null)
                    {
                        imageRend.color -= new Color32(0, 0, 0, 80);

                    }
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
            clicked = false;
            ResetPosition();
            NotPouring();
            OnDrop();
        }
    }
    

}
