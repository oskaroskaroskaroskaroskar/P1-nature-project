using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouringItem : Item
{
    bool clicked = false;
    bool inPourZone = false;
    public List<GameObject> pouringZones = new List<GameObject>();

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
        foreach (GameObject pouringZone in pouringZones)
        {
            if (other.gameObject == pouringZone)
            {
                inPourZone = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        foreach (GameObject pouringZone in pouringZones)
        {
            if (other.gameObject == pouringZone)
            {
                inPourZone = false;
            }
        }
    }
    private void OnMouseUp()
    {
        if (clicked == true)
        {
            clicked = false;
            ResetPosition();
        }
    }
   
}
