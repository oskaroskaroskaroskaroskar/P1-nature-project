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
    Color32 highLightColor;

    float upperYCord;
    public override void OnStart()
    {
        upperYCord = gameobject.transform.position.y+0.3f;

        highLightColor = highlight.GetComponent<SpriteRenderer>().color;
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
        Vector3 mousePos = GetMouseWorldPosition();
        if (isClicked && trashPicker != null && mousePos.y > upperYCord )
        {
            
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
        if (mouseOver&&clickedDown&&!isClicked)
        {
            highlight.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255,140);
        } else if (!isClicked)
        {

            highlight.GetComponent<SpriteRenderer>().color = highLightColor;
        }
    }

    public override void OnClick()
    {
        Clicked();
    }
   
    public void Clicked()
    {

        isClicked = !isClicked;

        if (isClicked) // If picker is active
        {
            //highlight.GetComponent<Renderer>().enabled = true;
            highlight.GetComponent<SpriteRenderer>().color = new Color32( 255, 255, 255, 255);
            trashPicker.SetActive(true);
            mouseStuckActive = true;
            trashPicker.transform.position = new Vector3(0,0,9.7f);
        }
        else
        {
            //highlight.GetComponent<Renderer>().enabled = false;
            highlight.GetComponent<SpriteRenderer>().color = highLightColor;
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
