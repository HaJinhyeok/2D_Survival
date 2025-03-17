using UnityEngine;

public class HoodController : EnemyController
{
    protected override void Initialize()
    {
        base.Initialize();
        _speed = 5f;
        _atk = 1f;
    }
    private void OnEnable()
    {
        _hp = Define.InitHoodHp;
    }

    private void Update()
    {
        if (_isAttacked)
        {
            _coolTime += Time.deltaTime;
        }
        if (_coolTime >= _interval)
        {
            _coolTime = 0;
            _isAttacked = false;
        }
        ChasePlayer();
    }

    protected override void ChasePlayer()
    {
        Vector3 direction = _target.position - transform.position;
        transform.Translate(direction.normalized * _speed * Time.deltaTime);
    }
}
