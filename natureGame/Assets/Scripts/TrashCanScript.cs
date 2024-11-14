using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanScript : MonoBehaviour
{
    private Picker picker;
    private GameObject trashInCan; // To store the trash object in the trash can area

    void Start()
    {
        // Find the Picker script in the scene
        picker = FindObjectOfType<Picker>();
        Debug.Log("TrashCanScript: Picker script found.");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if an item with "trash" tag has entered the trash can area
        GameObject pickedTrash = picker.GetPickedTrash();

        if (pickedTrash != null && pickedTrash == other.gameObject && pickedTrash.CompareTag("trash"))
        {

            // Store the object in the trash can area
            trashInCan = pickedTrash;
            Debug.Log("TrashCanScript: Trash item entered trash can area.");

            Trash trashComponent = trashInCan.GetComponent<Trash>();

            if (trashComponent != null)
            {
                trashComponent.inTrashCan = true; // Set flag to indicate it's in the trash can
            }

        }
        else
        {
            Debug.Log("TrashCanScript: Object entered trash can area but is not picked trash or lacks 'trash' tag.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // If the trash object leaves the trash can area, clear the reference
        if (trashInCan == other.gameObject)
        {
            Trash trashComponent = trashInCan.GetComponent<Trash>();
            
            if (trashComponent != null)
            {
                trashComponent.inTrashCan = false; // Clear the flag when it exits the trash can
            }
            
            Debug.Log("TrashCanScript: Trash item exited trash can area.");
            trashInCan = null;
        }
    }

    void Update()
    {
        // Check if the mouse button has been released and an item is in the trash can area
        if (trashInCan != null && Input.GetMouseButtonUp(0))
        {
            Debug.Log("TrashCanScript: Dropping and destroying trash item in trash can.");
            Destroy(trashInCan); // Destroy the trash item
            trashInCan = null;   // Clear the reference
        }
    }
}
