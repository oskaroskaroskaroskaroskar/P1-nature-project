using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageTrash : Trash, EnvironmentInfluence
{
    public float influence { get; set; } = -0.5f;
    public override void OnStart ()
    {
        Camera.main.GetComponent<GameManager>().influences.Add(this);
        GarbageItem.count++;
    }
    void OnDisable()
    {
        Camera.main.GetComponent<GameManager>().influences.Remove(this);
        GarbageItem.count--;
    }
}
