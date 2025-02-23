using UnityEngine;
using TMPro;

public class VamGameManager : MonoBehaviour
{
    #region SingleTon

    private static VamGameManager s_instance = null;

    public static VamGameManager Instance
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

    public void GetScore()
    {
        _score++;
        ScoreText.text = _score.ToString();
    }
}
