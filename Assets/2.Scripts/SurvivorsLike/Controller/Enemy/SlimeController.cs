using UnityEngine;

public class SlimeController : EnemyController
{    
    protected override void Initialize()
    {
        base.Initialize();
        _speed = 2f;
        _atk = 2f;
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
