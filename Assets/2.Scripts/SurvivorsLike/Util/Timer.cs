using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public static float s_TimeInfo;
    public static UnityAction s_TimerAction;
    public static int s_MinuteCount;
    public static int s_SecondCount;

    float _startTime;

    void Start()
    {
        _startTime = Time.time;
        s_MinuteCount = 0;
    }

    void Update()
    {
        s_TimeInfo = Time.time - _startTime;
        s_SecondCount = (int)s_TimeInfo;
        s_MinuteCount = s_SecondCount / 60;
        s_SecondCount %= 60;
        s_TimerAction?.Invoke();
    }
}
