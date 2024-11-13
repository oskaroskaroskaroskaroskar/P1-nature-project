using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseStuckItem : Item
{
    protected bool isClicked = false;
    public GameObject trashPickerPrefab; // Optional if instantiating
    public GameObject trashPicker; // Assign the triangle prefab here or instantiate it from prefab

    public override void OnStart()
    {

        // Ensure the trashPicker is initialized correctly
        if (trashPicker == null && trashPickerPrefab != null)
        {
            // Instantiate the prefab if trashPicker is not assigned directly
            trashPicker = Instantiate(trashPickerPrefab, listPosition, Quaternion.identity);
        }

        trashPicker.SetActive(false); // Ensure trashPicker is hidden initially
    }

    private void Update()
    {
        if (isClicked && trashPicker != null)
        {
            Vector3 mousePos = GetMouseWorldPosition();
            trashPicker.transform.position = mousePos;

            SpriteRenderer sr = trashPicker.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = Color.blue;
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Reset Position Triggered");
            ResetItem();
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse Down on TrashPickerItem");
        OnClick();
    }

    public override void OnClick()
    {
        isClicked = !isClicked;
        Debug.Log("ToggleClick called. isClicked is now: " + isClicked);

        if (isClicked)
        {
            trashPicker.SetActive(true);
            trashPicker.transform.position = transform.position;
            Debug.Log("Trash Picker activated and positioned.");
        }
        else
        {
            ResetItem();
        }
    }

    private void ResetItem()
    {
        isClicked = false;
        trashPicker.SetActive(false);
        SpriteRenderer sr = trashPicker.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Color.red;
        }
        ResetPosition();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; // Set z to ensure it's in the correct plane for 2D
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    // Implement the abstract method from Item

}
