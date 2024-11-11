using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OiilItem : PouringItem
{
    public GameObject oilStain;
    float pourSpeed;
    public override void OnStart()
    {
        pourSpeed = 0.2f;
        cam = Camera.main;
        listPosition = new Vector3(2f, 0, 0);
        ResetPosition();
     
    }
    public override void Pour()
    {
        if (oilStain.transform.localScale.x<1)
        {
            oilStain.transform.localScale += new Vector3(Time.deltaTime*pourSpeed, Time.deltaTime*pourSpeed, 0);
            oilStain.GetComponent<OilPool>().influence += Time.deltaTime * pourSpeed;
            Camera.main.GetComponent<GameManager>().GetInfluence();
        }
    }
}
