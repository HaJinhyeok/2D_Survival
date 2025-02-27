using UnityEngine;

public class SwordController : MonoBehaviour
{
    Transform _player;
    float _angle = 0f;

    const float _radius = 2f;


    void Start()
    {
        _player = GameObject.FindWithTag(Define.PlayerTag).transform;
    }

    void Update()
    {
        _angle += Time.deltaTime * 360 * Mathf.Deg2Rad;
        float offsetX = _radius * Mathf.Cos(_angle);
        float offsetY = _radius * Mathf.Sin(_angle);
        Vector3 newPos = new Vector3(offsetX, offsetY) + _player.position;
        transform.position = newPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.EnemyTag))
        {
            collision.gameObject.SetActive(false);
            GameManager.Instance.GetScore();
        }
    }
}
