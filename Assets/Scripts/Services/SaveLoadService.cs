using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SaveLoadService {
    private string GetPath(string key) => Path.Combine(Application.persistentDataPath, $"{key}.json");
    public bool Exists(string key) => File.Exists(GetPath(key)); 
    public string GetSavePath(string key) => GetPath(key);


    private readonly JsonSerializerSettings settings = new() {
        TypeNameHandling = TypeNameHandling.Auto,
        Formatting = Formatting.Indented
    };

    public void Save<T>(string key, T data) {
        string json = JsonConvert.SerializeObject(data, settings);
        File.WriteAllText(GetPath(key), json);
        Debug.Log($"[Save] {typeof(T)} to {GetPath(key)}");
    }

    public T Load<T>(string key) {
        string path = GetPath(key);
        if (!File.Exists(path)) {
            Debug.LogWarning($"[Load] File not found for key: {key}");
            return default;
        }

        try {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
        catch (System.Exception ex) {
            Debug.LogError($"[Load] Failed to load {key}: {ex.Message}");
            return default;
        }
    }

    public void Delete(string key) {
        string path = GetPath(key);
        if (File.Exists(path)) {
            File.Delete(path);
            Debug.Log($"[Delete] Save deleted for key: {key}");
        }
    }
}