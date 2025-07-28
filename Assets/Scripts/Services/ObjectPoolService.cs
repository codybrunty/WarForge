using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolService {
    private readonly Dictionary<string, Queue<GameObject>> pools = new();
    private readonly Dictionary<string, GameObject> prefabLookup = new();
    private readonly Dictionary<string, Transform> poolParents = new();
    private readonly Transform globalPoolRoot;

    public ObjectPoolService() {
        GameObject root = new GameObject("PooledObjects");
        Object.DontDestroyOnLoad(root);
        globalPoolRoot = root.transform;
    }

    public void RegisterPrefab(string key, GameObject prefab, int initialSize = 5) {
        if (pools.ContainsKey(key)) return;
        if (prefab == null) {
            Debug.LogError($"Tried to register null prefab for key: {key}");
            return;
        }
        prefabLookup[key] = prefab;
        pools[key] = new Queue<GameObject>();

        // Create a unique parent for this prefab
        GameObject group = new GameObject($"{key}_Pool");
        group.transform.SetParent(globalPoolRoot);
        poolParents[key] = group.transform;

        for (int i = 0; i < initialSize; i++) {
            var obj = GameObject.Instantiate(prefab, poolParents[key]);
            obj.SetActive(false);
            pools[key].Enqueue(obj);
        }
    }

    public GameObject GetFromPool(string key) {
        if (!pools.ContainsKey(key)) {
            Debug.LogError($"No pool registered for key: {key}");
            return null;
        }

        var pool = pools[key];
        var parent = poolParents[key];

        if (pool.Count == 0) {
            return GameObject.Instantiate(prefabLookup[key], parent);
        }

        var obj = pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(string key, GameObject obj) {
        if (!pools.ContainsKey(key)) {
            Debug.LogWarning($"Tried to return to unknown pool: {key}");
            GameObject.Destroy(obj);
            return;
        }

        if (!obj.activeInHierarchy) {
            Debug.LogWarning($"Object already inactive. Possibly returned twice? {obj.name}");
            return;
        }

        obj.SetActive(false);
        obj.transform.SetParent(poolParents[key], false);
        pools[key].Enqueue(obj);
    }

}
