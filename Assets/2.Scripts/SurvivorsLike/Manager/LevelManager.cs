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

    public LevelInfoStruct LevelInfo = new LevelInfoStruct();

    public WaveInfoStruct WaveInfo = new WaveInfoStruct();

    protected override void Initialize()
    {
        base.Initialize();
        LevelUpPanel = GameObject.FindGameObjectWithTag(Define.PanelTag).transform.Find(Define.LevelUpPanelTag).gameObject;
        LevelUpPanel.SetActive(false);
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == Define.SurvGameScene)
        {
            
        }        
    }

    public void NextLevel()
    {
        if (GameManager.Instance.Exp >= LevelInfo.ExpToNextLevel)
        {
            LevelInfo.ExpUntilCurrentLevel = LevelInfo.ExpToNextLevel;
            LevelInfo.ExpToNextLevel += Define.LevelInterval * LevelInfo.Level++;
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

            // 웨이브 단계 올라갈수록 스폰되는 몬스터의 양이 많아짐
            SpawningPool.Instance.SpawnInfo.SpawnLimit = Mathf.Min(SpawningPool.Instance.SpawnInfo.SpawnLimit + 5, 100);
        }
    }

    public void InitiateInfo()
    {
        Debug.Log("Initiate Information");
        LevelInfo.Level = Define.InitLevelInfo[0];
        LevelInfo.ExpUntilCurrentLevel = Define.InitLevelInfo[1];
        LevelInfo.ExpToNextLevel = Define.InitLevelInfo[2];

        WaveInfo.Wave = Define.InitWaveInfo[0];
        WaveInfo.ScoreUntilCurrentWave = Define.InitWaveInfo[1];
        WaveInfo.ScoreToNextWave = Define.InitWaveInfo[2];
    }
}
