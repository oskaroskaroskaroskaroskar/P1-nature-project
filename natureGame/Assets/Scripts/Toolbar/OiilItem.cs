using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OiilItem : PouringItem
{
    void Start()
    {
     
        cam = Camera.main;
        listPosition = new Vector3(2f, 0, 0);
        ResetPosition();
    }
}
