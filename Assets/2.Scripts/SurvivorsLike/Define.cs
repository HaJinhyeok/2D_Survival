public class Define
{
    #region Tag
    public const string PlayerTag = "Player";
    public const string EnemyTag = "Enemy";
    public const string ShotTag = "Shot";
    public const string ExplosionTag = "Explosion";
    #endregion

    #region Path
    public const string PlayerPath = "Prefabs/Player";

    public const string EnemyDogPath = "Prefabs/Enemy_Dog";
    public const string EnemyHoodPath = "Prefabs/Enemy_Hood";

    public const string ShotPath = "Prefabs/Shot";
    public const string ExplosionPath = "Prefabs/Explosion";
    public const string PickaxePath = "Prefabs/Pickaxe";
    public const string PickaxeDroppedPath = "Prefabs/Pickaxe_Dropped";
    public const string SwordPath = "Prefabs/Sword";
    public const string SwordDroppedPath = "Prefabs/Sword_Dropped";
    public const string LetterPath = "Prefabs/Letter";

    public const string CoinPath = "Prefabs/Coin";
    #endregion

    #region Scene
    public const string SurvMainScene = "1.Scenes/Main_SurvLike";
    public const string SurvGameScene = "1.Scenes/Game_SurvLike";
    #endregion

    #region Constants
    public const int BlinkCount = 5;
    #endregion

    #region FreePerks
    public const string SpeedUp = "SpeedUp";
    public const string HpUp = "HpUp";
    public const string AtkUp = "AtkUp";
    #endregion

    #region NonFreePerks
    public const string AddSword = "AddSword";
    public const string AddAxe = "AddAxe";
    #endregion

    public readonly string[] PerkNameList = 
        { SpeedUp, HpUp, AtkUp, AddSword, AddAxe };
}
