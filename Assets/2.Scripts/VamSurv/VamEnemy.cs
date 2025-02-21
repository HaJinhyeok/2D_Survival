using UnityEngine;

public class VamEnemy : MonoBehaviour
{
    Transform _target;

    void Start()
    {
        _target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 direction = _target.position - transform.position;
        transform.Translate(direction.normalized * 3 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
