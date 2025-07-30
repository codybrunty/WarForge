using System.Collections.Generic;
using UnityEngine;

public class NameGenerationService {
    private List<string> maleNames;
    private List<string> femaleNames;

    public NameGenerationService() {
        maleNames = LoadNamesFromFile("Names/MaleNames");
        femaleNames = LoadNamesFromFile("Names/FemaleNames");
    }

    public string GetMaleName() {
        return maleNames[Random.Range(0, maleNames.Count)];
    }

    public string GetFemaleName() {
        return femaleNames[Random.Range(0, femaleNames.Count)];
    }

    private List<string> LoadNamesFromFile(string path) {
        TextAsset file = Resources.Load<TextAsset>(path);
        if (file == null) {
            Debug.LogError("Name file not found at Resources/" + path);
            return new List<string>();
        }
        return new List<string>(file.text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries));
    }
}
