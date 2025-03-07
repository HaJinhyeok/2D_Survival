using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public static float s_TimeInfo;
    public static UnityAction s_TimerAction;

    float _startTime;

    void Start()
    {
        _startTime = Time.time;
    }

    void Update()
    {
        s_TimeInfo = Time.time - _startTime;
        s_TimerAction?.Invoke();
    }
}
