using UnityEngine;

public static class DBConverter
{
    private const float Multiplier = 30;
    public static float ConvertToDB(float value) => Mathf.Log10(value) * Multiplier;
}