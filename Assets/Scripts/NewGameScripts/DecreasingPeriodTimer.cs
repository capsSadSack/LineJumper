using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NewGameScripts
{
    public class DecreasingPeriodTimer
    {
        public float remainsTime_Sec;

        private float period_Sec;
        private float minimumPeriod_Sec;
        private float dPeriod_Sec;
        private Action timerEndAction;

        public DecreasingPeriodTimer(float initialPeriod_Sec, float minimumPeriod_Sec,
            float dPeriod_Sec, Action timerEndAction)
        {
            this.period_Sec = initialPeriod_Sec;
            this.remainsTime_Sec = period_Sec;
            this.minimumPeriod_Sec = minimumPeriod_Sec;
            this.dPeriod_Sec = dPeriod_Sec;
            this.timerEndAction = timerEndAction;
        }

        public void UpdateTimer()
        {
            remainsTime_Sec -= Time.deltaTime;

            if (remainsTime_Sec <= 0.0f)
            {
                timerEndAction();
                DecreasePeriod();

                remainsTime_Sec = period_Sec;
            }
        }

        private void DecreasePeriod()
        {
            period_Sec -= dPeriod_Sec;
            if (period_Sec <= minimumPeriod_Sec)
            {
                period_Sec = minimumPeriod_Sec;
            }
        }
    }
}
