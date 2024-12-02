using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStain : MonoBehaviour
{
    float maxSize;
    float growSpeed;
    float decreaseSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        maxSize = 1.0f;
        growSpeed = 1.5f;
        decreaseSpeed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale -= new Vector3(decreaseSpeed * Time.deltaTime, decreaseSpeed * Time.deltaTime, 1f);
        if (transform.localScale.x <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void BeingWatered()
    {
        if (transform.localScale.x < maxSize)
        {
            transform.localScale += new Vector3(growSpeed * Time.deltaTime, growSpeed * Time.deltaTime, 1f);
        } 

    }
}
