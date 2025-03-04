using System.Collections;
using UnityEngine;

public class PlayerController : BaseController, IMagnetic
{
    public GameObject Shot;

    public bool IsBlinking = false;

    bool _isBlink = false;
    bool _isExplosionActivated = false;

    float _explosionRadius = 5f; // 폭발 범위
    float _explosionInterval = 5f; // 폭발 간격

    Vector2 _moveDir;
    Color _originalColor;
    SpriteRenderer _spriteRenderer;
    Collider2D _collider;

    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set { _moveDir = value.normalized; }
    }

    public float Speed { get; set; } = 8f;

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

    private void FixedUpdate()
    {
        PullItemsAround(gameObject, 5f);
    }

    void Move()
    {
        transform.Translate(_moveDir * GameManager.Instance.PlayerInfo.Speed * Time.deltaTime);
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
        if (_isBlink)
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
    #endregion

    public void PullItemsAround(GameObject origin, float distance)
    {
        PoolManager.Instance.PullItems(origin, distance, Speed);
    }
}
