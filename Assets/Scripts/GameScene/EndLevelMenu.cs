using UnityEngine;
using UnityEngine.UI;

public class EndLevelMenu : MonoBehaviour
{
    [SerializeField] private Button nextLevelButton;
    public void ShowNextLevelButton(bool isShown) => nextLevelButton.gameObject.SetActive(isShown);

    private void OnEnable() => Time.timeScale = 0;

    private void OnDisable() => Time.timeScale = 1;
}
