using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;

    public float jumpForce = 8f;
    public float gravity = 9.81f * 2f;
    float radius;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        radius = character.radius;
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            direction = Vector3.down;

            // Jump
            if (Input.GetButton("Jump")) 
            {
                direction = Vector3.up * jumpForce;
            }

            // Crouch
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // Shrink the collider
                character.radius = radius/2;

                // Translate the collider so that it is near the feet of the character
                character.center = new Vector3(0, -0.24f, 0);
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                // Restore the collider
                character.radius = radius;
                character.center = new Vector3(0, 0, 0);
            }
        }
        
        character.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) 
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }

}
