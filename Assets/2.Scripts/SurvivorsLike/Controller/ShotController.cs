using System.Collections;
using UnityEngine;

public class ShotController : BaseController
{
    Vector2 _moveDir;

    protected override void Initialize()
    {
        // _moveDir = (GameManager.Instance.Target.transform.position - transform.position).normalized;
        // StartCoroutine(CoDeactivate());
    }

    private void OnEnable()
    {
        StartCoroutine(CoDeactivate());
    }

    // update는 추후 수정

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Define.EnemyTag))
        {
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            GameManager.Instance.GetScore();
        }
    }

    IEnumerator CoDeactivate()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
