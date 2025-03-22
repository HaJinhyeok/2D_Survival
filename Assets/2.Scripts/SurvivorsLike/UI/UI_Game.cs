using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_Game : MonoBehaviour
{
    public TMP_Text PlayTimeText;
    public TMP_Text WaveLevelText;
    public TMP_Text MoneyText;
    public TMP_Text ScoreText;
    public TMP_Text ClearScoreText;

    public GameObject PreferencePanel;
    public GameObject StatusPanel;
    public GameObject GameOverPanel;
    public GameObject ClearPanel;

    public Button PreferenceButton;
    public Button BackButton;
    public Button QuitButton;
    public Button ExitButton;
    public Button ClearButton;
    
    public Image PlayerHp;
    public Image PlayerExp;

    public static Action GameOverAction;
    public static Action ClearAction;
    public static Action GameClearAction;

    AudioSource GameAudio;

    void Start()
    {
        Timer.s_TimerAction += OnTimerWorking;

        GameManager.Instance.OnScoreChanged += OnScoreChanged;
        GameManager.Instance.OnMoneyChanged += OnMoneyChanged;
        GameManager.Instance.OnTakeDamage += OnPlayerHpChanged;
        GameManager.Instance.OnExpIncreased += OnExpChanged;

        ObjectManager.Instance.Player.OnEscPressed += OnPreferenceButtonClick;

        GameOverAction += OnGameOver;
        ClearAction += OnClearAction;
        GameClearAction += OnGameClear;

        PlayerHp.fillAmount = 1;
        PlayerExp.fillAmount = 0;
        GameAudio = GetComponent<AudioSource>();

        ScoreText.text = GameManager.Instance.Score.ToString();
        MoneyText.text = $"{GameManager.Instance.Money}";
        WaveLevelText.text = $"WAVE : {LevelManager.Instance.WaveInfo.Wave}\tLV. {LevelManager.Instance.LevelInfo.Level}";

        PreferencePanel.SetActive(false);
        StatusPanel.SetActive(false);
        ClearPanel.SetActive(false);
        PreferenceButton.onClick.AddListener(OnPreferenceButtonClick);
        BackButton.onClick.AddListener(OnBackButtonClick);
        QuitButton.onClick.AddListener(OnQuitButtonClick);
        ExitButton.onClick.AddListener(OnExitButtonClick);
        ClearButton.onClick.AddListener(OnExitButtonClick);
    }


    #region Button
    void OnPreferenceButtonClick()
    {
        if (!GameManager.Instance.IsPaused)
        {
            GameAudio.clip = AudioManager.Instance._gamePauseSound;
            GameAudio.Play();
            PreferencePanel.SetActive(true);
            StatusPanel.SetActive(true);
            Time.timeScale = 0f;
            GameManager.Instance.IsPaused = true;
        }
        else
        {
            GameAudio.clip = AudioManager.Instance._gameUnpauseSound;
            GameAudio.Play();
            PreferencePanel.SetActive(false);
            StatusPanel.SetActive(false);
            Time.timeScale = 1f;
            GameManager.Instance.IsPaused = false;
        }
    }

    void OnBackButtonClick()
    {
        PreferencePanel.SetActive(false);
        StatusPanel.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.IsPaused = false;
    }

    void OnQuitButtonClick()
    {
        OnGameOver();
    }

    void OnExitButtonClick()
    {
        OnClearAction();
        Time.timeScale = 1f;
        GameManager.Instance.IsGameOver = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(Define.SurvResultScene);
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
    }

    void OnTimerWorking()
    {
        PlayTimeText.text = $"{Timer.s_MinuteCount:D2}:{Timer.s_SecondCount:D2}";
    }

    void OnScoreChanged()
    {
        ScoreText.text = GameManager.Instance.Score.ToString();
        WaveLevelText.text = $"WAVE : {LevelManager.Instance.WaveInfo.Wave}\tLV. {LevelManager.Instance.LevelInfo.Level}";
    }

    void OnMoneyChanged()
    {
        MoneyText.text = $"{GameManager.Instance.Money}";
    }

    void OnGameOver()
    {
        GameOverPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
        GameManager.Instance.IsGameOver = true;
        GameAudio.clip = AudioManager.Instance._gameLoseSound;
        GameAudio.Play();
    }

    void OnGameClear()
    {
        ClearScoreText.text = $"Score: {GameManager.Instance.Score}";
        ClearPanel.SetActive(true);
        Time.timeScale = 0f;
        GameManager.Instance.IsGameOver = true;
        GameAudio.clip = AudioManager.Instance._gameWinSound;
        GameAudio.Play();
    }

    void OnClearAction()
    {
        // delegate 다시 빼주는 작업 해주어야 함 ㅋㅋ
        Timer.s_TimerAction -= OnTimerWorking;
        GameManager.Instance.OnScoreChanged -= OnScoreChanged;
        GameManager.Instance.OnMoneyChanged -= OnMoneyChanged;
        GameManager.Instance.OnTakeDamage -= OnPlayerHpChanged;
        GameManager.Instance.OnExpIncreased -= OnExpChanged;


        ObjectManager.Instance.Player.OnEscPressed -= OnPreferenceButtonClick;

        GameOverAction -= OnGameOver;
        ClearAction -= OnClearAction;
    }

    #endregion
}
