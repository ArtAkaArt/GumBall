using System.Collections.Generic;
using UnityEngine;

public class ColorDictionary : MonoBehaviour
{
    private static Dictionary<ColorEnum, Color> colorDictonary = new()
    {
        { ColorEnum.Red, Color.red },
        { ColorEnum.Green, Color.green },
        { ColorEnum.Yellow, Color.yellow },
        { ColorEnum.Blue, Color.blue },
        { ColorEnum.White, Color.white },
    };

    public static Color GetColor(ColorEnum inColorEnum) => colorDictonary[inColorEnum];
}