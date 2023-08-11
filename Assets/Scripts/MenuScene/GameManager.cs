using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    public static GameManager Data;
    public static Level[] Levels;
    public static event UnityAction<int> OnCurrentLevelChange;
    private static int maxAvailableLevel;

    public static int MaxAvailableLevel
    {
        get => maxAvailableLevel;
        set => maxAvailableLevel = value > Levels.Length ? Levels.Length : value;
    }

    private const string masterVol = "MasterVol";
    private const string musicVol = "MusicVol";
    private const string sfxVol = "SFXVol";

    public static int CurrentLevel { get; private set; }

    private void Awake()
    {
        CurrentLevel = -1;
        maxAvailableLevel = DataSaver.Load();

        if (Data != null)
        {
            Destroy(gameObject);
            return;
        }

        InitiateLevels();
        Data = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() => LoadSoundSettings();

    private void LoadSoundSettings()
    {
        mixer.SetFloat(masterVol, DBConverter.ConvertToDB(PlayerPrefs.GetFloat(masterVol)));
        mixer.SetFloat(musicVol, DBConverter.ConvertToDB(PlayerPrefs.GetFloat(musicVol)));
        mixer.SetFloat(sfxVol, DBConverter.ConvertToDB(PlayerPrefs.GetFloat(sfxVol)));
    }

    public static void ChangeCurrentLevel(int newLevel)
    {
        OnCurrentLevelChange?.Invoke(newLevel);
        CurrentLevel = newLevel;
    }

    private static void InitiateLevels()
    {
        Levels = new Level[]
        {
            new()
            {
                WallsCollors = new[] { 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3 },
                Obstacles = Array.Empty<Obstacle>(),
                GoalColor = 2,
            },
            new()
            {
                WallsCollors = new[] { 2, 1, 3, 1, 3, 2, 3, 1, 2, 0, 3, 1 },
                Obstacles = new[]
                {
                    new Obstacle { Position = new Vector2(1.5f, -2f), Scale = new Vector2(1, 4), Color = 2 },
                    new Obstacle { Position = new Vector2(-2.5f, 2f), Scale = new Vector2(1, 4), Color = 0 },
                },
                GoalColor = 2,
            },
            new()
            {
                WallsCollors = new[] { 0, 1, 2, 3, 0, 1, 2, 0, 3, 1, 2, 0 },
                Obstacles = new[]
                {
                    new Obstacle { Position = new Vector2(1.5f, -2f), Scale = new Vector2(1, 4), Color = 1 },
                    new Obstacle { Position = new Vector2(-2.5f, 2f), Scale = new Vector2(1, 4), Color = 3 },
                },
                GoalColor = 3,
            },
            new()
            {
                WallsCollors = new[] { 2, 1, 0, 3, 0, 2, 2, 1, 0, 3, 0, 2 },
                Obstacles = new[]
                {
                    new Obstacle { Position = new Vector2(-1.5f, 3), Scale = new Vector2(3, 2), Color = 1 },
                    new Obstacle { Position = new Vector2(-1.5f, -1.75f), Scale = new Vector2(3, 4.5f), Color = 1 },
                },
                GoalColor = 0,
            },
            new()
            {
                WallsCollors = new[] { 3, 3, 3, 0, 0, 0, 1, 1, 1, 2, 2, 2 },
                Obstacles = new[]
                {
                    new Obstacle { Position = new Vector2(4.5f, 2.5f), Scale = new Vector2(1, 3), Color = 3 },
                    new Obstacle { Position = new Vector2(-6f, -2.5f), Scale = new Vector2(1, 3), Color = 2 },
                },
                GoalColor = 2,
            },
        };
    }
}