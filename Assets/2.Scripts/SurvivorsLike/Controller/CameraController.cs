using UnityEngine;

public class CameraController : BaseController
{
    Transform _target;
    Vector3 _offset = new Vector3(0, 0, -10);
    Vector3 _movePos;
    float _speed = 3f;

    protected override void Initialize()
    {
        // _target = GameObject.FindGameObjectWithTag(Define.PlayerTag).transform;
        _target = ObjectManager.Instance.Player.transform;
    }

    private void FixedUpdate()
    {
        _movePos = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, _movePos, _speed * Time.deltaTime);
    }
}
