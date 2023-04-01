using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Left-hand limit for the game screen
    private float leftEdge;

    // Called once after the script is enabled
    private void Start()
    {
        // Set the limit to the screen's leftmost point offset by 2
        // ScreenToWorldPoint() transforms a point from screen space to world space
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
    }

    // Called at every frame
    private void Update()
    {
        // Move the object towards the left at every frame
        // The speed at which it moves depends upon the gameSpeed at that instant
        transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;

        // If the object has exceed the camera bounds
        if (transform.position.x < leftEdge) 
        {
            // Destroy the object
            Destroy(gameObject);
        }
    }

}
