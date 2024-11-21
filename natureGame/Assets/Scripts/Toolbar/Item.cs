using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public Camera cam;
    public GameObject gameobject;
    public Vector3 listPosition;
    public UnityEngine.UI.Image image;
    void Start()
    {
        if (gameObject!=null&& gameObject.GetComponent<UnityEngine.UI.Image>() != null)
        {
            image = gameObject.GetComponent<UnityEngine.UI.Image>();
        }


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
