using UnityEngine;

public class TimerTestUI : MonoBehaviour
{
    public Timer timer; // Referință la componenta Timer

    void Start()
    {
        if (timer != null)
        {
            timer.OnTimerEnd += TimerEnded;
            timer.StartTimerForSeconds(120); // Pornim un timer de 2 minute
        }
    }

    private void TimerEnded()
    {
        Debug.Log("Timerul UI s-a terminat!");
    }
}
