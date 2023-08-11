using UnityEngine;

public class ObstaclePool : ObjectPool
{
    [SerializeField] private ObstacleScript obstacle;

    private void Awake() => Initialize(obstacle.gameObject);

    public void InitObstacle(Obstacle obstacle)
    {
        if (!TryGetObject(out var obstacleObject))
            return;
        obstacleObject.SetActive(true);
        var obstacleScript = obstacleObject.GetComponent<ObstacleScript>();
        obstacleScript.Init((ColorEnum)obstacle.Color, obstacle.Position, obstacle.Scale);
    }
}