using UnityEngine;

[RequireComponent(typeof(ColorManager), typeof(SpriteRenderer))]
public class TrajectoryNode : MonoBehaviour
{
    [SerializeField] private Sprite destroyedSprite, normalSprite;
    private ColorManager manager;
    private LineRenderer lineRenderer;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        manager = GetComponent<ColorManager>();
        lineRenderer = GetComponent<LineRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetPositions(Vector2 startPosition, Vector2 lineEndPosition, ColorEnum lineColorcolor,
        ColorEnum ballColor)
    {
        transform.position = startPosition;
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, lineEndPosition);
        lineRenderer.startColor = ColorDictionary.GetColor(lineColorcolor);
        lineRenderer.endColor = ColorDictionary.GetColor(lineColorcolor);
        manager.RenderColor(ballColor);
    }

    public void RenderDestroyedSprite() => spriteRenderer.sprite = destroyedSprite;

    private void OnDisable() => spriteRenderer.sprite = normalSprite;
}