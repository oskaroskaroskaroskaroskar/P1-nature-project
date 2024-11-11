using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OiilItem : PouringItem
{
    public override void OnStart()
    {
     
        cam = Camera.main;
        listPosition = new Vector3(2f, 0, 0);
        ResetPosition();
    }
}
