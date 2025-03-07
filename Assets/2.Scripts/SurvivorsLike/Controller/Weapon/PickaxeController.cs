using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PickaxeController : BaseController
{
    Rigidbody2D _rigidbody2D;
    Vector2 _initPos;
    float _radius = 0;
    float _angle = 0;
    // enemy 3개를 공격
    int _hitNum = 3;

    readonly float[] _initAngle = { 0f, 90f, 180f, 270f };

    protected override void Initialize()
    {

    }

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.AddForce(new Vector2(Random.Range(-5f, 5f), 20) * 20);
        Destroy(gameObject, 3f);
        _initPos = transform.position;
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
            collision.gameObject.GetComponent<EnemyController>().GetDamage(5, gameObject);
            _hitNum--;
            if (_hitNum <= 0)
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
