using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticTrash : Trash, EnvironmentInfluence
{
    public float influence { get; set; } = -1f;
    public override void OnStart()
    {
        Camera.main.GetComponent<GameManager>().influences.Add(this);
        PlasticFlaskItem.count++;
    }
    void OnDisable()
    {
        Camera.main.GetComponent<GameManager>().influences.Remove(this);
        PlasticFlaskItem.count--;
    }
}
