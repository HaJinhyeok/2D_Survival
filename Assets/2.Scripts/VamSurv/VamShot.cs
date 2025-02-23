using System.Collections;
using UnityEngine;

public class VamShot : MonoBehaviour
{
    private void Start()
    {
        // Destroy(gameObject, 5f);
        StartCoroutine(CoDeactivate());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    IEnumerator CoDeactivate()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
