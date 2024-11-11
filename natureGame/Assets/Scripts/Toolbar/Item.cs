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
        OnStart();
    }
    public abstract void OnStart();
    public abstract void OnClick();
    void OnMouseDown ()
    {
      // if(Trashpicker==false||Item.GetType()=="mouseStuckItem") 
        OnClick();
    }
    public void ResetPosition()
    {
        transform.position = listPosition;
    }

}
