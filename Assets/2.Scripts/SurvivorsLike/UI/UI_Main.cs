using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour
{
    public Button StartButton;
    public Button Top3Button;
    public Button QuitButton;
    public TMP_InputField PlayerNameField;
    public GameObject Top3ScoreBoard;

    private void Awake()
    {
        RankMain.Instance.PostTop3Data();
    }

    void Start()
    {
        StartButton.onClick.AddListener(OnStartButtonClick);
        Top3Button.onClick.AddListener(()
            => Top3ScoreBoard.SetActive(true));
        QuitButton.onClick.AddListener(OnQuitButtonClick);

        PlayerNameField.onEndEdit.AddListener(InputPlayerName);


        GameObject gameObject = GameObject.Find("@Network");
        if (gameObject == null)
        {
            gameObject = new GameObject("@Network");
            gameObject.AddComponent<RankMain>();
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnStartButtonClick()
    {
        if (String.IsNullOrEmpty(GameManager.Instance.PlayerInfo.PlayerName))
        {
            // 예외처리
            Debug.Log(Define.Warning_Null_PlayerName);
            UI_PopUp.PopUpAction(Define.Warning_Null_PlayerName);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(Define.SurvGameScene);
        }
    }

    void InputPlayerName(string text)
    {
        GameManager.Instance.PlayerInfo.PlayerName = text;
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
