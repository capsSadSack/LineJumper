using UnityEngine;
using System;

public class SimpleTimer
{
    private float remainsTime_Sec;
    private float period_Sec;
    private Action timerEndAction;

    public SimpleTimer(float period_Sec, Action timerEndAction)
    {
        this.period_Sec = period_Sec;
        remainsTime_Sec = period_Sec;
        this.timerEndAction = timerEndAction;
    }

    public void UpdateTimer()
    {
        remainsTime_Sec -= Time.deltaTime;

        if (remainsTime_Sec <= 0.0f)
        {
            timerEndAction();
            remainsTime_Sec = period_Sec;
        }
    }
}
