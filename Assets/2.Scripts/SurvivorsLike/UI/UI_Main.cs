using UnityEngine;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour
{
    public Button StartButton;

    void Start()
    {
        StartButton.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(Define.SurvGameScene));
        GameObject gameObject = GameObject.Find("@Network");
        if(gameObject == null)
        {
            gameObject = new GameObject("@Network");
            gameObject.AddComponent<RankMain>();
            DontDestroyOnLoad(gameObject);
        }
    }
}
