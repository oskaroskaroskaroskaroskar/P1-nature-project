using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public Camera cam;
    public GameObject gameobject;
    public Vector3 listPosition;
    void Start()
    {

        listPosition = transform.localPosition;
        OnStart();
    }
    public abstract void OnStart();
    public abstract void OnClick();
    void OnMouseDown ()
    {
        if (MouseStuckItem.mouseStuckActive == false)
        {
            Debug.Log(this.GetType().BaseType.Name);
            OnClick();
        } else
        {
            Debug.Log("klikket");
            FindObjectOfType<MouseStuckItem>().Clicked();
        }
    }
    public void ResetPosition()
    {
        transform.localPosition = listPosition;
    }

}
