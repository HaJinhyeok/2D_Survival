using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Result : MonoBehaviour
{
    public TMP_Text ResultScoreText;
    public Button RestartButton;
    public Button MenuButton;

    void Start()
    {
        ResultScoreText.text = $"Score : {GameManager.Instance.Score}";
        RestartButton.onClick.AddListener(() =>
        {
            GameManager.Instance.PlayerInfo.CurrentHp = GameManager.Instance.PlayerInfo.MaxHp;
            UnityEngine.SceneManagement.SceneManager.LoadScene(Define.SurvGameScene);
        });
        MenuButton.onClick.AddListener(() =>
        {
            GameManager.Instance.PlayerInfo.CurrentHp = GameManager.Instance.PlayerInfo.MaxHp;
            UnityEngine.SceneManagement.SceneManager.LoadScene(Define.SurvMainScene);
        });
    }
}
