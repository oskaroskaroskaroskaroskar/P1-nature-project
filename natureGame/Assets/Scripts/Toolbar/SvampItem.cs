using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SvampItem : PouringItem
{
    public Animator animator;
    public GameObject oilStain;
    private float suckSpeed;
    private float envInfluence;
    private float darkness;
    public override void OnStart()
    {
        darkness = 0;
        envInfluence = 10f;
        suckSpeed = 0.05f;
    }

    public override void Pour()
    {
        if (oilStain.transform.localScale.x > 0)
        {
            animator.SetBool("sucking", true);
            oilStain.transform.localScale += new Vector3(-Time.deltaTime * suckSpeed, -Time.deltaTime * suckSpeed, 0);
            oilStain.GetComponent<OilPool>().influence += Time.deltaTime * suckSpeed * envInfluence;
            Camera.main.GetComponent<GameManager>().GetInfluence();
            if (darkness<200)
            {
                gameobject.GetComponent<SpriteRenderer>().color = new Color32((byte)(255-darkness), (byte)(255 -darkness), (byte)(225 -darkness), 255);
                darkness += Time.deltaTime * 20;
            }
        } else
        {
            NotPouring();
        }
    }
    public override void NotPouring()
    {

        animator.SetBool("sucking", false);
        

    }
    public override void Dropped()
    {
        
        darkness = 0;
    }
}
