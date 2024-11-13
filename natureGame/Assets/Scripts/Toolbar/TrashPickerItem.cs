using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPickerItem : MouseStuckItem
{
    public override void OnStart()
    {
        base.OnStart();

        // Ensure TrashPicker has a Collider2D and Rigidbody2D
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider2D>();
        }
        collider.isTrigger = true; // Set collider as trigger

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.isKinematic = true; // Set Rigidbody to kinematic
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isClicked == true && other.tag == "trash")
        {
            Debug.Log("Trash picked up: " + other.name);
            //other.gameObject.SetActive(false); // Pick up the trash (hide or destroy)
        }
    }
}
