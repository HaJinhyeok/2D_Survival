using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class PlayerController : BaseController, IMagnetic
{
    int _blinkFlag = 1;
    bool _isBlink = false;
    bool _isExplosionActivated = false;

    float _timeCount = 0;
    bool _isMagnetOn = false;

    Vector2 _moveDir;
    Color _currentColor;
    SpriteRenderer _spriteRenderer;
    Animator _animator;
    GameObject StatusPanel;
    AudioSource _playerAudioSource;

    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set { _moveDir = value.normalized; }
    }

    public float PullSpeed { get; set; } = 10f;

    public event Action OnEscPressed;

    protected override void Initialize()
    {
        StatusPanel = GameObject.Find(Define.HpExpPanel);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _playerAudioSource = GetComponent<AudioSource>();
        _currentColor = _spriteRenderer.color;
        _isMagnetOn = false;

        GameManager.Instance.OnMoveDirChanged += (dir) => { _moveDir = dir; };
        StartCoroutine(CoThrowPickaxe());
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.IsGameOver)
        {
            OnEscPressed?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        // 자석 효과 켜진 1초 동안
        if (_isMagnetOn)
        {
            PoolManager.Instance.PullItems(gameObject, 1000f, 100f);
            _timeCount += Time.deltaTime;
            if (_timeCount > 1f)
            {
                _isMagnetOn = false;
            }
        }
        // 평상시
        else
        {
            PullItemsAround(gameObject, GameManager.Instance.PlayerInfo.MagneticDistance);
        }
    }

    void Move()
    {
        if (_moveDir != Vector2.zero)
        {
            _animator.SetBool("Run", true);
            transform.Translate(_moveDir * GameManager.Instance.PlayerInfo.Speed * Time.deltaTime);
            if (_moveDir.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
            else
            {
                _spriteRenderer.flipX = false;
            }
        }
        else
        {
            _animator.SetBool("Run", false);
        }
        StatusPanel.transform.position =
        Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y - 1.5f, 0));
    }

    #region WeaponSystem
    IEnumerator CoThrowPickaxe()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            for (int i = 0; i < GameManager.Instance.PlayerInfo.AxeNum; i++)
            {
                PickaxeController pickaxeController = ObjectManager.Instance.Spawn<PickaxeController>(transform.position);

                pickaxeController.SetAngle(i);
            }
        }
    }

    public void StartExplosion()
    {
        _isExplosionActivated = true;
        StartCoroutine(CoInvokeExplosion());
    }

    IEnumerator CoInvokeExplosion()
    {
        while (_isExplosionActivated)
        {
            yield return new WaitForSeconds(GameManager.Instance.WeaponInfo.ExplosionInterval);
            Explosion();
            AudioManager.Instance.ExplosionSound.Play();
            ObjectManager.Instance.ExplosionEffect();
        }
    }

    void Explosion()
    {
        HashSet<EnemyController> enemyControllers = new HashSet<EnemyController>(ObjectManager.Instance.Enemies);
        foreach (var enemy in enemyControllers)
        {
            if ((transform.position - enemy.transform.position).sqrMagnitude
                < GameManager.Instance.WeaponInfo.ExplosionRadius * GameManager.Instance.WeaponInfo.ExplosionRadius
                && enemy.gameObject.activeSelf)
            {
                enemy.GetDamage(GameManager.Instance.WeaponInfo.ExplosionAtk, ObjectManager.Instance.Player.gameObject);
            }
        }
    }

    public void EndExplosion()
    {
        if (_isExplosionActivated)
        {
            _isExplosionActivated = false;
            StopCoroutine(CoInvokeExplosion());
        }
    }

    #endregion

    // 공격받을 경우 블리딩 이펙트 추가
    public void GetAttack()
    {
        if (!_isBlink)
        {
            _isBlink = true;
            StartCoroutine(Blink());
            _isBlink = false;
            ObjectManager.Instance.BleedingEffect();
        }
    }

    public void Sound()
    {
        _playerAudioSource.Play();
    }

    IEnumerator Blink()
    {
        _currentColor.g -= 0.2f * _blinkFlag;

        if (_currentColor.g <= 0)
        {
            _currentColor.g = 0;
            _spriteRenderer.color = _currentColor;
            _blinkFlag = -1;
        }

        if (_currentColor.g >= 1)
        {
            _currentColor.g = 1;
            _spriteRenderer.color = _currentColor;
            _blinkFlag = 1;
            yield break;
        }

        yield return new WaitForSeconds(0.05f);
        StartCoroutine(Blink());
    }

    public void PullItemsAround(GameObject origin, float distance)
    {
        PoolManager.Instance.PullItems(origin, distance, PullSpeed);
    }

    public void StartPullAllItems()
    {
        _timeCount = 0;
        _isMagnetOn = true;
    }

}
