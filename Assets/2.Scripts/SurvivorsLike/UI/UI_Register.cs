using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Register : MonoBehaviour
{
    public Button BackButton;
    public Button SignUpButton;
    public Button Top3Button;
    public TMP_InputField IDField;
    public TMP_InputField PasswordField;
    public TMP_InputField ConfirmPasswordField;

    public GameObject Top3ScoreBoard;
    public GameObject LoginPanel;

    void Start()
    {
        BackButton.onClick.AddListener(OnBackButtonClick);
        SignUpButton.onClick.AddListener(OnSignUpButtonClick);
        Top3Button.onClick.AddListener(()
            => Top3ScoreBoard.SetActive(true));

        IDField.onEndEdit.AddListener(InputID);
        PasswordField.onEndEdit.AddListener(InputPassword);
        ConfirmPasswordField.onEndEdit.AddListener(InputConfirmPassword);
    }

    void OnBackButtonClick()
    {
        gameObject.SetActive(false);
        LoginPanel.SetActive(true);
    }

    void OnSignUpButtonClick()
    {
        if (String.IsNullOrEmpty(GameManager.Instance.PlayerInfo.PlayerID))
        {
            // ID 공백 예외처리
            UI_PopUp.PopUpAction(Define.Warning_Null_PlayerID);
        }
        else if (String.IsNullOrEmpty(GameManager.Instance.PlayerInfo.PlayerPassword))
        {
            // Password 공백 예외처리
            UI_PopUp.PopUpAction(Define.Warning_Null_PlayerPassword);
        }
        else if(GameManager.Instance.PlayerInfo.PlayerPassword!=GameManager.Instance.PlayerInfo.ConfirmPassword)
        {
            // Password Confirm 실패 시
            UI_PopUp.PopUpAction(Define.Warning_Inappropriate_ConfirmPassword);
        }
        else
        {
            // 1. ID & Password를 req_login에 담아 PostRegister 실행
            // 2. return 받은 res_message의 cmd에 따라 상황 결정
            RankMain.Instance.PostRegisterData();
        }

    }

    void InputID(string text)
    {
        GameManager.Instance.PlayerInfo.PlayerID = text;
    }

    void InputPassword(string text)
    {
        GameManager.Instance.PlayerInfo.PlayerPassword = text;
    }

    void InputConfirmPassword(string text)
    {
        GameManager.Instance.PlayerInfo.ConfirmPassword = text;
    }
}
