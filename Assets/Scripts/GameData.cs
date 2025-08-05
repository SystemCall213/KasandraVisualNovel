using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class FlagPair
{
    public string key;
    public bool value;
}

[System.Serializable]
public class FlagData
{
    public List<FlagPair> flags;
}

public static class GameData
{
    public static Dictionary<string, bool> flags = new Dictionary<string, bool>();

    public static void LoadFlags()
    {
        string path = Path.Combine(Application.persistentDataPath, "Flags.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var flagData = JsonUtility.FromJson<FlagData>(json);
            flags = new Dictionary<string, bool>();
            foreach (var pair in flagData.flags)
            {
                flags[pair.key] = pair.value;
            }
        }
        else
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("GameData/Flags");
            if (jsonFile != null)
            {
                var flagData = JsonUtility.FromJson<FlagData>(jsonFile.text);
                flags = new Dictionary<string, bool>();
                foreach (var pair in flagData.flags)
                {
                    flags[pair.key] = pair.value;
                }
            }
        }
    }


    public static void SaveFlags()
    {
        var list = new List<FlagPair>();
        foreach (var kvp in flags)
        {
            list.Add(new FlagPair { key = kvp.Key, value = kvp.Value });
        }

        string json = JsonUtility.ToJson(new FlagData { flags = list });
        string path = Path.Combine(Application.persistentDataPath, "Flags.json");
        File.WriteAllText(path, json);
    }

    public static void SetFlag(string key, bool value)
    {
        if (flags.ContainsKey(key))
            flags[key] = value;
        else
            flags.Add(key, value);
    }

    public static bool GetFlag(string key)
    {
        return flags.ContainsKey(key) && flags[key];
    }
}
