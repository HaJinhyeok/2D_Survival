using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Game : MonoBehaviour
{
    public TMP_Text ScoreText;
    public GameObject PreferencePanel;
    public Button PreferenceButton;
    public Button BackButton;
    public Button QuitButton;

    bool _isPaused = false;

    void Start()
    {
        GameManager.Instance.OnScoreChanged += () => { ScoreText.text = GameManager.Instance.Score.ToString(); };

        ScoreText.text = GameManager.Instance.Score.ToString();
        PreferencePanel.SetActive(false);
        PreferenceButton.onClick.AddListener(OnPreferenceButtonClick);
        BackButton.onClick.AddListener(OnBackButtonClick);
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
}
