public class Define
{
    #region ObjectPath
    public const string Top3UIName = "UI_Top3";
    public const string GameUIName = "UI_Game";

    public const string HpExpPanel = "UI_Game/HpExpPanel";
    public const string InformationPanel = "UI_Game/InformationPanel";

    public const string WhiteMaterialPath = "Materials/PaintWhite";
    #endregion

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

    #region PrefabsPath
    public const string PlayerPath = "Prefabs/Player";

    public const string EnemyDogPath = "Prefabs/Enemy_Dog";
    public const string EnemyHoodPath = "Prefabs/Enemy_Hood";
    public const string EnemySlimePath = "Prefabs/Enemy_Slime";
    public const string EnemyGolemPath = "Prefabs/Golem";
    public const string EnemyReinforcedGolemPath = "Prefabs/ReinforcedGolem";

    public const string ShotPath = "Prefabs/Shot";
    public const string GolemShotPath = "Prefabs/GolemShot";
    public const string ExplosionPath = "Prefabs/Explosion";
    public const string BleedingPath = "Prefabs/Blood4";

    public const string PickaxePath = "Prefabs/Pickaxe";
    public const string SwordPath = "Prefabs/Sword";
    public const string AuraPath = "Prefabs/Aura";

    public const string MagnetPath = "Prefabs/Magnet";
    public const string CoinPath = "Prefabs/Coin";
    public const string BreadPath = "Prefabs/bread";
    public const string Exp1Path = "Prefabs/Exp1";
    public const string Exp2Path = "Prefabs/Exp2";
    public const string Exp3Path = "Prefabs/Exp3";
    public const string Exp4Path = "Prefabs/Exp4";
    public const string Exp5Path = "Prefabs/Exp5";

    public const string DamageTextPath = "Prefabs/DamageText";
    #endregion

    #region Scene
    public const string SurvMainScene = "1.Scenes/Main_SurvLike";
    public const string SurvGameScene = "1.Scenes/Game_SurvLike";
    public const string SurvResultScene = "1.Scenes/Result_SurvLike";
    #endregion

    #region Initiation Constants
    // Level & Spawn Information
    public const float SpawnRange = 4f;
    public const int LevelInterval = 30;
    public const int WaveInterval = 300; // 300
    public const int InitSpawnLimit = 5;
    public readonly static int[] InitLevelInfo = { 1, 0, 10 };
    public readonly static int[] InitWaveInfo = { 1, 0, 300 }; // 1, 0, 300

    // Player Information
    public const float InitAtk = 3;
    public const float InitMaxHp = 100;
    public const float InitSpeed = 6;
    public const float InitMagneticDistance = 5f;
    public const float InitExpMultiplier = 1f;
    public const int InitAxeNum = 0;
    public const int InitSwordNum = 0;

    // Weapon Information
    public const float InitShotInterval = 1f;
    public const int InitShotNum = 4;
    public const float InitShotSpeed = 10f;
    public const float InitSwordRotationSpeed = 180f;
    public const int InitAxeHitCount = 5;
    public const float InitAxeAtk = 1;
    public const float InitExplosionAtk = 10;
    public const float InitExplosionRadius = 5f;
    public const float InitExplosionInterval = 5f;
    public const float InitAuraAtk = 0;
    public const float InitAuraRadius = 2f;

    // Enemy Infromation
    public const float InitDogHp = 3f;
    public const float InitHoodHp = 3f;
    public const float InitSlimeHp = 7f;
    public const float InitGolemHp = 15f;
    public const float InitReinforcedGolemHp = 30f;
    public const float InitReinforcedAttackingGolemHp = 50f;
    #endregion

    #region Limit Constants
    public const float MaxSpeed = 8f;
    public const float MaxAtk = 10f;
    public const float MaxMagneticDistance = 10f;
    public const float MinShotInterval = 0.5f;
    public const int MaxShotNum = 36;
    public const float MaxShotSpeed = 15;
    public const float MaxSwordSpeed = 360;
    public const float MaxAxeAtk = 10;
    public const int MaxAxeHitCount = 15;
    public const float MaxExpMultiplier = 2f;
    public const int MaxSwordNum = 4;
    public const int MaxAxeNum = 4;
    public const float MaxExplosionRadius = 10f;
    public const float MinExplosionInterval = 3f;
    public const float MaxAuraAtk = 3;
    #endregion

    #region PopUp Phrase
    public const string Warning_Not_Enough_Gold = "골드가 부족합니다";
    public const string Warning_Full_Sword = "현재 검의 개수가 이미 최대입니다";
    public const string Warning_Full_Pickaxe = "현재 곡괭이의 개수가 이미 최대입니다";
    public const string Warning_Full_Aura_Atk = "현재 오오라의 공격력이 이미 최대입니다";

    public const string Warning_Null_PlayerID = "ID를 입력해주세요";
    public const string Warning_Null_PlayerPassword = "비밀번호를 입력해주세요";
    public const string Warning_Inappropriate_PlayerID = "존재하지 않는 ID입니다";
    public const string Warning_Inappropriate_PlayerPassword = "비밀번호가 올바르지 않습니다";
    public const string Warning_Inappropriate_ConfirmPassword = "비밀번호가 일치하지 않습니다";
    public const string Warning_Already_Exist_ID = "이미 존재하는 ID입니다";
    #endregion

    #region Perks
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
    public const string ExpMultiplierUp = "ExpMultiplierUp";

    public const string AddSword = "AddSword";
    public const string AddAxe = "AddAxe";
    public const string ExplosionRadiusUp = "ExplosionRadiusUp";
    public const string ExplosionIntervalDown = "ExplosionIntervalDown";
    public const string AuraUpgrade = "AuraUpgrade";
    #endregion

    #region PerkImagePath
    public const string QuickImagePath = "Images/quick";
    public const string MaxHpImagePath = "Images/maxHp";
    public const string HealImagePath = "Images/heal";
    public const string BuffImagePath = "Images/buff";
    public const string MagnetImagePath = "Images/Magnet";
    public const string ShotImagePath = "Images/shot";
    public const string ShotNumImagePath = "Images/shotNum";
    public const string ShotSpeedImagePath = "Images/shotSpeed";
    public const string SwordSpeedImagePath = "Images/swordSpeed";
    public const string SwordImagePath = "Images/sword";
    public const string AxeImagePath = "Images/pickaxe";
    public const string ExpImagePath = "Images/exp";
    public const string ExplosionImagePath = "Images/explosion";
    public const string AuraImagePath = "Images/aura";
    #endregion

    #region SoundPath
    public const string PlayerHitSound = "Sound/PlayerHit";
    public const string EnemyHitSound = "Sound/EnemyHit";
    public const string ShotSound = "Sound/Shot";
    public const string HpUpSound = "Sound/HpUp";
    public const string LevelUpSound = "Sound/LevelUp";
    public const string GameWinSound = "Sound/Win";
    public const string GameLoseSound = "Sound/Lose";
    public const string GamePauseSound = "Sound/Pause";
    public const string GameUnpauseSound = "Sound/Unpause";
    public const string PerkSelectSound = "Sound/PerkSelect";
    public const string WrongEneterSound = "Sound/WrongEneter";
    #endregion

}
