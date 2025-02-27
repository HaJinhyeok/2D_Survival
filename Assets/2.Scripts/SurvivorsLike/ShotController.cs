using System.Collections;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    private void Start()
    {
        // Destroy(gameObject, 5f);
        StartCoroutine(CoDeactivate());
    }

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
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
