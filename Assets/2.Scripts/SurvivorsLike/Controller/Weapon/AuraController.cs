using UnityEngine;

public class AuraController : BaseController
{
    float _coolTime = 0f;
    float _interval = 0.5f;

    void Update()
    {
        transform.position = ObjectManager.Instance.Player.transform.position;
        _coolTime += Time.deltaTime;
        if (_coolTime >= _interval)
        {
            _coolTime = 0f;
            AuraAttack(ObjectManager.Instance.Player.transform.position);
        }
    }

    void AuraAttack(Vector3 center)
    {
        foreach (var enemy in ObjectManager.Instance.Enemies)
        {
            if (Vector3.Distance(center, enemy.transform.position) <= GameManager.Instance.WeaponInfo.AuraRadius)
            {
                enemy.GetDamage(GameManager.Instance.WeaponInfo.AuraAtk, ObjectManager.Instance.Player.gameObject);
            }
        }
    }

    protected override void Initialize()
    {

    }
}
