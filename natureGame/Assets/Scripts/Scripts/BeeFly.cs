using UnityEngine;

public class BeeFly : MonoBehaviour
{
    private Vector2 startPosition;
    private Vector2 targetPosition;
    private float areaSize = 2f; // Define the small area size
    private float speed = 2f; // Movement speed
    private bool isFlyingAway = false;

    void Start()
    {
        startPosition = transform.position;
        GenerateNewTargetPosition();
        BeeItem.count++;
    }

    void Update()
    {
        if (!isFlyingAway)
        {
            // Move towards the target position
            MoveInArea();

            // Check the score in every frame
            if (Camera.main.GetComponent<GameManager>().environmentScore <= -2)
            {
                FlyAwayAndDestroy();
                //DestroyAllBeesWithTag();
            }
        }
    }

    private void MoveInArea()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // If the bee reaches the target position, generate a new one
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            GenerateNewTargetPosition();
        }
    }

    private void GenerateNewTargetPosition()
    {
        // Generate a random position within a 2-unit area around the start position
        targetPosition = new Vector2(
            Random.Range(startPosition.x - areaSize / 2, startPosition.x + areaSize / 2),
            Random.Range(startPosition.y - areaSize / 2, startPosition.y + areaSize / 2)
        );
    }

    public void FlyAwayAndDestroy()
    {
        this.isFlyingAway = true;
        Vector2 flyAwayDirection = new Vector2(Random.Range(-1f, 1f), 1f).normalized; // Fly upwards
        this.GetComponent<Rigidbody2D>().velocity = flyAwayDirection * 2f; // Adjust speed as needed
        Destroy(this.transform.parent.gameObject, 1f); // Destroy after 3 seconds
        
    }
    public void OnDisable()
    {
        BeeItem.count--;
    }

}
