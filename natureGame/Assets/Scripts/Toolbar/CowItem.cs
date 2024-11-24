using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowItem : DragAndDropItem
{
    public static int count = 0;
    public override void OnStart()
    {
        maxCount = 4;
        cam = Camera.main;
    }
    public override bool FilledUp()
    {
        if (count < maxCount)
        {
            return false;
        }
        else return true;
    }
}
