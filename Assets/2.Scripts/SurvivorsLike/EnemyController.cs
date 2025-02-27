using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Transform _target;

    void Start()
    {
        _target = GameObject.FindWithTag(Define.PlayerTag).transform;
    }

    void Update()
    {
        Vector3 direction = _target.position - transform.position;
        transform.Translate(direction.normalized * 3 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Define.PlayerTag))
        {
            // �÷��̾� ü�� ���� �� Ÿ�� �ִϸ��̼� �߰�
            gameObject.SetActive(false);
        }
    }
}
