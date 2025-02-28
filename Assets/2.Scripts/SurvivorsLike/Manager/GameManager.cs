using UnityEngine;
using TMPro;
using System;

public struct PlayerInfo
{
    public float Atk;
    public float CurrentHp;
    public float MaxHp;
    public float Speed;
}

public class GameManager : Singleton<GameManager>
{
    //#region SingleTon

    //private static GameManager s_instance = null;

    //public static GameManager Instance
    //{
    //    get
    //    {
    //        if (s_instance == null)
    //            return null;
    //        return s_instance;
    //    }
    //}

    //private void Awake()
    //{
    //    if (s_instance == null)
    //        s_instance = this;
    //    else
    //        Destroy(gameObject);
    //}

    //#endregion

    #region JoyStick
    public event Action<Vector2> OnMoveDirChanged;

    Vector2 _moveDir;

    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set 
        { 
            _moveDir = value;
            OnMoveDirChanged?.Invoke(value);
        }
    }
    #endregion

    #region PlayerInfo
    public PlayerInfo PlayerInfo = new PlayerInfo()
    {
        Atk = 1,
        CurrentHp = 100,
        MaxHp = 100,
        Speed = 2
    };

    public GameObject Target { get; set; }
    #endregion

    #region ScoreText
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
    #endregion
}
