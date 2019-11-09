using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    class MeanValue
    {
        private const int MAX_SECONDS = 60;
        private int[] m_curValue = new int[MAX_SECONDS];
        private int m_curSecond = 0;

        private const int MAX_MINUTES = 60;
        private int[] m_minuteAverage = new int[MAX_MINUTES];
        private int m_curMinute = 0;

        private const int MAX_HOURS = 24;
        private int[] m_hourAverage = new int[MAX_HOURS];
        private int m_curHour = 0;

        private const int MAX_DAYS = 7;
        private int[] m_dayAverage = new int[MAX_DAYS] {0, 0, 0, 0, 0, 0, 0 };
        private int m_curDay = 0;

        public void SecondPassed(int value)
        {
            if (m_curSecond != MAX_SECONDS - 1)
            {
                m_curValue[m_curSecond] = value;
                m_curSecond++;
            }
            else
            {
                MinutePassed();

                Array.Clear(m_curValue, 0, MAX_SECONDS);
                m_curSecond = 0;
            }
        }

        private void MinutePassed()
        {
            if (m_curMinute != MAX_MINUTES - 1)
            {
                m_minuteAverage[m_curMinute] = Convert.ToInt32(Math.Round(m_curValue.Average()));
                m_curMinute++;
            }
            else
            {
                HourPassed();

                Array.Clear(m_minuteAverage, 0, MAX_MINUTES);
                m_curMinute = 0;
            }
        }

        private void HourPassed()
        {
            if (m_curHour != MAX_HOURS - 1)
            {
                m_hourAverage[m_curHour] = Convert.ToInt32(Math.Round(m_minuteAverage.Average()));
                m_curHour++;
            }
            else
            {
                DayPassed();

                Array.Clear(m_hourAverage, 0, MAX_HOURS);
                m_curHour = 0;
            }
        }

        private void DayPassed()
        {
            if (m_curDay != MAX_DAYS - 1)
            {
                m_dayAverage[m_curDay] = Convert.ToInt32(Math.Round(m_hourAverage.Average()));
                m_curDay++;
            }
            else
            {
                Array.Clear(m_hourAverage, 0, MAX_DAYS);
                m_curDay = 0;
            }
        }

        public int GetCurMinuteAverage()
        {
            if (m_curMinute != 0)
                return m_minuteAverage[m_curMinute - 1];
            else
                return Convert.ToInt32(Math.Round(m_curValue.Average()));
        }

        public int GetHourAverage(int hour)
        {
            return m_hourAverage[hour];
        }

        public int CurDayStatistics(int curDay)
        {
            return m_dayAverage[curDay];
        }

    }
}
