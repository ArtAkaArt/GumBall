using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelMenu : MonoBehaviour
{
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private TMP_Text gameoverText;
    public void ShowNextLevelButton(bool isShown) => nextLevelButton.gameObject.SetActive(isShown);
    public void ShowGameOver(bool isShown) => gameoverText.gameObject.SetActive(isShown);

    private void OnEnable() => Time.timeScale = 0;

    private void OnDisable() => Time.timeScale = 1;
}
