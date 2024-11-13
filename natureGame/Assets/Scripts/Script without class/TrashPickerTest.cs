using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPickerTest : Item
{
    private bool isClicked = false;
    private Vector3 originalPosition; // Store the original position
    public float resetDistance = 0.5f; // Distance threshold for resetting

    public override void OnStart()
    {
        originalPosition = transform.position; // Store the initial position
        SetTrashPickerColor(Color.red); // Set initial color to red
    }

    private void Update()
    {
        if (isClicked)
        {
            Vector3 mousePos = GetMouseWorldPosition();
            transform.position = mousePos; // Make the object follow the mouse

            // Set color to blue while moving
            SetTrashPickerColor(Color.blue);
        }

        // Reset to original position when pressing the 'B' key
        if (Input.GetKeyDown(KeyCode.B))
        {
            ResetItem();
        }
    }

    private void OnMouseDown()
    {
        // Check if we're clicking near the original position to reset
        if (!isClicked && Vector3.Distance(transform.position, originalPosition) < resetDistance)
        {
            ResetItem();
        }
        else
        {
            OnClick();
        }
    }

    public override void OnClick()
    {
        isClicked = !isClicked; // Toggle the follow behavior
        Debug.Log("ToggleClick called. isClicked is now: " + isClicked);

        if (isClicked)
        {
            Debug.Log("Trash Picker activated and positioned.");
        }
        else
        {
            ResetItem(); // Reset position if unclicked
        }
    }

    private void ResetItem()
    {
        isClicked = false;
        transform.position = originalPosition; // Reset to the original position
        SetTrashPickerColor(Color.red); // Change color to red
        Debug.Log("Trash Picker reset to original position.");
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; // Set z to ensure it's in the correct plane for 2D
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void SetTrashPickerColor(Color color)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = color;
        }
    }
}
