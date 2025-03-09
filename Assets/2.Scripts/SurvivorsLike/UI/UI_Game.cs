using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Game : MonoBehaviour
{
    public TMP_Text PlayTimeText;
    public TMP_Text WaveLevelText;
    public TMP_Text MoneyText;
    public TMP_Text ScoreText;

    public GameObject PreferencePanel;

    public Button PreferenceButton;
    public Button BackButton;
    public Button QuitButton;
    
    public Image PlayerHp;
    public Image PlayerExp;

    void Start()
    {
        Timer.s_TimerAction += OnTimerWorking;

        GameManager.Instance.OnScoreChanged += OnScoreChanged;
        GameManager.Instance.OnMoneyChanged += OnMoneyChanged;
        GameManager.Instance.OnTakeDamage += OnPlayerHpChanged;
        GameManager.Instance.OnExpIncreased += OnExpChanged;

        PlayerHp.fillAmount = 1;
        PlayerExp.fillAmount = 0;

        ScoreText.text = GameManager.Instance.Score.ToString();
        MoneyText.text = $":  {GameManager.Instance.Money}";
        WaveLevelText.text = $"Wave : {LevelManager.Instance.WaveInfo.Wave}  LV. {LevelManager.Instance.LevelInfo.Level}";

        PreferencePanel.SetActive(false);
        PreferenceButton.onClick.AddListener(OnPreferenceButtonClick);
        BackButton.onClick.AddListener(OnBackButtonClick);
        QuitButton.onClick.AddListener(OnQuitButtonClick);
    
    }

    private void OnDisable()
    {
        // delegate 다시 빼주는 작업 해주어야 함 ㅋㅋ
        Timer.s_TimerAction -= OnTimerWorking;

        GameManager.Instance.OnScoreChanged -= OnScoreChanged;
        GameManager.Instance.OnMoneyChanged -= OnMoneyChanged;
        GameManager.Instance.OnTakeDamage -= OnPlayerHpChanged;
        GameManager.Instance.OnExpIncreased -= OnExpChanged;
    }

    #region Button
    void OnPreferenceButtonClick()
    {
        if (!GameManager.Instance.IsPaused)
        {
            PreferencePanel.SetActive(true);
            Time.timeScale = 0f;
            GameManager.Instance.IsPaused = true;
        }
        else
        {
            PreferencePanel.SetActive(false);
            Time.timeScale = 1f;
            GameManager.Instance.IsPaused = false;
        }
    }

    void OnBackButtonClick()
    {
        PreferencePanel.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.IsPaused = false;
    }

    void OnQuitButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Define.SurvMainScene);
        GameManager.Instance.InitiatePlayerInfo();
        LevelManager.Instance.InitiateInfo();
        Time.timeScale = 1f;
        GameManager.Instance.IsPaused = false;
    }
    #endregion

    #region Delegate
    void OnPlayerHpChanged()
    {
        PlayerHp.fillAmount = GameManager.Instance.PlayerHp / GameManager.Instance.PlayerInfo.MaxHp;
    }

    void OnExpChanged()
    {
        LevelInfoStruct tmp = LevelManager.Instance.LevelInfo;
        PlayerExp.fillAmount = (GameManager.Instance.Exp - tmp.ExpUntilCurrentLevel) / (float)(tmp.ExpToNextLevel - tmp.ExpUntilCurrentLevel);
        WaveLevelText.text = $"Wave : {LevelManager.Instance.WaveInfo.Wave}  LV. {LevelManager.Instance.LevelInfo.Level}";
    }

    void OnTimerWorking()
    {
        PlayTimeText.text = $"{Timer.s_TimeInfo:N2}";
    }

    void OnScoreChanged()
    {
        ScoreText.text = GameManager.Instance.Score.ToString();
        WaveLevelText.text = $"Wave : {LevelManager.Instance.WaveInfo.Wave}  LV. {LevelManager.Instance.LevelInfo.Level}";
    }

    void OnMoneyChanged()
    {
        MoneyText.text = $":  {GameManager.Instance.Money}";
    }

    #endregion
}
