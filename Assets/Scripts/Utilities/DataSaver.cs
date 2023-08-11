using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Well PlayerPrefs would be enough, but I keep saving that way for learning purposes
public static class DataSaver
{
    private const string fileName = "save";
    private static BinaryFormatter formatter = new();

    public static void Save(SaveData data)
    {
        using var fileStream = File.Create(GetPath());
        formatter.Serialize(fileStream, data);
    }

    public static int Load()
    {
        if (!File.Exists(GetPath()))
            return 0;

        using var fileStream = File.Open(GetPath(), FileMode.Open);

        try
        {
            var save = formatter.Deserialize(fileStream) as SaveData;
            return save?.Level ?? 0;
        }
        catch (SerializationException ex)
        {
            Debug.LogException(ex);
            return 0;
        }
    }

    private static string GetPath() => Path.Combine(Application.persistentDataPath, fileName);

    [Serializable]
    public class SaveData
    {
        public int Level;
    }
}