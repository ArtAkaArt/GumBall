using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// For Desktop Builds
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Toggle fullScreen;
    [SerializeField] private TMP_Dropdown dropdown;

    private List<Resolution> resolutions;
    private int currentResolution;

    private void Start()
    {
        fullScreen.isOn = Screen.fullScreen;
        resolutions = Screen.resolutions
            .Select(x => new Resolution(x.width, x.height))
            .Distinct()
            .ToList();

        for (int i = 0; i < resolutions.Count; i++)
        {
            if (!CompareResolution(resolutions[i]))
                continue;
            currentResolution = i;
            break;
        }

        dropdown.AddOptions(resolutions.Select(x => x.ToString()).ToList());
        dropdown.value = currentResolution;
    }

    private static bool CompareResolution(Resolution res) => Screen.width == res.Width && Screen.height == res.Height;

    public void SetResolution(int resolutionChoise) => currentResolution = resolutionChoise;

    public void ApplySettings()
    {
        Screen.fullScreen = fullScreen.isOn;
        Screen.SetResolution(resolutions[currentResolution].Width, resolutions[currentResolution].Height,
            fullScreen.isOn);
    }

    public void SwitchActiveState() => gameObject.SetActive(!gameObject.activeSelf);
}

public struct Resolution
{
    public int Width { get; set; }
    public int Height { get; set; }

    public Resolution(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public override string ToString() => $"{Width} x {Height}";
}