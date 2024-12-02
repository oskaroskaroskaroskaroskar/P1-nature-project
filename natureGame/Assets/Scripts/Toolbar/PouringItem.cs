using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class PouringItem : Item
{
    public bool clicked = false;
    public List<GameObject> pouringZones = new List<GameObject>();
    Vector3 lastMousePosition; //variable used to detect change in mouseposition
    float pouringTimer = 0f; //used to make delay for pouring
    public List<GameObject> inDropzones = new List<GameObject>();

    public bool fullOpacity = true;
    public SpriteRenderer spriteRend;
    public UnityEngine.UI.Image Image;

    void Awake()
    {
        spriteRend = gameObject.GetComponent<SpriteRenderer>();
        Image = gameObject.GetComponent<UnityEngine.UI.Image>();
    }

    public override void OnClick() //method triggered when gameobject is clicked
    {
        clicked = true;
        EnableDropzones();
    }
    public abstract void Pour();
    public virtual void NotPouring() { }
    void OnDrop()
    {
        fullOpacity = true;
        if (spriteRend != null)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
        else if (image != null)
        {
            image.color = new Color32(255, 255, 255, 255); 
        }
        Dropped();
    }
    public virtual void Dropped() { }

    public void Update()
    {
        if (clicked == true)
        {
            // Code to make object follow mouse
            var mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y + GameManager.touchYOffset, 0f);

            // Check if the oilcan is in any pouring zone
            if (inDropzones.Count > 0)
            {
                Pour(); // Continuously pour when in the pouring zone
                if (!fullOpacity)
                {
                    fullOpacity = true;
                    if (spriteRend != null)
                    {
                        gameObject.GetComponent<SpriteRenderer>().color += new Color32(0, 0, 0, 80);
                    } 
                    else if (image != null)
                    {
                        image.color += new Color32(0, 0, 0, 80); 
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
                        gameObject.GetComponent<SpriteRenderer>().color -= new Color32(0, 0, 0, 80);
                    }
                    else if (image != null)
                    {
                        image.color -= new Color32(0, 0, 0, 80); 
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
            DisableDropzones();
            clicked = false;
            ResetPosition();
            NotPouring();
            OnDrop();
        }
    }
    void EnableDropzones()
    {
        foreach (GameObject DZ in pouringZones)
        {
            Dropzone dropzone = DZ.GetComponent<Dropzone>();

        }
    }
    void DisableDropzones()
    {
        foreach (GameObject DZ in pouringZones)
        {
            Dropzone dropzone = DZ.GetComponent<Dropzone>();

        }
    }

}
