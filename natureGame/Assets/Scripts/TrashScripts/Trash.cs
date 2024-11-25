using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public abstract class Trash : MonoBehaviour
{
    
    public Vector3 position;
    public bool inTrashCan = false;
    int xTwist = 1;


    void Start()
    {

        position = this.transform.position;

        bool b = new System.Random().Next(2) == 0;
        if (b)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
            xTwist = -1;
            
        }
        OnStart();
    }
    public abstract void OnStart();
    void Update()
    {
        fixScale();
    }
    public void OnMouseDown()
    {


        if (MouseStuckItem.mouseStuckActive)
        {
            GameObject.Find("PickerOpen(Clone)").GetComponent<Picker>().TrashPicked(this);
        }
    }

        void fixScale()
    {
        float yPosition = this.transform.position.y;
        if (yPosition > 0)
        {
            yPosition = 0;
        }
        float scale = 0.4f - yPosition / 2.5f;
        this.transform.localScale = new Vector3(xTwist * scale, scale, 1);

    }
    public void Dropped()
    {   
        if (!inTrashCan)
        {
            
            this.transform.position = position;
            fixScale();
        }
        
    }
    void OnDestroy()
    {
        // Notify the TrashManager when this object is destroyed
        if (Picker.Instance != null)
        {
            GameManager.Instance.IncrementDestroyedCount();
        }
    }

}
