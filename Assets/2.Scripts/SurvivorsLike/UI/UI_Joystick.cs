using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public GameObject JoyStick;
    public GameObject Handler;

    Vector2 _currentPos;
    Vector2 _startPos;
    Vector2 _initPos;
    Vector2 _direction;
    float _radius;

    void Start()
    {
        _radius = JoyStick.GetComponent<RectTransform>().sizeDelta.x / 3;
        _initPos = JoyStick.transform.position;
        SetActiveJoyStick(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetActiveJoyStick(true);
        _startPos = Input.mousePosition;
        JoyStick.transform.position = Input.mousePosition;
        Handler.transform.position = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _currentPos = eventData.position;
        _direction = (_currentPos - _startPos).normalized;
        float distance = (_currentPos - _startPos).sqrMagnitude;

        Vector3 newPos;
        if (distance < _radius)
        {
            newPos = _startPos + (_direction * distance);
        }
        else
        {
            newPos = _startPos + (_direction * _radius);
        }
        Handler.transform.position = newPos;
        GameManager.Instance.MoveDir = _direction;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _direction = Vector2.zero;
        JoyStick.transform.position = _initPos;
        Handler.transform.position = _initPos;
        // player와 연동할 무언가 필요
        GameManager.Instance.MoveDir = _direction;
        SetActiveJoyStick(false);
    }

    void SetActiveJoyStick(bool isActive)
    {
        JoyStick.SetActive(isActive);
        Handler.SetActive(isActive);
    }
}
