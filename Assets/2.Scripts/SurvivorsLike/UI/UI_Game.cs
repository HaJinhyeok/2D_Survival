using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Game : MonoBehaviour
{
    public TMP_Text ScoreText;
    public TMP_Text MoneyText;
    public GameObject PreferencePanel;
    public Button PreferenceButton;
    public Button BackButton;
    public Button QuitButton;
    public Image PlayerHp;

    bool _isPaused = false;

    void Start()
    {
        GameManager.Instance.OnScoreChanged += () => { ScoreText.text = GameManager.Instance.Score.ToString(); };
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
        if (!_isPaused)
        {
            PreferencePanel.SetActive(true);
            Time.timeScale = 0f;
            _isPaused = true;
        }
        else
        {
            PreferencePanel.SetActive(false);
            Time.timeScale = 1f;
            _isPaused = false;
        }
    }

    void OnBackButtonClick()
    {
        PreferencePanel.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }

    void OnPlayerHpChanged()
    {
        PlayerHp.fillAmount = GameManager.Instance.PlayerHp / GameManager.Instance.PlayerInfo.MaxHp;
    }
}
