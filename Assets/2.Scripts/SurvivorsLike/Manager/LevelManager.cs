using UnityEngine;

public struct LevelInfoStruct
{
    public int Level;
    public int ExpUntilCurrentLevel;
    public int ExpToNextLevel;
    public const int MaxLevel = 50;
}

public struct WaveInfoStruct
{
    public int Wave;
    public int ScoreUntilCurrentWave;
    public int ScoreToNextWave;
    public const int MaxWave = 16;
}

public class LevelManager : Singleton<LevelManager>
{
    GameObject LevelUpPanel;

    public LevelInfoStruct LevelInfo = new LevelInfoStruct();

    public WaveInfoStruct WaveInfo = new WaveInfoStruct();

    private void Awake()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        base.Initialize();
        InitializeInfo();
        LevelUpPanel = GameObject.FindGameObjectWithTag(Define.PanelTag).transform.Find(Define.LevelUpPanelTag).gameObject;
        LevelUpPanel.SetActive(false);
    }

    public void NextLevel()
    {
        if (GameManager.Instance.Exp >= LevelInfo.ExpToNextLevel)
        {
            LevelInfo.Level++;
            // 최대 레벨 도달 시
            if (LevelInfo.Level >= LevelInfoStruct.MaxLevel)
            {
                UI_Game.GameClearAction();
                return;
            }
            LevelInfo.ExpUntilCurrentLevel = LevelInfo.ExpToNextLevel;
            LevelInfo.ExpToNextLevel += Define.LevelInterval * LevelInfo.Level;
            GameManager.Instance.GetExp(0);

            // Level Up Perks
            Time.timeScale = 0f;
            GameManager.Instance.IsPaused = true;
            LevelUpPanel = GameObject.FindGameObjectWithTag(Define.PanelTag).transform.Find(Define.LevelUpPanelTag).gameObject;
            LevelUpPanel.SetActive(true);
        }
    }

    public void NextWave()
    {
        if (GameManager.Instance.Score >= WaveInfo.ScoreToNextWave)
        {
            WaveInfo.Wave++;
            if (WaveInfo.Wave % 5 == 0)
            {
                SpawningPool.Instance.OnBossWave(WaveInfo.Wave,
                    ObjectManager.Instance.Player.transform.position);
            }
            // 최대 웨이브 통과 시
            if (WaveInfo.Wave >= WaveInfoStruct.MaxWave)
            {
                UI_Game.GameClearAction();
                return;
            }
            WaveInfo.ScoreUntilCurrentWave = WaveInfo.ScoreToNextWave;
            WaveInfo.ScoreToNextWave += Define.WaveInterval * WaveInfo.Wave;
            GameManager.Instance.GetScore(0);

            // 웨이브 단계 올라갈수록 스폰되는 몬스터의 양이 많아짐
            SpawningPool.Instance.SpawnInfo.SpawnLimit += 5;
        }
    }

    public void InitializeInfo()
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
