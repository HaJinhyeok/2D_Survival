using System.Collections;
using UnityEngine;

public class PlayerController : BaseController
{
    public GameObject Shot;
    public GameObject Pickaxe;

    public bool IsBlinking = false;

    bool _isBlink = false;
    bool _isExplosionActivated = false;
    float _explosionRadius = 5f;
    Vector2 _moveDir;
    Color _originalColor;
    SpriteRenderer _spriteRenderer;
    Collider2D _collider;

    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set { _moveDir = value.normalized; }
    }

    protected override void Initialize()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _originalColor = _spriteRenderer.color;
        GameManager.Instance.OnMoveDirChanged += (dir) => { _moveDir = dir; };
        StartCoroutine(CoThrowPickaxe());
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(_moveDir * GameManager.Instance.PlayerInfo.Speed * Time.deltaTime);
    }

    void Rotation()
    {
        // 마우스 위치 바라보게
    }

    IEnumerator CoThrowPickaxe()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            Instantiate(Pickaxe, transform.position, Quaternion.identity);
        }
    }

    public void StartExplosion()
    {
        if (!_isExplosionActivated)
        {
            _isExplosionActivated = true;
            StartCoroutine(CoLetterExplosion());
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
                ObjectManager.Instance.DeSpwan<EnemyController>(item);
                GameManager.Instance.GetScore();
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

    // 공격받을 경우 빨갛게 번쩍이는 이펙트 추가
    public IEnumerator GetAttack(int count)
    {
        if(_isBlink)
        {
            _spriteRenderer.color = _originalColor;
            _isBlink = false;
        }
        else
        {
            _spriteRenderer.color = new Color(_originalColor.r, _originalColor.g, _originalColor.b, 0.3f);
            _isBlink = true;
        }
        yield return new WaitForSeconds(0.2f);
        if (count > 0)
        {
            StartCoroutine(GetAttack(count - 1));
        }
    }
}
