using System.Collections;
using UnityEngine;

public interface IDamageable
{
    bool GetDamage(float damage, GameObject damageCauser, Vector2 hitPoint = default);
    IEnumerator CoFlashWhite();
}
