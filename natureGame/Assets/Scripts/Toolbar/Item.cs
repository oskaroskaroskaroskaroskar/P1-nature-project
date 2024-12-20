using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public Camera cam;
    public GameObject gameobject;
    public Vector3 listPosition;
    public UnityEngine.UI.Image image;
    public bool mouseOver = false;
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
        if (MouseStuckItem.mouseStuckActive == true && this.GetType().BaseType.Name != "MouseStuckItem") //Checks if a MouseStuckItem is called and deactivates it unles it is a mousestuckitem that is clicked
        {
            FindObjectOfType<MouseStuckItem>().Clicked();
          
        }
        OnClick();
    }
    private void OnMouseEnter()
    {
        mouseOver = true;
    }
    private void OnMouseExit()
    {
        mouseOver = false;
    }
    public void ResetPosition()
    {
        transform.localPosition = listPosition;
    }

}
