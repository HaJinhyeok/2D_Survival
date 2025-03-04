using UnityEngine;
using System;

public struct PlayerInfo
{
    public float Atk;
    public float CurrentHp;
    public float MaxHp;
    public float Speed;
    public int AxeNum;
    public int SwordNum;
}

public class GameManager : Singleton<GameManager>
{
    bool _isPaused = false;

    public bool IsPaused
    {
        get { return _isPaused; }
        set { _isPaused = value; }
    }

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
    public event Action OnTakeDamage;
    public PlayerInfo PlayerInfo = new PlayerInfo()
    {
        Atk = 1,
        CurrentHp = 100,
        MaxHp = 100,
        Speed = 6,
        AxeNum = 0,
        SwordNum = 0,
    };

    public float PlayerHp
    {
        get { return PlayerInfo.CurrentHp; }
        set
        {
            PlayerInfo.CurrentHp = value;
            OnTakeDamage?.Invoke();
        }
    }

    public GameObject Target { get; set; }
    #endregion

    #region Score
    public event Action OnScoreChanged;
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

    #region Money
    public event Action OnMoneyChanged;
    int _money;

    public int Money
    {
        get { return _money; }
        set
        {
            _money = value;
            OnMoneyChanged?.Invoke();
        }
    }

    public void GetMoney(int money = 5)
    {
        _money += money;
        OnMoneyChanged?.Invoke();
    }
    #endregion
}
