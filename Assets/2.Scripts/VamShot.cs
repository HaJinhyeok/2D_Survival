using UnityEngine;

public class VamShot : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
