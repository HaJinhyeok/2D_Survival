using System.Collections.Generic;
using UnityEngine;

public class AuraController : BaseController
{
    float _coolTime = 0f;
    float _interval = 0.5f;
    Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        transform.position = ObjectManager.Instance.Player.transform.position;
        float offset = _transform.rotation.eulerAngles.z;
        offset += Time.deltaTime * 100;
        _transform.rotation = Quaternion.Euler(0, 0, offset);
        _coolTime += Time.deltaTime;
        if (_coolTime >= _interval)
        {
            _coolTime = 0f;
            AuraAttack(ObjectManager.Instance.Player.transform.position);
        }
    }

    void AuraAttack(Vector3 center)
    {
        HashSet<EnemyController> enemyControllers = new HashSet<EnemyController>(ObjectManager.Instance.Enemies);
        foreach (var enemy in enemyControllers)
        {
            if (Vector3.Distance(center, enemy.transform.position) <= GameManager.Instance.WeaponInfo.AuraRadius
                && enemy.gameObject.activeSelf)
            {
                enemy.GetDamage(GameManager.Instance.WeaponInfo.AuraAtk, ObjectManager.Instance.Player.gameObject);
            }
        }
    }

    protected override void Initialize()
    {

    }
}
