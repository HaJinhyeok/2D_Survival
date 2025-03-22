using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioClip _playerHitSound;
    public AudioClip _enemyHitSound;
    public AudioClip _shotSound;
    public AudioClip _hpUpSound;
    public AudioClip _levelUpSound;
    public AudioClip _gameWinSound;
    public AudioClip _gameLoseSound;
    public AudioClip _gamePauseSound;
    public AudioClip _gameUnpauseSound;
    public AudioClip _perkSelectSound;
    public AudioClip _wrongEnterSound;

    public AudioSource EnemyHitSound;
    public AudioSource PerkSelectSound;
    public AudioSource HpUpSound;
    public AudioSource CoinSound;
    public AudioSource ExplosionSound;
    public AudioSource MagnetSound;
    public AudioSource LevelUpSound;

    public void AudioSourceLoad()
    {
        _playerHitSound = Resources.Load<AudioClip>(Define.PlayerHitSound);
        _enemyHitSound = Resources.Load<AudioClip>(Define.EnemyHitSound);
        _shotSound = Resources.Load<AudioClip>(Define.ShotSound);
        _hpUpSound = Resources.Load<AudioClip>(Define.HpUpSound);
        _levelUpSound = Resources.Load<AudioClip>(Define.LevelUpSound);
        _gameWinSound = Resources.Load<AudioClip>(Define.GameWinSound);
        _gameLoseSound = Resources.Load<AudioClip>(Define.GameLoseSound);
        _gamePauseSound = Resources.Load<AudioClip>(Define.GamePauseSound);
        _gameUnpauseSound = Resources.Load<AudioClip>(Define.GameUnpauseSound);
        _perkSelectSound = Resources.Load<AudioClip>(Define.PerkSelectSound);
        _wrongEnterSound = Resources.Load<AudioClip>(Define.WrongEneterSound);

        EnemyHitSound = GameObject.Find("EnemyHitSound").GetComponent<AudioSource>();
        PerkSelectSound = GameObject.Find("PerkSelectSound").GetComponent<AudioSource>();
        HpUpSound = GameObject.Find("HpUpSound").GetComponent<AudioSource>();
        CoinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
        ExplosionSound = GameObject.Find("ExplosionSound").GetComponent<AudioSource>();
        MagnetSound = GameObject.Find("MagnetSound").GetComponent<AudioSource>();
        LevelUpSound = GameObject.Find("LevelUpSound").GetComponent<AudioSource>();
    }
}
