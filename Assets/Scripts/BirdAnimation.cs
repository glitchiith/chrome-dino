using UnityEngine;

// Requires the bird prefab to have a SpriterRenderer component attached
// If there is no such component attached, then one is created automatically
[RequireComponent(typeof(SpriteRenderer))]
public class BirdAnimation : MonoBehaviour
{
    // Array of sprites for the flying animation
    public Sprite[] sprites;

    private SpriteRenderer spriteRenderer;
    private int frame;

    // Called first of all (when the scripts are being loaded)
    private void Awake()
    {
        // Obtain a reference to the SpriteRenderer component attached to the prefab
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Called when the GameObject becomes active
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

    // Custom method for animating the bird
    private void Animate()
    {
        // Increment frame at every Animate() call
        frame++;

        // If frame overshoots the lenght of the array, loop it back to 0
        if (frame >= sprites.Length)
            frame = 0;

        // Render the flying sprite
        if (frame >= 0 && frame < sprites.Length)
            spriteRenderer.sprite = sprites[frame];

        // Invoke Animate() again after 1/gameSpeed seconds
        // The higher the gameSpeed, the lower is the time between Animate() calls, 
        // the higher is the frequency of Animate() calls and the higher is the flying speed
        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    }

}