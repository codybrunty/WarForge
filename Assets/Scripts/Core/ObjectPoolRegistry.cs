using UnityEngine;

public class ObjectPoolRegistry : MonoBehaviour, IRegistry {
    [SerializeField] private PooledPrefab[] pooledPrefabs;

    public void Setup() {
        foreach (var pool in pooledPrefabs) {
            GameBootstrapper.ObjectPool.RegisterPrefab(pool.key, pool.prefab, pool.initialSize);
        }
        Debug.Log("[ObjectPoolRegistry] Pooled Prefabs registered.");
    }
}

[System.Serializable]
public struct PooledPrefab {
    public string key;
    public GameObject prefab;
    public int initialSize;
}
