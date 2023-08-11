using UnityEngine;

public static class Vector2Extension
{
    public static bool IsEdge(this Vector2 normal) => Mathf.Abs(1 - Mathf.Abs(normal.x + normal.y)) > .01f;
}