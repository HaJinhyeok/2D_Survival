using System;
using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;

public class PlayerController : BaseController, IMagnetic
{
    int _blinkFlag = 1;
    bool _isBlink = false;
    bool _isExplosionActivated = false;

    float _explosionRadius = 5f; // 폭발 범위
    float _explosionInterval = 5f; // 폭발 간격
    float _timeCount = 0;
    bool _isMagnetOn = false;

    Vector2 _moveDir;
    Color _currentColor;
    SpriteRenderer _spriteRenderer;
    Animator _animator;
    GameObject StatusPanel;

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
        _currentColor = _spriteRenderer.color;
        _isMagnetOn = false;

        GameManager.Instance.OnMoveDirChanged += (dir) => { _moveDir = dir; };
        StartCoroutine(CoThrowPickaxe());
    }

    void Update()
    {
        Move();
        if(Input.GetKeyDown(KeyCode.Escape))
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

        //Vector3 currentPos = transform.position;
        //currentPos.x = Mathf.Clamp(transform.position.x, -Define.MapHalfSize + 1, Define.MapHalfSize - 1);
        //currentPos.y = Mathf.Clamp(transform.position.y, -Define.MapHalfSize + 1, Define.MapHalfSize - 1);
        //transform.position = currentPos;


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
        if (!_isExplosionActivated)
        {
            _isExplosionActivated = true;
            StartCoroutine(CoLetterExplosion());
        }
        else
        {
            // 폭발 범위 증가 or 간격 감소
            // 범위는 최대 10f, 간격은 최소 3sec
            int rand = Random.Range(0, 2);
            switch (rand)
            {
                case 0:
                    _explosionRadius = Mathf.Min(10f, _explosionRadius + 1);
                    Debug.Log("폭발 범위 증가!!!");
                    break;

                case 1:
                    _explosionInterval = Mathf.Max(3f, _explosionInterval - 1);
                    Debug.Log("폭발 간격 감소!!!");
                    break;
            }
        }
    }

    IEnumerator CoLetterExplosion()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            Explosion();
            ObjectManager.Instance.ExplosionEffect();
        }
    }

    void Explosion()
    {
        foreach (var enemy in ObjectManager.Instance.Enemies)
        {
            if ((transform.position - enemy.transform.position).sqrMagnitude < _explosionRadius * _explosionRadius)
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
            StopCoroutine(CoLetterExplosion());
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
