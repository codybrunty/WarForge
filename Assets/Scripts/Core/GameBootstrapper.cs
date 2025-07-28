using UnityEngine;

public class GameBootstrapper : MonoBehaviour {
    public static GameBootstrapper Instance { get; private set; }
    public static SaveLoadService SaveLoad => ServiceLocator.Get<SaveLoadService>();
    public static ObjectPoolService ObjectPool => ServiceLocator.Get<ObjectPoolService>();
    public static UIService UIService => ServiceLocator.Get<UIService>();

    [SerializeField] private Transform uiRoot;

    void Awake() {        
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SetupAllServices();
        SetupAllRegistries();

        Debug.Log("Game Bootstrapper initialized.");
    }

    #region Registeries
    private void SetupAllRegistries() {
        foreach (var registry in GetComponents<IRegistry>()) {
            Debug.Log($"[Bootstrapper] Setting up registry: {registry.GetType().Name}");
            registry.Setup();
        }
    }
    #endregion

    #region Service Registration
    private void SetupAllServices() {
        RegisterSaveLoadService();
        RegisterObjectPoolService();
        RegisterUIService();
    }
    private void RegisterSaveLoadService() {
        ServiceLocator.Register(new SaveLoadService());
    }
    private void RegisterObjectPoolService() {
        ServiceLocator.Register(new ObjectPoolService());

    }
    private void RegisterUIService() {
        ServiceLocator.Register(new UIService(uiRoot));
    }
    #endregion

}