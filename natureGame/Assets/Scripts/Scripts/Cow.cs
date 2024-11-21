using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cow : MonoBehaviour
{
    GameManager gameManager;
    public Animator animator;
    bool isWalking = false;
    bool isHappy = true;
    public GameObject walkzone;
    float leftEdge;
    float rightEdge;
    float upperEdge;
    float lowerEdge;
    Vector2 targetDest;
    float walkSpeed = 0.6f;
    float walkLength = 1;
    System.Random rand = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        walkzone = GameObject.FindWithTag("cow walkzone");
        leftEdge = walkzone.transform.position.x-(walkzone.transform.localScale.x/2);
        rightEdge = walkzone.transform.position.x + (walkzone.transform.localScale.x / 2);
        upperEdge = walkzone.transform.position.y + (walkzone.transform.localScale.y / 2);
        lowerEdge = walkzone.transform.position.y - (walkzone.transform.localScale.y / 2);
        gameManager = Camera.main.GetComponent<GameManager>();
        CowItem.count++;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.environmentScore < -5 && isHappy)
        {
            isHappy = false;
            animator.SetBool("isHappy", false);
        } 
        else if (gameManager.environmentScore>-3 && !isHappy)
        {
            isHappy = true;
            animator.SetBool("isHappy", true);
        }
        if (!isWalking)
        {
            double random = rand.NextDouble()*1000;
            if (random<=5)
            {
                isWalking = true;
                animator.SetBool("isWalking",true);
                targetDest = NewPosition();
            }
        } else if(isWalking)
        {
            Walk();
        }
        fixScale();
    }
    void Walk()
    {
        float speed = walkSpeed*Time.deltaTime;
        float xDist = targetDest.x- transform.position.x;
        float yDist = targetDest.y- transform.position.y;
        float totalDist = Mathf.Sqrt(xDist*xDist+yDist*yDist);
        float ratio = totalDist/speed;
        float xSpeed = xDist / ratio;
        float ySpeed = yDist / ratio;
        if (speed > totalDist)
        {
            xSpeed = xDist;
            ySpeed = yDist;
            isWalking = false;
            animator.SetBool("isWalking", false);
        }
        transform.position += new Vector3(xSpeed,ySpeed,0);
    }
    Vector2 NewPosition ()
    {
        Vector2 newPosition;
        double random = rand.NextDouble();
        
        float angle = (float)(random * 360f);
        random = rand.NextDouble();
        float x = Mathf.Cos(angle) * (float) random * walkLength;
        float y = Mathf.Sin(angle) * (float) random * walkLength;
        newPosition = new Vector2(transform.position.x+x, transform.position.y+y);
        if (newPosition.x < leftEdge)
        {
            newPosition.x = leftEdge;
        }
        if (newPosition.x > rightEdge)
        {
            newPosition.x = rightEdge;
        }
        if(newPosition.y < lowerEdge)
        {
            newPosition.y = lowerEdge;
        }
        if (newPosition.y > upperEdge)
        {
            newPosition.y = upperEdge;
        }
        if(x > 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
        }
        else if (x < 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        return (newPosition);
    }
    void fixScale()
    {
        int xTwist = 0;
        if(this.transform.localScale.x>0)
        {
            xTwist = 1;
        }
        else if (this.transform.localScale.x<0)
        {
            xTwist = -1;
        }
        float yPosition = this.transform.position.y;
        if (yPosition > 0)
        {
            yPosition = 0;
        }
        float scale = 0.4f - yPosition / 2.5f;
        this.transform.localScale = new Vector3(xTwist*scale, scale, 1);

    }
    public void OnDisable()
    {
        CowItem.count--;
    }

}
