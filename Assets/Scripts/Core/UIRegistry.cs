using UnityEngine;
using System.Collections.Generic;

public class UIRegistry : MonoBehaviour, IRegistry {

    [SerializeField] private UIScreen[] screens;

    public void Setup() {
        foreach (var screen in screens) {
            GameBootstrapper.UIService.RegisterScreen(screen.key, screen.prefab);
        }
        Debug.Log("[UIRegistry] UI Screens registered.");
    }
}

[System.Serializable]
public struct UIScreen {
    public string key;
    public GameObject prefab;
}