using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] runSprites;
    public Sprite[] crouchSprites;

    private SpriteRenderer spriteRenderer;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Invoke(nameof(Animate), 0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        frame++;

        if (frame >= runSprites.Length) {
            frame = 0;
        }

        if (frame >= 0 && frame < runSprites.Length) {
            // Animating running/crouching - NOT working 
            if (Input.GetKeyDown(KeyCode.DownArrow))
                spriteRenderer.sprite = crouchSprites[frame];
            else
                spriteRenderer.sprite = runSprites[frame];
        }

        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    }

}
