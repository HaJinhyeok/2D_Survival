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
    #region JoyStick
    public event Action<Vector2> OnMoveDirChanged;

    Vector2 _moveDir;
    Vector2 _lastDir;

    public Vector2 MoveDir
    {
        get { return _lastDir; }
        set
        {
            _moveDir = value;
            if (value != Vector2.zero)
                _lastDir = value;
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

    #region Score
    public event Action OnScoreChanged;
    [SerializeField]
    int _score;

    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
        }
    }

    public void GetScore(int score = 1)
    {
        _score += score;
        OnScoreChanged?.Invoke();
    }
    #endregion
}
