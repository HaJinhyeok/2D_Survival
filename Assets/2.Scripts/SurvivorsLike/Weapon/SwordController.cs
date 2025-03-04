using UnityEngine;

public class SwordController : BaseController
{
    Transform _player;
    float _angle = 0f;

    // default: 1round/1sec
    public static float RotationSpeed = 360;
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
        _angle += Time.deltaTime * RotationSpeed;
        float offsetX = _radius * Mathf.Cos(_angle * Mathf.Deg2Rad);
        float offsetY = _radius * Mathf.Sin(_angle * Mathf.Deg2Rad);
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
