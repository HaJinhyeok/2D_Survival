using UnityEngine;

public class LetterController : BaseController
{
    protected override void Initialize()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            Destroy(gameObject);
            // Player���� �нú� ȿ�� �ο�
        }
    }
}
