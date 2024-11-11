using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPickerItem : MouseStuckItem
{
    private Trash currentTrashItem; // Reference to the trash item being picked up
    private bool isHoldingTrash = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the collider is a Trash item
        Trash trashItem = other.GetComponent<Trash>();
        if (trashItem != null && !isHoldingTrash) // Only pick up if not already holding trash
        {
            currentTrashItem = trashItem;
            isHoldingTrash = true;
            trashItem.transform.SetParent(transform); // Attach the trash item to the picker
            trashItem.transform.localPosition = Vector3.zero; // Center it on the picker
        }
    }

    private void Update()
    {
        // This Update does not interfere with movement; it only checks for releasing the trash
        if (Input.GetMouseButtonUp(0) && isHoldingTrash)
        {
            ReleaseTrash();
        }
    }

    private void ReleaseTrash()
    {
        if (currentTrashItem != null)
        {
            // Check if the trash item is in the trash can
            Collider2D[] hitColliders = Physics2D.OverlapPointAll(transform.position);
            bool inTrashCan = false;
            
            foreach (Collider2D collider in hitColliders)
            {
                if (collider.GetComponent<TrashCanScript>() != null) // Check if the collider belongs to the trash can
                {
                    inTrashCan = true;
                    break;
                }
            }

            if (inTrashCan)
            {
                currentTrashItem.Dropped(true); // Dispose of the trash if in the trash can
            }
            else
            {
                currentTrashItem.Dropped(false); // Reset if dropped outside
            }

            currentTrashItem.transform.SetParent(null); // Detach from picker
            currentTrashItem = null;
            isHoldingTrash = false; // Reset holding state
        }
    }
}
