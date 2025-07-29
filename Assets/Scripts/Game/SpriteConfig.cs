using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteConfig", menuName = "Scriptable Objects/SpriteConfig")]
public class SpriteConfig : ScriptableObject{

    [System.Serializable]
    public class Entry {
        public string key;
        public Sprite sprite;
    }

    [SerializeField] private List<Entry> entries = new();

    private Dictionary<string, Sprite> lookup;

    private void OnEnable() {
        lookup = new Dictionary<string, Sprite>();
        foreach (var entry in entries) {
            if (!lookup.ContainsKey(entry.key)) {
                lookup[entry.key] = entry.sprite;
            }
        }
    }

    public Sprite Get(string key) {
        if (lookup == null) OnEnable(); // Fallback init
        if (lookup.TryGetValue(key, out var sprite)) {
            return sprite;
        }

        Debug.LogWarning($"[SpriteConfig] No sprite found for key: {key}");
        return null;
    }

}
