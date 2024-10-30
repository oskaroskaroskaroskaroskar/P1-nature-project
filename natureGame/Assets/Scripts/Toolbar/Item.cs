using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public Camera cam;
    public GameObject gameobject;
    public abstract void OnClick();
    public BoxCollider2D collider2D;
    void OnMouseDown ()
    {
        OnClick();
    }
    void Start ()
    {
       
    }

}
