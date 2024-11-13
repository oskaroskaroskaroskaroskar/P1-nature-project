using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
    public Camera cam;
    bool hoversTrash = false;
    GameObject hoveredTrash;
    bool pickedTrash = false;
    bool clicked = false;
    void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicked = true;
            if (hoversTrash)
            {
                pickedTrash = true;
            }

        } else if (Input.GetMouseButtonUp(0))
        {
            clicked = false;
            pickedTrash = false;
        }
        if (pickedTrash)
        {
            var mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            hoveredTrash.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0f);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "trash" && !clicked)
        {

            hoversTrash = true;
            hoveredTrash = other.gameObject;

            //other.gameObject.SetActive(false); // Pick up the trash (hide or destroy)
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "trash")
        {
            hoversTrash = false;
        }
    }
   
}
