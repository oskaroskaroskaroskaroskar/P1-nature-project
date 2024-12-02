using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
    public Camera cam;
    bool hoversTrash = false;
    bool hoversCan = false;
    //GameObject pickedTrashObj;
    bool pickedTrash = false;
    bool clicked = false;
    List<GameObject> hoverTrashList = new List<GameObject> ();
    List<Trash> pickedTrashList = new List<Trash>();
    
    public static Picker Instance; // Singleton instance


    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if(pickedTrash)
            {
                List<Trash> removeList = new List<Trash>();
                foreach (Trash trash in pickedTrashList)
                {
                    removeList.Add(trash);

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
                foreach (Trash trash in removeList)
                {

                    pickedTrashList.Remove(trash);
                }

            }
            clicked = false;
            pickedTrash = false;
        }

        if (pickedTrash)
        {
            foreach (Trash trash in pickedTrashList)
            {
                var mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
                trash.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0f);

            }
        }
    }
    public void TrashPicked(Trash trash)
    {
        pickedTrash = true;
        pickedTrashList.Add(trash);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
         if (other.tag == "trashcan")
        {
            hoversCan = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "trashcan")
        {
            hoversCan = false;
        }
    }
    private void OnMouseUp()
    {
        if (pickedTrash)
        {
            List<Trash> dropList = new List<Trash>();
            foreach (Trash trash in pickedTrashList)
            {
                trash.Dropped();
            }


            foreach (Trash trash in dropList)
            {
                pickedTrashList.Remove(trash);
            }
            pickedTrash = false;
        }
    }       

}
