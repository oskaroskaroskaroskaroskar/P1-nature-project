using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OiilItem : PouringItem
{
    public GameObject oilStain;
    float pourSpeed;
    float EnvInfl;
    public override void OnStart()
    {
        pourSpeed = 0.2f;
        EnvInfl = -10f;

        cam = Camera.main;
     
    }
    public override void Pour()
    {
        if (oilStain.transform.localScale.x<1)
        {
            oilStain.transform.localScale += new Vector3(Time.deltaTime*pourSpeed, Time.deltaTime*pourSpeed, 0);
            oilStain.GetComponent<OilPool>().influence += Time.deltaTime * pourSpeed* EnvInfl;
            Camera.main.GetComponent<GameManager>().GetInfluence();
        }
    }
}
