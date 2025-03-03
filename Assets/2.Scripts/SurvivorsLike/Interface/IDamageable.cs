using UnityEngine;

public interface IDamageable
{
    string Tag { get; set; }
    bool GetDamage(float damage, GameObject damageCauser, Vector2 hitPoint = default);
}
