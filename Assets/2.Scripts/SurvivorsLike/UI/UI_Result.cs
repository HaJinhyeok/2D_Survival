using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Result : MonoBehaviour
{
    public TMP_Text ResultScoreText;
    public Button RestartButton;
    public Button MenuButton;
    public Button Top3Button;
    public Button QuitButton;
    public GameObject Top3ScoreBoard;

    private void Awake()
    {
        RankMain.Instance.PostUpdateData();
        //RankMain.Instance.PostTop3Data();
    }

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
        QuitButton.onClick.AddListener(OnQuitButtonClick);
    }

    void OnQuitButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
