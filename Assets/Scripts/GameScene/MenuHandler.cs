using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private EndLevelMenu menuPanel;
    [SerializeField] private OptionsMenu options;

    private void Start() => LevelManager.OnSceneRestart += DeactivateMenu;

    public void InputHandler(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        OpenMenu();
    }

    private void DeactivateMenu()=> menuPanel.gameObject.SetActive(false);

    public void ToMainMenu()
    {
        LevelManager.OnSceneRestart -= DeactivateMenu;
        SceneManager.LoadScene(0);
    }

    public void OpenMenu()
    {
        if (options.gameObject.activeSelf)
        {
            options.gameObject.SetActive(false);
            return;
        }

        LevelManager.IsPaused = !LevelManager.IsPaused;
        menuPanel.gameObject.SetActive(LevelManager.IsPaused);
        var isNextLevelAvailable = GameManager.MaxAvailableLevel > GameManager.CurrentLevel &&
                                   GameManager.CurrentLevel < GameManager.Levels.Length - 1;
        menuPanel.ShowNextLevelButton(isNextLevelAvailable);
        var isLastLevelCleared = GameManager.MaxAvailableLevel > GameManager.CurrentLevel &&
                                 GameManager.CurrentLevel == GameManager.Levels.Length - 1;
        menuPanel.ShowGameOver(isLastLevelCleared);
    }
}