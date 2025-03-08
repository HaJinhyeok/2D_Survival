using UnityEngine;
using System;

public struct PlayerInfo
{
    public float Atk;
    public float CurrentHp;
    public float MaxHp;
    public float Speed;
    public float MagneticDistance;
    public int AxeNum;
    public int SwordNum;
}

public class GameManager : Singleton<GameManager>
{
    protected override void Initialize()
    {
        InitiatePlayerInfo();
    }

    bool _isPaused = false;

    public bool IsPaused
    {
        get { return _isPaused; }
        set { _isPaused = value; }
    }

    // LevelUp UI È®ÀÎ¿ë
    bool _isDone = true;

    public bool IsDone
    {
        get { return _isDone; }
        set { _isDone = value; }
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
    public PlayerInfo PlayerInfo = new PlayerInfo();
    //{
    //    Atk = Define.InitAtk,
    //    CurrentHp = Define.InitMaxHp,
    //    MaxHp = Define.InitMaxHp,
    //    Speed = Define.InitSpeed,
    //    MagneticDistance = Define.InitMagneticDistance,
    //    AxeNum = Define.InitAxeNum,
    //    SwordNum = Define.InitSwordNum,
    //};

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
        LevelManager.Instance.NextWave();
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

    #region Exp
    public event Action OnExpIncreased;
    int _exp;

    public int Exp
    {
        get { return _exp; }
        set { _exp = value; }
    }

    public void GetExp(int exp=1)
    {
        _exp += exp;
        OnExpIncreased?.Invoke();
        LevelManager.Instance.NextLevel();
    }
    #endregion

    public void InitiatePlayerInfo()
    {
        PlayerInfo.Atk = Define.InitAtk;
        PlayerInfo.CurrentHp = Define.InitMaxHp;
        PlayerInfo.MaxHp = Define.InitMaxHp;
        PlayerInfo.Speed = Define.InitSpeed;
        PlayerInfo.MagneticDistance = Define.InitMagneticDistance;
        PlayerInfo.AxeNum = Define.InitAxeNum;
        PlayerInfo.SwordNum = Define.InitSwordNum;

        _score = 0;
        _money = 0;
        _exp = 0;
    }
}
