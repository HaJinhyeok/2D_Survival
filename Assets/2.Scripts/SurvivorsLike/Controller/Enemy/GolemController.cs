using UnityEngine;

public class GolemController : EnemyController
{
    protected override void Initialize()
    {
        base.Initialize();
        _speed = 2f;
        _atk = 5f;
        _isGolem = true;
    }

    private void OnEnable()
    {
        _hp = Define.InitGolemHp;
    }

    void Update()
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
        if(direction.x<0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
        transform.Translate(direction.normalized * _speed * Time.deltaTime);
    }

    // * animation effect
    void OnDieAnimationEnd()
    {
        ObjectManager.Instance.DeSpwan(this);
    }
}
