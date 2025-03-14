using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Login : MonoBehaviour
{
    public Button StartButton;
    public Button RegisterButton;
    public Button Top3Button;
    public TMP_InputField IDField;
    public TMP_InputField PasswordField;

    public GameObject Top3ScoreBoard;
    public GameObject RegisterPanel;

    void Start()
    {
        StartButton.onClick.AddListener(OnStartButtonClick);
        RegisterButton.onClick.AddListener(OnRegisterButtonClick);
        Top3Button.onClick.AddListener(()
            => Top3ScoreBoard.SetActive(true));

        // 처음 시작 시 공백, 재시작 시 아이디는 갖고있도록
        IDField.text = GameManager.Instance.PlayerInfo.PlayerID;
        IDField.onEndEdit.AddListener(InputID);
        PasswordField.onEndEdit.AddListener(InputPassword);
    }

    void OnStartButtonClick()
    {
        if (String.IsNullOrEmpty(GameManager.Instance.PlayerInfo.PlayerID))
        {
            // ID 공백 예외처리
            // Debug.Log(Define.Warning_Null_PlayerID);
            UI_PopUp.PopUpAction(Define.Warning_Null_PlayerID);
        }
        else if(String.IsNullOrEmpty(GameManager.Instance.PlayerInfo.PlayerPassword))
        {
            // Password 공백 예외처리
            UI_PopUp.PopUpAction(Define.Warning_Null_PlayerPassword);
        }
        else
        {
            // 1. ID & Password를 req_login에 담아 PostLogin 실행
            // 2. return 받은 res_message의 cmd에 따라 로그인할지 말지 결정            
            RankMain.Instance.PostLoginData();            
        }
    }

    void OnRegisterButtonClick()
    {
        gameObject.SetActive(false);
        RegisterPanel.SetActive(true);
    }

    void InputID(string text)
    {
        GameManager.Instance.PlayerInfo.PlayerID = text;
    }

    void InputPassword(string text)
    {
        GameManager.Instance.PlayerInfo.PlayerPassword = text;
    }
}
