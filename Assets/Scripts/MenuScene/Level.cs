using UnityEngine;

public struct Level
{
    public int[] WallsCollors;
    public Obstacle[] Obstacles;
    public int GoalColor;
}

public struct Obstacle
{
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public int Color { get; set; }
}