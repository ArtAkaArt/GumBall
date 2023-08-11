using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorManager : MonoBehaviour
{
    [SerializeField] private ColorEnum color;

    public ColorEnum Color
    {
        get => color;
        set => color = value;
    }

    private SpriteRenderer spriteRenderer;

    private void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();
    public void RenderColor() => spriteRenderer.color = ColorDictionary.GetColor(color);
    public void RenderColor(ColorEnum newColorEnum) => spriteRenderer.color = ColorDictionary.GetColor(newColorEnum);
}