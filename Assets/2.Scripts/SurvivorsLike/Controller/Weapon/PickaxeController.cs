using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PickaxeController : BaseController
{
    Rigidbody2D _rigidbody2D;
    Vector2 _initPos;
    float _radius = 0;
    float _angle = 0;
    // 하나의 곡괭이가 타격할 수 있는 적의 수
    int _hitCount;
    // 곡괭이가 입히는 추가적인 대미지
    int _atk;

    readonly float[] _initAngle = { 0f, 90f, 180f, 270f };

    protected override void Initialize()
    {

    }

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        float rand = Random.Range(-5f, 5f);
        _rigidbody2D.AddForce(new Vector2(rand, 20) * 20);
        _rigidbody2D.AddTorque(rand);
        Destroy(gameObject, 3f);
        _initPos = transform.position;
        _hitCount = GameManager.Instance.WeaponInfo.AxeHitCount;
        _atk = GameManager.Instance.WeaponInfo.AxeAtk;
    }

    //private void Update()
    //{
    //    _radius += Time.deltaTime * 3f;
    //    _angle += Time.deltaTime * 60f;
    //    _angle %= 360f;

    //    // Move();
    //}

    void Move()
    {
        float offsetX = _radius * Mathf.Cos(_angle * Mathf.Deg2Rad);
        float offsetY = _radius * Mathf.Sin(_angle * Mathf.Deg2Rad);
        Vector2 newPos = _initPos + new Vector2(offsetX, offsetY);
        transform.position = newPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.EnemyTag))
        {
            collision.gameObject.GetComponent<EnemyController>().GetDamage(GameManager.Instance.PlayerInfo.Atk + _atk, ObjectManager.Instance.Player.gameObject);
            _hitCount--;
            if (_hitCount <= 0)
            {
                Destroy(gameObject);
            }
            GameManager.Instance.GetScore();
        }
    }

    public void SetAngle(int idx)
    {
        _angle += _initAngle[idx];
    }
}
