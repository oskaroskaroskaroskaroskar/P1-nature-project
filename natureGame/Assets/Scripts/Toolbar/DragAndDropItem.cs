using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DragAndDropItem : Item
{
    public GameObject dropObject; //object dropped when mouserelease in dropzone
    bool clicked = false;
    public List<GameObject> dropzones = new List<GameObject>(); //list of posible zones to drop item
    public List<GameObject> inDropzones = new List<GameObject>();
    public int maxCount;
    

    bool fullOpacity = true;
    SpriteRenderer spriteRend;
    UnityEngine.UI.Image Image;
    void Awake()
    {
        spriteRend = gameObject.GetComponent<SpriteRenderer>();
        Image = gameObject.GetComponent<UnityEngine.UI.Image>();
    }
    public override void OnClick() //method triggered when gameobject is clicked
    {
        if (!FilledUp())
        {
            clicked = true;
        }
    }
   
    void Update()
    {
        if (clicked)
        {
            //code to make object follow mouse:
            var mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            gameobject.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y+GameManager.touchYOffset, 0);

            if (inDropzones.Count > 0 && !fullOpacity) //if hovers a dropzone and transparent
            {
                fullOpacity = true;
                if (spriteRend != null) 
                {
                    gameObject.GetComponent<SpriteRenderer>().color += new Color32(0, 0, 0, 100);
                }
                else if (image != null)
                {
                    image.color += new Color32(0, 0, 0, 100);
                }
            } else if (inDropzones.Count == 0 && fullOpacity) //if hovers a dropzone and not transparent
            {
                fullOpacity = false;
                if (spriteRend != null)
                {
                    gameObject.GetComponent<SpriteRenderer>().color -= new Color32(0, 0, 0, 100);
                }
                else if (image != null)
                {
                    image.color -= new Color32(0, 0, 0, 100);
                }
            }
        }
        if (!clicked)
        {
            fullOpacity = true;
            if (FilledUp())
            {
                image.color = new Color32(105, 105, 105, 255);
            }
            else
            {
                image.color = new Color32(255, 255, 225, 255);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other) //method triggered when object enters any collider 
    {
        foreach (GameObject dropzone in dropzones) //runs through list of dropzones
        {
            if (other.gameObject==dropzone) //checks if triggered collider is attached to dropzone
            {
                inDropzones.Add(dropzone);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) //method triggered when object leaves any collider
    {
        foreach (GameObject dropzone in dropzones) //runs through list of dropzones
        {
            if (other.gameObject == dropzone) //checks if triggered collider is attached to dropzone
            {
                inDropzones.Remove(dropzone);
            }
        }
    }
   
    private void OnMouseUp() //method triggered when mouse release anywhere
    {
        if (clicked == true)
        {
            clicked = false;
            if (FilledUp())
            {
                image.color = new Color32(105, 105, 105, 255);
            }
            else
            {
                image.color = new Color32(255, 255, 225, 255);
            }
            Released();
        }
    }

    protected virtual void Released() //method called when item is relaesed/dropped
    {
        if (inDropzones.Count > 0)
        {
            InstObject();
        }
        
        ResetPosition();
    }

    private void InstObject () //method called if item is in dropzone and released
    {
        //code to instatiate(=create) dropped object:
        GameObject obj = Instantiate(dropObject);
       
        obj.transform.position = new Vector3(this.transform.position.x,this.transform.position.y, (float)ZDisplacement());
       
    }
    public virtual float ZDisplacement()
    {
        return 0f;
    }

    public abstract bool FilledUp();
}
