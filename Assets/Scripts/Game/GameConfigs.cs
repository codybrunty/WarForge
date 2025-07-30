using UnityEngine;

public static class GameConfigs {
    private static ClassConfig classConfig;
    private static SpriteConfig spriteConfig;
    private static TeamConfig teamConfig;

    public static ClassConfig ClassConfig => classConfig ??= Resources.Load<ClassConfig>("Configs/ClassConfig");
    public static SpriteConfig SpriteConfig => spriteConfig ??= Resources.Load<SpriteConfig>("Configs/SpriteConfig");
    public static TeamConfig TeamConfig => teamConfig ??= Resources.Load<TeamConfig>("Configs/TeamConfig");

    public static void PreloadAllConfigs() {
        _ = ClassConfig;
        _ = SpriteConfig;
        _ = TeamConfig;
    }
    public static void ValidateConfigs() {
        if (ClassConfig == null) Debug.LogWarning("ClassConfig is missing in Resources.");
        if (SpriteConfig == null) Debug.LogWarning("SpriteConfig is missing in Resources.");
        if (TeamConfig == null) Debug.LogWarning("TeamConfig is missing in Resources.");
    }
}