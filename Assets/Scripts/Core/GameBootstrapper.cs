using System.Collections.Generic;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour {
    public static GameBootstrapper Instance { get; private set; }
    public static SaveLoadService SaveLoad => ServiceLocator.Get<SaveLoadService>();
    public static ObjectPoolService ObjectPool => ServiceLocator.Get<ObjectPoolService>();
    public static UIService UI => ServiceLocator.Get<UIService>();
    public static RecruitService Recruit => ServiceLocator.Get<RecruitService>();
    public static NameGenerationService NameGeneration => ServiceLocator.Get<NameGenerationService>();

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
        LoadAllConfigs();
        LoadAllData();
        SubscribeToEvents();

        Debug.Log("GameBootstrapper Initialized");

        StartGame();
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
        RegisterNameGenerationService();
        RegisterRecruitService();
    }
    private void RegisterNameGenerationService() {
        ServiceLocator.Register(new NameGenerationService());
    }
    private void RegisterRecruitService() {
        ServiceLocator.Register(new RecruitService());
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

    #region Save Load Reset
    private void LoadAllData() {
        Recruit.LoadTeam();
    }
    public void SaveAllData() {
        Recruit.SaveTeam();
    }
    public void ResetAllData(ResetGameEvent evt) {
        Recruit.ResetTeam();
        UI.CloseAllScreens();
        LoadAllData();
        StartGame();
    }
    private void LoadAllConfigs() {
        GameConfigs.PreloadAllConfigs();
        GameConfigs.ValidateConfigs();
    }
    #endregion

    #region Events

    private void SubscribeToEvents() {
        EventBus.Subscribe<ResetGameEvent>(ResetAllData);
    }
    private void OnDestroy() {
        EventBus.Unsubscribe<ResetGameEvent>(ResetAllData);
    }
    #endregion

    #region Game
    private void StartGame() {
        UI.OpenScreen("MainMenu");
    }
    #endregion

}