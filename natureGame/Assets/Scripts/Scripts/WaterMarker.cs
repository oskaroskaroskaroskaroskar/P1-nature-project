using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMarker : MonoBehaviour
{
    float makeStainTimer = 0f;
    float makeStainDuration;

    public GameObject waterStain;
    List<WaterStain> markedStains = new List<WaterStain>();
    // Start is called before the first frame update
    void Start()
    {
        makeStainDuration = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void Pour()
    {
        if (markedStains.Count>0)
        {
            markedStains[0].BeingWatered();
        }
        else if(makeStainTimer >= makeStainDuration)
        {
            GameObject stainObj = Instantiate(waterStain);
            stainObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 2f);
            makeStainTimer = 0f;
        } 
        else
        {
            makeStainTimer += Time.deltaTime;
        }
    }
    void OnTriggerEnter2D (Collider2D other) {
        if(other.tag=="WaterStain")
        {
            markedStains.Add(other.GetComponent<WaterStain>());
        }
    }
    void OnTriggerExit2D (Collider2D other)
    {
        if (other.tag == "WaterStain")
        {
            markedStains.Remove(other.GetComponent<WaterStain>());
        }
    }
}
