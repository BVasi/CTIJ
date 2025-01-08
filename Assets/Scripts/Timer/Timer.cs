using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;

    void Start()
    {
        isRunning_ = false;
        timeRemaining_ = EXPIRED_TIMER_VALUE;
    }

    void Update()
    {
        if (!isRunning_)
        {
            return;
        }
        timeRemaining_ -= Time.deltaTime;
        if (timeRemaining_ <= EXPIRED_TIMER_VALUE)
        {
            StopTimer();
            OnTimerEnd?.Invoke();
        }
    }

    public void StartTimerForSeconds(float seconds)
    {
        timeRemaining_ = seconds;
        isRunning_ = true;
    }

    public void StopTimer()
    {
        isRunning_ = false;
        timeRemaining_ = EXPIRED_TIMER_VALUE;
    }

    public bool HasExpired()
    {
        return timeRemaining_ <= EXPIRED_TIMER_VALUE;
    }

    public int GetTimeRemainingInSeconds()
    {
        return (int)timeRemaining_;
    }

    public event System.Action OnTimerEnd;
    private bool isRunning_;
    private float timeRemaining_;
    private const float EXPIRED_TIMER_VALUE = 0f;
}
