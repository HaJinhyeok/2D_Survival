public class Define
{
    public const string Top3UIName = "UI_Top3";

    #region Tag
    public const string PlayerTag = "Player";
    public const string EnemyTag = "Enemy";
    public const string ShotTag = "Shot";
    public const string ExplosionTag = "Explosion";
    public const string BleedingTag = "Bleeding";

    public const string PanelTag = "Panel";
    public const string LevelUpPanelTag = "LevelUpPanel";
    public const string FilledImageTag = "FilledImage";
    #endregion

    #region Path
    public const string PlayerPath = "Prefabs/Player";

    public const string EnemyDogPath = "Prefabs/Enemy_Dog";
    public const string EnemyHoodPath = "Prefabs/Enemy_Hood";
    public const string EnemySlimePath = "Prefabs/Enemy_Slime";

    public const string ShotPath = "Prefabs/Shot";
    public const string ExplosionPath = "Prefabs/Explosion";
    public const string BleedingPath = "Prefabs/Blood4";

    public const string PickaxePath = "Prefabs/Pickaxe";
    public const string SwordPath = "Prefabs/Sword";

    public const string MagnetPath = "Prefabs/Magnet";
    public const string CoinPath = "Prefabs/Coin";
    public const string BreadPath = "Prefabs/bread";
    public const string Exp1Path = "Prefabs/Exp1";
    public const string Exp2Path = "Prefabs/Exp2";
    public const string Exp3Path = "Prefabs/Exp3";
    #endregion

    #region Scene
    public const string SurvMainScene = "1.Scenes/Main_SurvLike";
    public const string SurvGameScene = "1.Scenes/Game_SurvLike";
    public const string SurvResultScene = "1.Scenes/Result_SurvLike";
    #endregion

    #region Initiation Constants
    // Level & Spawn Information
    public const float SpawnRange = 4;
    public const int LevelInterval = 30;
    public const int WaveInterval = 300;
    public const float MapHalfSize = 400;
    public const int InitSpawnLimit = 5;
    public readonly static int[] InitLevelInfo = { 1, 0, 10 };
    public readonly static int[] InitWaveInfo = { 1, 0, 300 };

    // Player Information
    public const float InitAtk = 3;
    public const float InitMaxHp = 100;
    public const float InitSpeed = 6;
    public const float InitMagneticDistance = 5;
    public const float InitExpMultiplier = 1;
    public const int InitAxeNum = 0;
    public const int InitSwordNum = 0;

    // Weapon Information
    public const float InitShotInterval = 1f;
    public const int InitShotNum = 4;
    public const float InitShotSpeed = 500f;
    public const float InitSwordRotationSpeed = 180f;
    public const int InitAxeHitCount = 5;
    public const int InitAxeAtk = 1;
    public const int InitExplosionAtk = 10;
    #endregion

    #region Warning
    public const string Warning_Not_Enough_Gold = "골드가 부족합니다";
    public const string Warning_Full_Sword = "현재 검의 개수가 이미 최대입니다";
    public const string Warning_Full_Pickaxe = "현재 곡괭이의 개수가 이미 최대입니다";
    public const string Warning_Null_PlayerName = "플레이어 이름을 입력해주세요";
    #endregion

    #region FreePerks
    public const string SpeedUp = "SpeedUp";
    public const string MaxHpUp = "MaxHpUp";
    public const string HpUp = "HpUp";
    public const string AtkUp = "AtkUp";
    public const string MagneticDistanceUp = "MagneticDistanceUp";
    public const string ShotIntervalDown = "ShotIntervalDown";
    public const string ShotNumUp = "ShotNumUp";
    public const string ShotSpeedUp = "ShotSpeedUp";
    public const string SwordSpeedUp = "SwordSpeedUp";
    public const string AxeAtkUp = "AxeAtkUp";
    public const string AxeHitCountUp = "AxeHitCountUp";
    #endregion

    #region NonFreePerks
    public const string AddSword = "AddSword";
    public const string AddAxe = "AddAxe";
    public const string AddExplosion = "AddExplosion";
    #endregion

    public readonly static string[] PerkNameList =
        { SpeedUp, MaxHpUp, HpUp, AtkUp, MagneticDistanceUp,
        ShotIntervalDown, ShotNumUp, ShotSpeedUp,
        SwordSpeedUp, AxeAtkUp, AxeHitCountUp,
        AddSword, AddAxe, AddExplosion };
}
