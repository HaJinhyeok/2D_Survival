using UnityEngine;

public struct LevelInfoStruct
{
    public int Level;
    public int ExpUntilCurrentLevel;
    public int ExpToNextLevel;
}

public struct WaveInfoStruct
{
    public int Wave;
    public int ScoreUntilCurrentWave;
    public int ScoreToNextWave;
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

    public WaveInfoStruct WaveInfo = new WaveInfoStruct()
    {
        Wave = 1,
        ScoreUntilCurrentWave = 0,
        ScoreToNextWave = 300
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
            LevelInfo.ExpToNextLevel += Define.LevelInterval * ++LevelInfo.Level;
            GameManager.Instance.GetExp(0);

            // Level Up Perks
            Time.timeScale = 0f;
            GameManager.Instance.IsPaused = true;
            LevelUpPanel.SetActive(true);
        }
    }

    public void NextWave()
    {
        if(GameManager.Instance.Score>=WaveInfo.ScoreToNextWave)
        {
            WaveInfo.ScoreUntilCurrentWave = WaveInfo.ScoreToNextWave;
            WaveInfo.ScoreToNextWave += Define.WaveInterval * WaveInfo.Wave++;
            GameManager.Instance.GetScore(0);

            // ���̺� �ܰ� �ö󰥼��� �����Ǵ� ������ ���� ������
            SpawningPool.Instance.SpawnInfo.SpawnLimit = Mathf.Min(SpawningPool.Instance.SpawnInfo.SpawnLimit + 10, 100);
        }
    }
}
