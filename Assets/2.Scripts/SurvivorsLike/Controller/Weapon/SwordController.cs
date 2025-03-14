using UnityEngine;

public class SwordController : BaseController
{
    Transform _player;
    float _angle = 0f;

    public float Angle
    {
        get { return _angle; }
        set { _angle = value; }
    }

    const float _radius = 2f;

    protected override void Initialize()
    {
        _player = ObjectManager.Instance.Player.transform;
    }

    private void OnEnable()
    {
        _angle = 0f;
    }

    void Update()
    {
        _angle += Time.deltaTime * GameManager.Instance.WeaponInfo.SwordRotationSpeed;
        float offsetX = _radius * Mathf.Cos(_angle * Mathf.Deg2Rad);
        float offsetY = _radius * Mathf.Sin(_angle * Mathf.Deg2Rad);
        Vector3 newPos = new Vector3(offsetX, offsetY) + _player.position;
        transform.position = newPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.EnemyTag))
        {
            //collision.gameObject.SetActive(false);
            collision.GetComponent<EnemyController>().GetDamage(GameManager.Instance.PlayerInfo.Atk, ObjectManager.Instance.Player.gameObject);
        }
    }
}
