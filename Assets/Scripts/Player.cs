using UnityEngine;

// Requires the player GameObject to have a CharacterController component attached
// If there is no such component attached, then one is created automatically
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    // Unity's CharacterController component for handling (non-physics based) movement
    // If you want to use Unity's physics system instead, use the Rigidbody component
    private CharacterController character;
    private Vector3 direction;

    // Declaration of other variables
    public float jumpForce = 8f;
    public float gravity = 9.81f * 2f;
    float radius;

    // Called first of all (when the scripts are being loaded)
    private void Awake()
    {
        // Obtain a reference to the CharacterController component attached to the GameObject
        character = GetComponent<CharacterController>();

        // Obtain the radius of the CharacterController's collider
        radius = character.radius;
    }

    // Called when the GameObject becomes active (e.g., restarting after dying)
    private void OnEnable()
    {
        // Initialize the movement direction to the zero vector (0, 0, 0)
        direction = Vector3.zero;
    }

    // Called at every frame
    private void Update()
    {
        // Pull the player down due to gravity
        direction += Vector3.down * gravity * Time.deltaTime;

        // If the character is on the ground
        if (character.isGrounded)
        {
            // Initialize direction to (0, -1, 0)
            direction = Vector3.down;

            // Jump when the "Jump" button is pressed 
            // The button map is defined in Edit -> Project Settings -> Input Manager -> Axes
            if (Input.GetButton("Jump")) 
            {
                // Change direction to point upwards (to jump)
                direction = Vector3.up * jumpForce;
            }

            // Crouch as long as the down arrow key is held down
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // Shrink the collider
                // When the player crouches, colliding with an obstacle is less likely
                character.radius = radius/2;

                // Translate the collider so that it is near the feet of the character
                // This is done because just shrinking the collider will keep the center unchanged
                // This would lead to the full body not being covered by the collider
                character.center = new Vector3(0, -0.24f, 0);
            }
            // Stop crouching when the down arrow key is released
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                // Restore the collider back to its original size
                character.radius = radius;

                // Restore the center of the collider back to its original position
                character.center = new Vector3(0, 0, 0);
            }
        }
        // Move the character in the computed direction
        // Time.deltaTime is to account for varying frame rates
        character.Move(direction * Time.deltaTime);
    }

    // Called when the player collides with another GameObject
    private void OnTriggerEnter(Collider other)
    {
        // Check if the GameObject the player collided with has the tag "Obstacle"
        // Tags can be seen and edited in the top part of the Inspector that appears on selecting a GameObject
        // It is just below the object name
        if (other.CompareTag("Obstacle")) 
        {
            // Access the GameOver() method in the GameManager script to end the game
            // FindObjectOfType finds and returns the first instance of the GameManager class it finds in the entire script
            FindObjectOfType<GameManager>().GameOver();
        }
    }

}
