using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
    public Camera cam;
    bool hoversTrash = false;
    bool hoversCan = false;
    GameObject pickedTrashObj;
    bool pickedTrash = false;
    bool clicked = false;
    List<GameObject> hoverTrashList = new List<GameObject> ();
    
    
    public static Picker Instance; // Singleton instance


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

        } 
        else if (Input.GetMouseButtonUp(0))
        {
            if(pickedTrash)
            {
                Trash trash = pickedTrashObj.GetComponent<Trash>();

                 // Ensure the Trash component exists before calling Dropped()
                if (trash != null)
                {
                    if (hoversCan)
                    {
                        GameManager.Instance.IncrementDestroyedCount();

                        Destroy(trash.gameObject);
                        
                    }
                    else
                    {
                        Instance = this;
                        trash.Dropped();
                    }
                }
            }
            clicked = false;
            pickedTrash = false;
            pickedTrashObj = null; // reset the picked GameObject
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
        else if (other.tag == "trashcan")
        {
            hoversCan = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "trash")
        {
            hoverTrashList.Remove(other.gameObject);

        } 
        else if (other.tag == "trashcan")
        {
            hoversCan = false;
        }
    }
}
