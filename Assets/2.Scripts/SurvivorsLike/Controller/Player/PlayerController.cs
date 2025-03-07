using System.Collections;
using UnityEngine;

public class PlayerController : BaseController, IMagnetic
{
    public GameObject Shot;

    int _blinkFlag = 1;
    bool _isBlink = false;
    bool _isExplosionActivated = false;

    float _explosionRadius = 5f; // ���� ����
    float _explosionInterval = 5f; // ���� ����

    Vector2 _moveDir;
    Color _currentColor;
    SpriteRenderer _spriteRenderer;
    Collider2D _collider;

    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set { _moveDir = value.normalized; }
    }

    public float Speed { get; set; } = 10f;

    protected override void Initialize()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
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
        transform.Translate(_moveDir * GameManager.Instance.PlayerInfo.Speed * Time.deltaTime);
    }

    void Rotation()
    {
        // ���콺 ��ġ �ٶ󺸰�
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
            // ���� ���� ���� or ���� ����
            // ������ �ִ� 10f, ������ �ּ� 3sec
            int rand = Random.Range(0, 2);
            switch (rand)
            {
                case 0:
                    _explosionRadius = Mathf.Min(10f, _explosionRadius + 1);
                    Debug.Log("���� ���� ����!!!");
                    break;

                case 1:
                    _explosionInterval = Mathf.Max(3f, _explosionInterval - 1);
                    Debug.Log("���� ���� ����!!!");
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

    #endregion

    // ���ݹ��� ��� ������ ��½�̴� ����Ʈ �߰�
    public void GetAttack()
    {
        if (!_isBlink)
        {
            _isBlink = true;
            StartCoroutine(Blink());
            _isBlink = false;
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
