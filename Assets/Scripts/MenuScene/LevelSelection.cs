using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private LevelView levelViewPrefab;
    [SerializeField] private Transform contentLayout;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Button startLevelButton;
    [SerializeField] private Button leftArrow, rightArrow;

    private float scrollStepLength;
    private const float Precision = .00001f;
    private List<LevelView> levels;

    private void Start()
    {
        GameManager.OnCurrentLevelChange += ChangeButtonAvailability;
        levels = new List<LevelView>();
        for (int i = 0; i < GameManager.Levels.Length; i++)
        {
            var view = Instantiate(levelViewPrefab, contentLayout);
            levels.Add(view);
            var isCleared = i < GameManager.MaxAvailableLevel;
            var isSelectable = i <= GameManager.MaxAvailableLevel;
            view.Init(i, isSelectable, isCleared);
            view.OnLevelClick += UnselectLevels;
            scrollStepLength = 1.0f / (GameManager.Levels.Length - 3);
        }
    }

    private void OnDestroy()
    {
        GameManager.OnCurrentLevelChange -= ChangeButtonAvailability;
        levels.ForEach(x => x.OnLevelClick -= UnselectLevels);
    }

    private void UnselectLevels(int selectedIndex)
    {
        for (int i = 0; i < levels.Count; i++)
        {
            if (i == selectedIndex)
                continue;
            levels[i].HideBorder();
        }
    }

    private void ChangeButtonAvailability(int availability) => startLevelButton.interactable = availability != -1;

    public void HideMenu()
    {
        UnselectLevels(-1);
        GameManager.ChangeCurrentLevel(-1);
        gameObject.SetActive(false);
    }

    public void StartLevel() => SceneManager.LoadScene(1);

    public void ScrollView(bool isPositive) =>
        scrollRect.horizontalNormalizedPosition += scrollStepLength * (isPositive ? 1 : -1);

    public void DisableButtons(Vector2 scrollViewValue)
    {
        leftArrow.interactable = scrollViewValue.x != 0;
        rightArrow.interactable = (1 - scrollViewValue.x) > Precision;
    }
}