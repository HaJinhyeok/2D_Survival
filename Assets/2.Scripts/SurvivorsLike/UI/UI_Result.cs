using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Result : MonoBehaviour
{
    public TMP_Text ResultScoreText;
    public Button RestartButton;
    public Button MenuButton;
    public Button Top3Button;
    public GameObject Top3ScoreBoard;

    void Start()
    {
        ResultScoreText.text = $"Score : {GameManager.Instance.Score}";
        RestartButton.onClick.AddListener(() =>
        {
            GameManager.Instance.InitiatePlayerInfo();
            LevelManager.Instance.InitiateInfo();
            UnityEngine.SceneManagement.SceneManager.LoadScene(Define.SurvGameScene);
        });
        MenuButton.onClick.AddListener(() =>
        {
            GameManager.Instance.InitiatePlayerInfo();
            LevelManager.Instance.InitiateInfo();
            UnityEngine.SceneManagement.SceneManager.LoadScene(Define.SurvMainScene);
        });
        Top3Button.onClick.AddListener(()
            => Top3ScoreBoard.SetActive(true));
    }
}
