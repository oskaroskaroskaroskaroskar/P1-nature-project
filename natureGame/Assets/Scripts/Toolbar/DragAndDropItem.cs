using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DragAndDropItem : Item
{
    public bool clicked = false;
    public List<DropZone> Dropzones = new List<DropZone>();
    public override void OnClick()
    {
        clicked = true;
    }
    void Update()
    {
        if (clicked == true)
        {
            gameobject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);

        }
    }
}
