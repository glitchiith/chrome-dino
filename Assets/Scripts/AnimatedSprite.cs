using UnityEngine;

// Requires the player GameObject to have a SpriterRenderer component attached
// If there is no such component attached, then one is created automatically
[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    // Array of sprites for running and crouching animation
    public Sprite[] runSprites;
    public Sprite[] crouchSprites;

    private SpriteRenderer spriteRenderer;
    private int frame;
    public bool isCrouching;

    // Called first of all (when the scripts are being loaded)
    private void Awake()
    {
        // Obtain a reference to the SpriteRenderer component attached to the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Called when the GameObject becomes active (e.g., restarting after dying)
    private void OnEnable()
    {
        // Invoke the Animate() method immediately
        Invoke(nameof(Animate), 0f);
    }

    // Called when the GameObject becomes inactive 
    private void OnDisable()
    {
        // Cancel all pending invokes
        CancelInvoke();
    }

    // Called at every frame
    private void Update()
    {
        // Update the crouching status of the player
        if (!gameObject.activeSelf)
        {
            isCrouching = false;
        }
        if (gameObject.GetComponent<CharacterController>().isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                isCrouching = true;
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                isCrouching = false;
            }
        }
        else
        {
            isCrouching = false;    
        }
    }

    // Custom method for animating the player
    private void Animate()
    {
        // Increment frame at every Animate() call
        frame++;

        // If frame overshoots the lenght of the array, loop it back to 0
        if (frame >= runSprites.Length)
            frame = 0;

        // Render the crouching sprite
        if (isCrouching && frame < crouchSprites.Length)
            spriteRenderer.sprite = crouchSprites[frame];

        // Render the running sprite
        else if (!isCrouching && frame < runSprites.Length)
            spriteRenderer.sprite = runSprites[frame];

        // Invoke Animate() again after 1/gameSpeed seconds
        // The higher the gameSpeed, the lower is the time between Animate() calls, 
        // the higher is the frequency of Animate() calls and the higher is the running speed
        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    }

}
