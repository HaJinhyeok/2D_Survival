using UnityEngine;

public class DamageTextController : MonoBehaviour
{
    void Update()
    {
        transform.position += Vector3.up * 2 * Time.deltaTime;
    }

    public void DestroyText()
    {
        Destroy(gameObject);
    }
}
