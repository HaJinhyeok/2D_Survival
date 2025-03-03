using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DogController : EnemyController
{
    protected override void Initialize()
    {
        base.Initialize();
    }

    private void Update()
    {
        ChasePlayer();
    }

    protected override void ChasePlayer()
    {
        Vector3 direction = _target.position - transform.position;
        transform.Translate(direction.normalized * _speed * Time.deltaTime);
    }
}
