using UnityEngine;

public class ReinforcedGolemController : EnemyController
{
    public GameObject GolemShot;

    bool _isPossibleAttack = false;
    float _attackCoolTime = 0;
    const float _attackInterval = 5f;
    const float _lifeTime = 30f;

    protected override void Initialize()
    {
        base.Initialize();
        _isGolem = true;
        _atk = 10f;
        _speed = 2f;
    }
    private void OnEnable()
    {
        if (LevelManager.Instance.WaveInfo.Wave >= WaveInfoStruct.MaxWave - 1)
        {
            _isPossibleAttack = true;
            _speed = 1f;
            _hp = Define.InitReinforcedAttackingGolemHp;
        }
        else
        {
            _hp = Define.InitReinforcedGolemHp;
        }
        Invoke("DeActivate", _lifeTime);
    }

    void Update()
    {
        //if (_isAttacked)
        //{
        //    _coolTime += Time.deltaTime;
        //}
        //if (_coolTime >= _interval)
        //{
        //    _coolTime = 0;
        //    _isAttacked = false;
        //}
        if (_isPossibleAttack)
        {
            _attackCoolTime += Time.deltaTime;
            if (_attackCoolTime >= _attackInterval)
            {
                _attackCoolTime = 0;
                AttackPlayer();
            }
        }
        ChasePlayer();
    }

    protected override void ChasePlayer()
    {
        Vector3 direction = _target.position - transform.position;
        if (direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
        transform.Translate(direction.normalized * _speed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        // 플레이어 방향으로 에너지 발사
        Vector3 direction = (_target.position - transform.position).normalized;
        GameObject golemShot = Instantiate(GolemShot, transform.position, Quaternion.FromToRotation(Vector3.right, direction));
        golemShot.GetComponent<GolemShotController>().SetDirection(direction);
    }

    void DeActivate()
    {
        _animator.SetTrigger("Die");
    }

    // * animation effect
    void OnDieAnimationEnd()
    {
        ObjectManager.Instance.DeSpwan(this);
    }
}
