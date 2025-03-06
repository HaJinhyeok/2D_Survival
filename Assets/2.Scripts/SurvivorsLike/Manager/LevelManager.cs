using UnityEngine;

public struct LevelInfoStruct
{
    public int Level;
    public int ExpUntilCurrentLevel;
    public int ExpToNextLevel;
}

public class LevelManager : Singleton<LevelManager>
{
    GameObject LevelUpPanel;

    public LevelInfoStruct LevelInfo = new LevelInfoStruct()
    {
        Level = 0,
        ExpUntilCurrentLevel = 0,
        ExpToNextLevel = 10
    };

    protected override void Initialize()
    {
        base.Initialize();
        LevelUpPanel = GameObject.FindGameObjectWithTag(Define.PanelTag).transform.Find(Define.LevelUpPanelTag).gameObject;
        LevelUpPanel.SetActive(false);
    }

    public void NextLevel()
    {
        if (GameManager.Instance.Exp >= LevelInfo.ExpToNextLevel)
        {
            LevelInfo.ExpUntilCurrentLevel = LevelInfo.ExpToNextLevel;
            LevelInfo.ExpToNextLevel += Define.StageInterval * ++LevelInfo.Level;

            SpawningPool.Instance.SpawnInfo.SpawnLimit = Mathf.Min(SpawningPool.Instance.SpawnInfo.SpawnLimit + 10, 100);

            // Level Up Perks
            Time.timeScale = 0f;
            GameManager.Instance.IsPaused = true;
            LevelUpPanel.SetActive(true);
        }
    }
}
