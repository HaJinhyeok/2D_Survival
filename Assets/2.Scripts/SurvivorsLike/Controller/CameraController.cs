using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform _target;
    Vector3 _offset = new Vector3(0, 0, -10);
    Vector3 _movePos;
    float _speed = 3f;

    // ���� Singleton ���� �����ϰ� BaseController ��ӹ޾Ƽ� Initialize�� �ٲ���
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
