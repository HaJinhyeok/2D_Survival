using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public static float s_TimeInfo;
    public static UnityAction s_TimerAction;
    public static int s_MinuteCount;

    float _startTime;

    void Start()
    {
        _startTime = Time.time;
    }

    void Update()
    {
        s_TimeInfo = Time.time - _startTime;
        //if (s_TimeInfo >= 60f)
        //{
        //    s_MinuteCount++;
        //    s_TimeInfo -= 60f;
        //}
        s_TimerAction?.Invoke();
    }
}
