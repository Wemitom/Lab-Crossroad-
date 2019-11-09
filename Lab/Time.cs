using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    class Time
    {
        private int m_seconds, m_minutes, m_hours;

        public Time()
        {
            m_seconds = m_minutes = m_hours = 0;
        }

        public bool SecondPassed(out bool didHourChange)
        {
            bool didDayChange = false;
            didHourChange = false;

            if (m_seconds == 59)
                didDayChange = MinutePassed(out didHourChange);
            else
                m_seconds++;

            return didDayChange;
        }

        private bool MinutePassed(out bool didHourChange)
        {
            bool didDayChange = false;
            didHourChange = false;

            m_seconds = 0;

            if (m_minutes == 59)
            {
                didHourChange = true;
                didDayChange = HourPassed();
            }

            else
            {
                m_minutes++;
            }

            return didDayChange;
        }

        private bool HourPassed()
        {
            bool didDayChange = false;

            m_minutes = 0;

            if (m_hours == 23)
            {
                didDayChange = true;
                m_seconds = m_hours = 0;
            }
            else
            {
                m_hours++;
            }

            return didDayChange;
        }

        public void GetTime(out int hours, out int minutes)
        {
            hours = m_hours;
            minutes = m_minutes;
        }
    }
}
