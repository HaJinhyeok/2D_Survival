using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour
{
    public Button QuitButton;

    private void Awake()
    {
        //RankMain.Instance.PostTop3Data();
    }

    void Start()
    {
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
