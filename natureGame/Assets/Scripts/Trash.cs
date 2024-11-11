using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public Vector3 position;
    void Start()
    {
        position = this.transform.position;
    }

    public void Dropped()
    {
        this.transform.position = position;
    }
}
