using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public Camera cam;
    public GameObject gameobject;
    public Vector3 listPosition;
    public abstract void OnClick();
    void OnMouseDown ()
    {
        OnClick();
    }
    public void ResetPosition()
    {
        transform.position = listPosition;
    }

}
