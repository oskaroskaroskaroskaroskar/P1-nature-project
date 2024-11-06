using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private bool isStuckToMouse = false;
    public GameObject trashPickerPrefab;
    public GameObject trashPicker;
    private Vector3 initialPickupPosition;

    private void Start()
    {
        // Initialize the red object, make it initially inactive
        trashPicker = Instantiate(trashPickerPrefab); // Duplicate this object
        trashPicker.GetComponent<Renderer>().material.color = Color.red; // Set color to red
        trashPicker.SetActive(false); // Hide the red object initially

    }


    public void OnMouseDown()
    {
        Debug.Log("work Trash");


        isStuckToMouse = !isStuckToMouse;

        if (isStuckToMouse)
        {
            isStuckToMouse = true;
            initialPickupPosition = GetMouseWorldPosition();
            trashPicker.transform.position = transform.position;
            trashPicker.SetActive(true);
        }
        else
        {
            if (Vector3.Distance(GetMouseWorldPosition(), initialPickupPosition) < 0.2f)
            {
                isStuckToMouse = false;
                trashPicker.SetActive(false);
            }
        }

    }


    private void Update()
    {
        if (isStuckToMouse)
        {
            // Make trashPicker follow the mouse
            trashPicker.transform.position = GetMouseWorldPosition();
        }

    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
