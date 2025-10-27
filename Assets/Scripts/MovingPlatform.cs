using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private float speed = 2.0f;
    private int point ;

    // Array of points for the platform to move between
    public Transform[] points;

    void Start()
    {
        // Set the initial position of the platform to the first point
        transform.position = points[0].position;
    }

    
    void Update()
    {
        // Check if the platform has reached the current target point
        if (Vector2.Distance(transform.position, points[point].position) == 0.0f){

            point++;
            // Loop back to the first point if at the end of the array
            if (point >= points.Length){
                point = 0;
            }
        }
        // Move the platform towards the current target point
        transform.position = Vector2.MoveTowards(transform.position, points[point].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Make the player a child of the platform to move together
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Detach the player from the platform
            collision.gameObject.transform.SetParent(null);
        }
    }
}
