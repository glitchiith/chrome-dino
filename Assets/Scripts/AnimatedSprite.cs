using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] runSprites;
    public Sprite[] crouchSprites;

    private SpriteRenderer spriteRenderer;
    private int frame;
    private bool isCrouching;

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

    private void Update()
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

    private void Animate()
    {
        frame++;
        if (frame >= runSprites.Length)
            frame = 0;

        if (isCrouching && frame < crouchSprites.Length)
        {
            spriteRenderer.sprite = crouchSprites[frame];
        }
        else if (!isCrouching && frame < runSprites.Length)
        {
            spriteRenderer.sprite = runSprites[frame];
        }

        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    }

}
