using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public abstract class MouseStuckItem : Item
{

    public static bool mouseStuckActive = false;
    protected bool isClicked = false;
    bool clickedDown = false;
    public GameObject trashPickerPrefab; // Optional if instantiating
    public GameObject trashPicker; // Assign the triangle prefab here or instantiate it from prefab
    public GameObject highlight;
    
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
            // Debug.Log("Reset Position Triggered");
            ResetItem();
        }
    }

    public override void OnClick()
    {
        clickedDown = true;
    }
    public void OnMouseUp()
    {
        if (clickedDown)
        {
            clickedDown = false;
            Clicked();
        }
    }
    public void Clicked()
    {
        isClicked = !isClicked;
        // Debug.Log("ToggleClick called. isClicked is now: " + isClicked);

        if (isClicked)
        {
            highlight.GetComponent<Renderer>().enabled = true;
            trashPicker.SetActive(true);
            mouseStuckActive = true;
            trashPicker.transform.position = transform.position;
            // Debug.Log("Trash Picker activated and positioned.");
        }
        else
        {
            highlight.GetComponent<Renderer>().enabled = false;
            ResetItem();
        }
    }

    private void ResetItem()
    {
        isClicked = false;
        trashPicker.SetActive(false);
        mouseStuckActive = false;
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
