using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MouseStuckItem : Item
{


    private bool isStuckToMouse = false;
    public GameObject trashPicker;

    private void Start()
    {
        // Initialize the red object, make it initially inactive
        trashPicker = Instantiate(gameObject); // Duplicate this object
        trashPicker.GetComponent<Renderer>().material.color = Color.red; // Set color to red
        trashPicker.SetActive(false); // Hide the red object initially

    }


    public override void OnClick()
    {

        if (isStuckToMouse)
        {
            isStuckToMouse = true;
            trashPicker.transform.position = transform.position;
            trashPicker.SetActive(true);
        }
        else
        {
            isStuckToMouse = false;
            trashPicker.SetActive(false);
        }


    }


    private void TrashPickerUpdate()
    {
        if (isStuckToMouse)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        }
    }
}
