using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region SingleTon

    private static GameManager s_instance = null;

    public static GameManager Instance
    {
        get
        {
            if (s_instance == null)
                return null;
            return s_instance;
        }
    }

    private void Awake()
    {
        if (s_instance == null)
            s_instance = this;
        else
            Destroy(gameObject);
    }

    #endregion

    public TMP_Text ScoreText;

    int _score;

    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }

    void Start()
    {
        ScoreText.text = _score.ToString();
    }

    public void GetScore(int score = 1)
    {
        _score += score;
        ScoreText.text = _score.ToString();
    }
}
