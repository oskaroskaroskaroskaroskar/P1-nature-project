using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DragAndDropItem : Item
{
    public GameObject dropObject;
    public bool clicked = false;
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

        } else if (clicked == false)
        {
            if (inDropZone == true)
            {
                InstObject();
            } else
            {
                ResetPosition();
            }
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
        clicked = false;
    }
    void InstObject ()
    {
        GameObject obj = Instantiate(dropObject);
        obj.transform.position = this.transform.position;
        ResetPosition();
    }
    void ResetPosition ()
    {
        transform.position = Vector3.zero;
    }
}
