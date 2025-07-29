using UnityEngine;

public static class GameConfigs {
    private static ClassConfig _classConfig;
    private static SpriteConfig _spriteConfig;

    public static ClassConfig ClassConfig => _classConfig ??= Resources.Load<ClassConfig>("Configs/ClassConfig");

    public static SpriteConfig SpriteConfig => _spriteConfig ??= Resources.Load<SpriteConfig>("Configs/SpriteConfig");
}