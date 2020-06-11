using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ResetableTimer
{
    private bool isStarted = false;

    private float remainsTime_Sec;
    private float period_Sec;
    private Action timerEndAction;

    public ResetableTimer(float period_Sec, Action timerEndAction)
    {
        this.period_Sec = period_Sec;
        remainsTime_Sec = period_Sec;
        this.timerEndAction = timerEndAction;
    }

    public void UpdateTimer()
    {
        if (isStarted)
        {
            remainsTime_Sec -= Time.deltaTime;

            if (remainsTime_Sec <= 0.0f)
            {
                timerEndAction();
                remainsTime_Sec = period_Sec;
            }
        }
    }

    public void Stop()
    {
        remainsTime_Sec = period_Sec;
        isStarted = false;
    }

    public void Start()
    {
        remainsTime_Sec = period_Sec;
        isStarted = true;
    }
}

