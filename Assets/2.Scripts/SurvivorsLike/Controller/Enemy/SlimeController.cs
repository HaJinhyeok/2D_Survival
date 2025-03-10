using UnityEngine;

public class SlimeController : EnemyController
{    
    protected override void Initialize()
    {
        base.Initialize();
        _speed = 2f;
        _atk = 2f;
    }

    private void OnEnable()
    {
        _hp = 7;
    }

    void Update()
    {
        ChasePlayer();
    }

    protected override void ChasePlayer()
    {
        Vector3 direction = _target.position - transform.position;
        transform.Translate(direction.normalized * _speed * Time.deltaTime);
    }
}
