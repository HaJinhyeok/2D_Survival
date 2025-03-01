using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    // * Explosion Animation Event
    void EndExplosion()
    {
        Destroy(GameObject.FindGameObjectWithTag(Define.ExplosionTag));
    }
}
