public class Define
{
    #region Tag
    public const string PlayerTag = "Player";
    public const string EnemyTag = "Enemy";
    public const string ShotTag = "Shot";
    public const string ExplosionTag = "Explosion";

    public const string PanelTag = "Panel";
    public const string LevelUpPanelTag = "LevelUpPanel";
    #endregion

    #region Path
    public const string PlayerPath = "Prefabs/Player";

    public const string EnemyDogPath = "Prefabs/Enemy_Dog";
    public const string EnemyHoodPath = "Prefabs/Enemy_Hood";
    public const string EnemySlimePath = "Prefabs/Enemy_Slime";

    public const string ShotPath = "Prefabs/Shot";
    public const string ExplosionPath = "Prefabs/Explosion";
    public const string PickaxePath = "Prefabs/Pickaxe";
    public const string PickaxeDroppedPath = "Prefabs/Pickaxe_Dropped";
    public const string SwordPath = "Prefabs/Sword";
    public const string SwordDroppedPath = "Prefabs/Sword_Dropped";
    public const string LetterPath = "Prefabs/Letter";

    public const string CoinPath = "Prefabs/Coin";
    public const string BreadPath = "Prefabs/bread";
    #endregion

    #region Scene
    public const string SurvMainScene = "1.Scenes/Main_SurvLike";
    public const string SurvGameScene = "1.Scenes/Game_SurvLike";
    public const string SurvResultScene = "1.Scenes/Result_SurvLike";
    #endregion

    #region Constants
    public const int BlinkCount = 5;
    public const float SpawnRange = 4;
    public const int StageInterval = 300;
    #endregion

    #region Warning
    public const string Warning_Not_Enough_Gold = "골드가 부족합니다";
    public const string Warning_Full_Sword = "현재 검의 개수가 이미 최대입니다";
    public const string Warning_Full_Pickaxe = "현재 곡괭이의 개수가 이미 최대입니다";
    #endregion

    #region FreePerks
    public const string SpeedUp = "SpeedUp";
    public const string HpUp = "HpUp";
    public const string AtkUp = "AtkUp";
    public const string MagneticDistanceUp = "MagneticDistanceUp";
    #endregion

    #region NonFreePerks
    public const string AddSword = "AddSword";
    public const string AddAxe = "AddAxe";
    public const string AddExplosion = "AddExplosion";
    #endregion

    public readonly static string[] PerkNameList = 
        { SpeedUp, HpUp, AtkUp, AddSword, AddAxe, AddExplosion };
}
