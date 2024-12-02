using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashAnim : MonoBehaviour
{
    public Sprite[] trashCanSprites; // Array of trash can images
    private Image trashCanImage; // Image component of the trash can
    private int itemsDestroyed = 0; // Tracks the total number of destroyed items

    void Start()
    {
        // Get the Image component
        trashCanImage = GetComponent<Image>();

        // Set the initial sprite if available
        if (trashCanSprites.Length > 0)
        {
            trashCanImage.sprite = trashCanSprites[0];
        }
    }

    public void AddItemToTrash()
    {
        // Increment the destroyed items count
        itemsDestroyed++;

        // Update the trash can image after every 2 items
        if (itemsDestroyed % 2 == 0)
        {
            UpdateTrashCanImage(itemsDestroyed / 2);
        }
    }

    private void UpdateTrashCanImage(int spriteIndex)
    {
        // Clamp the sprite index to ensure it stays within bounds
        spriteIndex = Mathf.Clamp(spriteIndex, 0, trashCanSprites.Length - 1);

        // Update the trash can sprite
        trashCanImage.sprite = trashCanSprites[spriteIndex];
    }
}
