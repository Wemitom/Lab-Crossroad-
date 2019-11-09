using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    enum Colour
    {
        Colour_Red = 0,
        Colour_Green = 1,
        Colour_Yellow = 2
    }

    class TrafficLight
    {
        private Colour m_trafficLightColour;
        private int m_redInterval, m_greenInterval, m_yellowInterval, m_curLightTime;

        public TrafficLight(int redInterval, int greenInterval, Colour initialColour)
        {
            m_trafficLightColour = initialColour;
            m_redInterval = redInterval;
            m_greenInterval = greenInterval;
            m_yellowInterval = 5;
            switch(initialColour)
            {
                case Colour.Colour_Red:
                    m_curLightTime = m_redInterval;
                    break;
                case Colour.Colour_Green:
                    m_curLightTime = m_greenInterval;
                    break;
            }
        }

        public Colour SecondPassed(out int timeLeft)
        {
            switch(m_trafficLightColour)
            {
                case Colour.Colour_Red:
                    {
                        if (m_curLightTime != 1)
                            m_curLightTime--;
                        else
                        {
                            m_trafficLightColour++;
                            m_curLightTime = m_greenInterval;
                        }
                        break;
                    }
                case Colour.Colour_Green:
                    {
                        if (m_curLightTime != 1)
                            m_curLightTime--;
                        else
                        {
                            m_trafficLightColour++;
                            m_curLightTime = m_yellowInterval;
                        }
                        break;
                    }
                case Colour.Colour_Yellow:
                    {
                        if (m_curLightTime != 1)
                            m_curLightTime--;
                        else
                        {
                            m_trafficLightColour = 0;
                            m_curLightTime = m_redInterval;
                        }
                        break;
                    }
            }

            timeLeft = m_curLightTime;

            return m_trafficLightColour;
        }
    }
}
