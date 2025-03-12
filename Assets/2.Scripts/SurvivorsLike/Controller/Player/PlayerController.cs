using System.Collections;
using UnityEngine;

public class PlayerController : BaseController, IMagnetic
{
    int _blinkFlag = 1;
    bool _isBlink = false;
    bool _isExplosionActivated = false;

    float _explosionRadius = 5f; // 폭발 범위
    float _explosionInterval = 5f; // 폭발 간격

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

    public float Speed { get; set; } = 10f;

    protected override void Initialize()
    {
        StatusPanel = GameObject.Find("UI_Game/StatusPanel");
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _currentColor = _spriteRenderer.color;
        GameManager.Instance.OnMoveDirChanged += (dir) => { _moveDir = dir; };
        StartCoroutine(CoThrowPickaxe());
    }

    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        PullItemsAround(gameObject, GameManager.Instance.PlayerInfo.MagneticDistance);
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
            StatusPanel.transform.position =
            Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y - 1.5f, 0));
        }
        else
        {
            _animator.SetBool("Run", false);
        }

        //Vector3 currentPos = transform.position;
        //currentPos.x = Mathf.Clamp(transform.position.x, -Define.MapHalfSize + 1, Define.MapHalfSize - 1);
        //currentPos.y = Mathf.Clamp(transform.position.y, -Define.MapHalfSize + 1, Define.MapHalfSize - 1);
        //transform.position = currentPos;


    }

    void Rotation()
    {
        // 마우스 위치 바라보게
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
        foreach (var item in ObjectManager.Instance.Enemies)
        {
            if ((transform.position - item.transform.position).sqrMagnitude < _explosionRadius * _explosionRadius)
            {
                item.GetDamage(GameManager.Instance.WeaponInfo.ExplosionAtk, ObjectManager.Instance.Player.gameObject);
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
        PoolManager.Instance.PullItems(origin, distance, Speed);
    }
}
