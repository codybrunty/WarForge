using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EventBus {
    private static readonly Dictionary<Type, Delegate> eventTable = new();

    public static void Subscribe<T>(Action<T> callback) {
        Type type = typeof(T);
        if (eventTable.TryGetValue(type, out var del)) {
            //already subscribed guard
            if (del.GetInvocationList().Contains(callback)) { return; }
            eventTable[type] = Delegate.Combine(del, callback);
        }
        else {
            eventTable[type] = callback;
        }
        Debug.Log($"[EventBus] Subscribed to: {type}");
    }

    public static void Unsubscribe<T>(Action<T> callback) {
        Type type = typeof(T);
        if (eventTable.TryGetValue(type, out var del)) {
            var newDel = Delegate.Remove(del, callback);
            if (newDel == null) {
                eventTable.Remove(type);
            }
            else {
                eventTable[type] = newDel;
            }
            Debug.Log($"[EventBus] Unsubscribed from: {type}");
        }
    }

    public static void Publish<T>(T eventData) {
        Type type = typeof(T);
        if (eventTable.TryGetValue(type, out var del)) {
            Debug.Log($"[EventBus] Publishing event: {type}");
            (del as Action<T>)?.Invoke(eventData);
        }
        else {
            Debug.LogWarning($"[EventBus] No subscribers for event: {type}");
        }
    }

}