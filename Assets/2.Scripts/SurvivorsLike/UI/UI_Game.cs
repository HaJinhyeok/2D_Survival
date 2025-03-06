using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Game : MonoBehaviour
{
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
        GameManager.Instance.OnScoreChanged += () => 
        { 
            ScoreText.text = GameManager.Instance.Score.ToString(); 
            OnExpChanged();
        };
        GameManager.Instance.OnTakeDamage += OnPlayerHpChanged;
        GameManager.Instance.OnMoneyChanged += () => { MoneyText.text = $":  {GameManager.Instance.Money}"; };

        ScoreText.text = GameManager.Instance.Score.ToString();
        MoneyText.text = $":  {GameManager.Instance.Money}";

        PreferencePanel.SetActive(false);
        PreferenceButton.onClick.AddListener(OnPreferenceButtonClick);
        BackButton.onClick.AddListener(OnBackButtonClick);
        QuitButton.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(Define.SurvMainScene));
    }

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

    void OnPlayerHpChanged()
    {
        PlayerHp.fillAmount = GameManager.Instance.PlayerHp / GameManager.Instance.PlayerInfo.MaxHp;
    }

    void OnExpChanged()
    {
        LevelInfoStruct tmp = LevelManager.Instance.LevelInfo;
        PlayerExp.fillAmount = (GameManager.Instance.Score - tmp.ExpUntilCurrentLevel) / (float)(tmp.ExpToNextLevel - tmp.ExpUntilCurrentLevel);
    }
}
