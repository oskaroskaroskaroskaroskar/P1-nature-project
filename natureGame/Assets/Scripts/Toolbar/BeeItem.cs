using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeItem : DragAndDropItem
{
    public override void OnStart()
    {
        cam = Camera.main;
    }


}
