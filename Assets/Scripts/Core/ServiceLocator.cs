using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator {
    private static readonly Dictionary<Type, object> services = new();

    public static void Register<T>(T service) {
        Type type = typeof(T);
        if (services.ContainsKey(type)) {
            Debug.LogWarning($"Service {type} is already registered. Replacing.");
        }
        services[type] = service;
    }

    public static void Unregister<T>() {
        Type type = typeof(T);
        if (services.ContainsKey(type)) {
            services.Remove(type);
        }
    }
    public static bool IsRegistered<T>() {
        return services.ContainsKey(typeof(T));
    }
    public static T Get<T>() {
        Type type = typeof(T);
        if (services.TryGetValue(type, out var service)) {
            return (T)service;
        }
        Debug.LogError($"Service {type} not found. Did you forget to register it?");
        return default;
    }

}