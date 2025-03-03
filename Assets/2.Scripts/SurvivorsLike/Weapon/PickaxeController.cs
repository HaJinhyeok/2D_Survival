using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PickaxeController : BaseController
{
    // 2차 함수가 아니라, 그냥 반지름이 점점 커지는 원을 그리도록 만들자
    Vector2 _initPos;
    float _radius = 0;
    float _angle = 0;

    readonly float[] _initAngle = { 0f, 90f, 180f, 270f };

    protected override void Initialize()
    {

    }

    void Start()
    {
        Destroy(gameObject, 5f);
        _angle = _initAngle[Random.Range(0, _initAngle.Length)];
        _initPos = transform.position;
    }

    private void Update()
    {
        _radius += Time.deltaTime * 3f;
        _angle += Time.deltaTime * 60f;
        _angle %= 360f;

        Move();
    }

    void Move()
    {
        float offsetX = _radius * Mathf.Cos(_angle * Mathf.Deg2Rad);
        float offsetY = _radius * Mathf.Sin(_angle * Mathf.Deg2Rad);
        Vector2 newPos = _initPos + new Vector2(offsetX, offsetY);
        transform.position = newPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.EnemyTag))
        {
            collision.gameObject.SetActive(false);
            GameManager.Instance.GetScore();
        }
    }
}
