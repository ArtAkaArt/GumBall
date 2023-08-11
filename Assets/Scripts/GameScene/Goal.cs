using UnityEngine;

[RequireComponent(typeof(ColorManager))]
public class Goal : MonoBehaviour
{
    [SerializeField] private MenuHandler menu;
    [SerializeField] private AudioHub hub;
    private ColorManager colorManager;

    private void Awake() => colorManager = GetComponent<ColorManager>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<Ball>(out var ball))
            return;
        var isCleared = other.GetComponent<ColorManager>().Color == colorManager.Color;
        if (isCleared)
        {
            hub.PlayWin();
            GameManager.MaxAvailableLevel++;
            DataSaver.Save(new DataSaver.SaveData { Level = GameManager.MaxAvailableLevel });
        }
        else 
            hub.PlayGameOver();

        Destroy(other.gameObject);
        menu.OpenMenu();
    }
}