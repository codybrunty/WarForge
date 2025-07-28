using System.Collections.Generic;
using UnityEngine;

public class UIService {
    private readonly Dictionary<string, GameObject> prefabLookup = new();
    private readonly Dictionary<string, GameObject> openScreens = new();
    private readonly Transform uiRoot;

    public UIService(Transform root) {
        uiRoot = root;
    }

    public void RegisterScreen(string key, GameObject prefab) {
        if (prefab == null) {
            Debug.LogWarning($"[UIService] Tried to register null prefab for key: {key}");
            return;
        }

        if (prefabLookup.ContainsKey(key)) {
            Debug.LogWarning($"[UIService] Duplicate screen key: {key}. Overwriting previous.");
        }

        prefabLookup[key] = prefab;
    }

    public GameObject OpenScreen(string key) {
        if (openScreens.ContainsKey(key)) {
            Debug.LogWarning($"[UIService] Screen already open: {key}");
            return openScreens[key];
        }

        if (!prefabLookup.TryGetValue(key, out var prefab)) {
            Debug.LogError($"[UIService] No prefab registered for key: {key}");
            return null;
        }

        GameObject screen = GameObject.Instantiate(prefab, uiRoot);
        openScreens[key] = screen;
        screen.SetActive(true);
        return screen;
    }

    public void CloseScreen(string key) {
        if (!openScreens.TryGetValue(key, out var screen)) {
            Debug.LogWarning($"[UIService] Tried to close screen not currently open: {key}");
            return;
        }

        GameObject.Destroy(screen);
        openScreens.Remove(key);
    }

    public void CloseAllScreens() {
        foreach (var screen in openScreens.Values) {
            GameObject.Destroy(screen);
        }
        openScreens.Clear();
    }

    public bool IsOpen(string key) => openScreens.ContainsKey(key);
}
