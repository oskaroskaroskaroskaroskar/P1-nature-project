using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropzone : MonoBehaviour
{
    public void Activate() 
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    public void DeActivate() 
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
