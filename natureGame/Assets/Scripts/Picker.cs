using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
    public Camera cam;
    bool hoversTrash = false;
    GameObject hoveredTrash;
    GameObject pickedTrashObj;
    bool pickedTrash = false;
    bool clicked = false;
    List<GameObject> hoverTrashList = new List<GameObject> ();
    void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !clicked)
        {
            clicked = true;
            foreach (GameObject trash in hoverTrashList)
            {
                pickedTrashObj = trash;
                pickedTrash = true; 
            }

        } else if (Input.GetMouseButtonUp(0))
        {
            if(pickedTrash)
            {
               // Trash trash = pickedTrashObj(Trash);
            }
            clicked = false;
            pickedTrash = false;
        }
        if (pickedTrash)
        {
            var mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            pickedTrashObj.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0f);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "trash")
        {
             hoverTrashList.Add(other.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "trash")
        {
            hoverTrashList.Remove(other.gameObject);
        }
    }
   
}
