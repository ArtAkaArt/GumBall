using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LevelView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text levelLable;
    [SerializeField] private Image fade;
    [SerializeField] private Image medal;
    public event UnityAction<int> OnLevelClick;

    private int index;
    private bool isSelectable;
    private Image border;
    private Color borderColorBuffer;


    private void Awake()
    {
        border = GetComponent<Image>();
        borderColorBuffer = border.color;
    }

    private void TurnOffFade() => fade.gameObject.SetActive(false);

    private void TurnOffMedal() => medal.gameObject.SetActive(false);

    public void Init(int index, bool isSelectable, bool cleared)
    {
        this.index = index;
        this.isSelectable = isSelectable;
        if (isSelectable)
            TurnOffFade();

        if (!cleared)
            TurnOffMedal();
        levelLable.text = $"Level {index + 1}";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isSelectable)
            return;
        GameManager.ChangeCurrentLevel(index);
        OnLevelClick?.Invoke(index);
        ShowBorder();
    }

    public void ShowBorder() => ChangeBorderAlpha(1);

    public void HideBorder() => ChangeBorderAlpha(0);

    private void ChangeBorderAlpha(float alpha)
    {
        borderColorBuffer.a = alpha;
        border.color = borderColorBuffer;
    }
}