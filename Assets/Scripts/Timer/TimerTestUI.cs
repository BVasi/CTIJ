using UnityEngine;

public class TimerTestUI : MonoBehaviour
{
    public Timer timer;

    void Start()
    {
        if (timer != null)
        {
            timer.OnTimerEnd += TimerEnded;
            timer.StartTimerForSeconds(120);
        }
    }

    private void TimerEnded()
    {
        Debug.Log("Timer End!");
    }
}
