using UnityEngine;

[RequireComponent(typeof(ColorManager), typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class ObstacleScript : MonoBehaviour
{
    private ColorManager colorManager;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        colorManager = GetComponent<ColorManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Init(ColorEnum color, Vector2 position, Vector2 scale)
    {
        colorManager.Color = color;
        colorManager.RenderColor();
        transform.position = position;
        spriteRenderer.size = scale;
        boxCollider.size = scale;
    }
}