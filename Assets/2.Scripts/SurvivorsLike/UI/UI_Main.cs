using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour
{
    public Button StartButton;
    public Button Top3Button;
    public TMP_InputField PlayerNameField;
    public GameObject Top3ScoreBoard;

    void Start()
    {
        StartButton.onClick.AddListener(()
            => UnityEngine.SceneManagement.SceneManager.LoadScene(Define.SurvGameScene));
        Top3Button.onClick.AddListener(()
            => Top3ScoreBoard.SetActive(true));
        
        PlayerNameField.onEndEdit.AddListener(InputPlayerName);


        GameObject gameObject = GameObject.Find("@Network");
        if (gameObject == null)
        {
            gameObject = new GameObject("@Network");
            gameObject.AddComponent<RankMain>();
            DontDestroyOnLoad(gameObject);
        }
    }

    void InputPlayerName(string text)
    {
        GameManager.Instance.PlayerInfo.PlayerName = text;
    }
}
