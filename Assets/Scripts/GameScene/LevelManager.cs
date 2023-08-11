using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ColorManager[] walls;
    [SerializeField] private ObstaclePool pool;
    [SerializeField] private ColorManager goal;
    [SerializeField] private BallShooter shooter;
    public static bool IsPaused { get; set; }
    public static bool IsShooted { get; set; }

    public static event UnityAction OnSceneRestart;
    private void Start() => InitLevel(GameManager.CurrentLevel);

    public void InitLevel(int levelIndex)
    {
        pool.ResetNodes();
        var level = GameManager.Levels[levelIndex];
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].Color = (ColorEnum)level.WallsCollors[i];
            walls[i].RenderColor();
        }

        foreach (var obstacle in GameManager.Levels[levelIndex].Obstacles)
            pool.InitObstacle(obstacle);

        goal.Color = (ColorEnum)level.GoalColor;
        goal.RenderColor();
    }

    public void RestartScene()
    {
        IsShooted = false;
        IsPaused = false;
        shooter.SetShootPoints();
        shooter.InitBall();
        OnSceneRestart?.Invoke();
    }

    public void StartNextLevel()
    {
        GameManager.ChangeCurrentLevel(GameManager.CurrentLevel + 1);
        InitLevel(GameManager.CurrentLevel);
        RestartScene();
    }
}