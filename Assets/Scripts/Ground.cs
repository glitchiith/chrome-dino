using UnityEngine;

// Requires the ground GameObject to have a MeshRenderer component attached
// If there is no such component attached, then one is created automatically
[RequireComponent(typeof(MeshRenderer))]
public class Ground : MonoBehaviour
{
    // MeshRenderer for accessing and modifying the texture of the ground
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        // Obtain a reference to the MeshRenderer component attached to the object
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // Calculate the speed at which the ground has to be moved
        float speed = GameManager.Instance.gameSpeed / transform.localScale.x;

        // Scroll the ground texture to give an illusion of movement
        meshRenderer.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
    }

}
