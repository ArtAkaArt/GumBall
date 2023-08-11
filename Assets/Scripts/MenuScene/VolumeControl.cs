using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider slider;
    [SerializeField] private string parameterName;

    private void Awake() => slider.onValueChanged.AddListener(HandleSliderValueChanged);

    private void Start() => slider.value = PlayerPrefs.GetFloat(parameterName, slider.value);

    private void HandleSliderValueChanged(float value) => mixer.SetFloat(parameterName, DBConverter.ConvertToDB(value));

    private void OnDisable() => PlayerPrefs.SetFloat(parameterName, slider.value);
}