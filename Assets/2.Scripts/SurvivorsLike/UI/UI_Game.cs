using UnityEngine;
using TMPro;

public class UI_Game : MonoBehaviour
{
    public TMP_Text ScoreText;

    void Start()
    {
        ScoreText.text = GameManager.Instance.Score.ToString();
        GameManager.Instance.OnScoreChanged += () => { ScoreText.text = GameManager.Instance.Score.ToString(); };
    }
}
