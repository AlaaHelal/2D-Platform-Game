using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed = 2.0f;
    private int point;

    // Array of points for the platform to move between
    public Transform[] points;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        // Check if the platform has reached the current target point
        if (Vector2.Distance(transform.position, points[point].position) == 0.0f)
        {

            point++;
            // Loop back to the first point if at the end of the array
            if (point >= points.Length)
            {
                point = 0;
            }
        }
        // Move the platform towards the current target point
        transform.position =
            Vector2.MoveTowards(transform.position, points[point].position, speed * Time.deltaTime);

        // Flip the sprite based on movement direction
        spriteRenderer.flipX = transform.position.x < points[point].position.x;
    }
}
