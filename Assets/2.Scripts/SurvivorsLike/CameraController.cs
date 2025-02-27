using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform _target;
    Vector3 _offset = new Vector3(0, 0, -10);
    Vector3 _movePos;
    float _speed = 3f;

    // 이후 Singleton 패턴 공부하고 BaseController 상속받아서 Initialize로 바꾸자
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        _movePos = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, _movePos, _speed * Time.deltaTime);
    }
}
