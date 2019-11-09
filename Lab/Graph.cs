using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Lab
{
    class Graph
    {
        private Point m_minCoord, m_maxCoord;
        private int m_numDivisionsX, m_numDivisionsY, m_divisionSize, m_xAxisLength, m_yAxisLength, m_unitX, m_unitY;

        private Graphics m_graph;
        Pen m_blackPen;

        public void DrawAxis()
        {
            m_blackPen = new Pen(Color.Black);

            Point xAxisFirst = new Point(m_maxCoord.X, m_maxCoord.Y);
            Point xAxisSecond = new Point(m_minCoord.X, m_maxCoord.Y);
            m_graph.DrawLine(m_blackPen, xAxisFirst, xAxisSecond);

            Point yAxisFirst = new Point(m_minCoord.X, m_maxCoord.Y);
            Point yAxisSecond = new Point(m_minCoord.X, m_minCoord.Y);
            m_graph.DrawLine(m_blackPen, yAxisFirst, yAxisSecond);

            Point xAxis = new Point(m_minCoord.X, m_maxCoord.Y);
            for (var i = 0; i < m_numDivisionsX; i++)
            {
                xAxis.X += m_xAxisLength / m_numDivisionsX;

                m_graph.DrawLine(m_blackPen, xAxis.X, xAxis.Y, xAxis.X, xAxis.Y + m_divisionSize);
                m_graph.DrawLine(m_blackPen, xAxis.X, xAxis.Y, xAxis.X, xAxis.Y - m_divisionSize);
            }

            Point yAxis = new Point(m_minCoord.X, m_minCoord.Y);
            for (var i = 0; i < m_numDivisionsY; i++)
            {
                m_graph.DrawLine(m_blackPen, yAxis.X, yAxis.Y, yAxis.X + m_divisionSize, yAxis.Y);
                m_graph.DrawLine(m_blackPen, yAxis.X, yAxis.Y, yAxis.X - m_divisionSize, yAxis.Y);

                yAxis.Y += m_yAxisLength / m_numDivisionsY;
            }
        }

        private List<Point> m_graphsPoints = new List<Point>();
        private int m_pointsInArray = 0;


        public void GetPoint(int firstValue, int secondValue)
        {
            int firstCoord = ((firstValue * m_xAxisLength) / (m_numDivisionsX * m_unitX)) + m_minCoord.X;
            int secondCoord = m_maxCoord.Y - ((secondValue * m_yAxisLength) / (m_numDivisionsY * m_unitY));

            m_graphsPoints.Add(new Point(firstCoord, secondCoord));

            m_pointsInArray++;
        }


        public void PlotAGraph()
        {
            const int DISTANCE_FROM_DIVISION = 3;

            Point firstCoord;
            Point secondCoord;
            Point thirdCoord;
            Point fourthCoord;

            int i = 0;
            foreach (Point coord in m_graphsPoints)
            {
                int newFirstCoordX = (m_xAxisLength / m_numDivisionsX) * i + DISTANCE_FROM_DIVISION + m_minCoord.X;
                int newSecondCoordX = (m_xAxisLength / m_numDivisionsX) * (i + 1) - DISTANCE_FROM_DIVISION + m_minCoord.X;

                firstCoord = new Point(newFirstCoordX, m_maxCoord.Y);
                secondCoord = new Point(newFirstCoordX, coord.Y);
                thirdCoord = new Point(newSecondCoordX, coord.Y);
                fourthCoord = new Point(newSecondCoordX, m_maxCoord.Y);

                m_graph.DrawLine(m_blackPen, firstCoord, secondCoord);
                m_graph.DrawLine(m_blackPen, secondCoord, thirdCoord);
                m_graph.DrawLine(m_blackPen, thirdCoord, fourthCoord);

                i++;
            }
        }

        public void Clear()
        {
            m_graph.Clear(SystemColors.Control);
        }

        public Graph(Point minCoord, Point maxCoord, int numDivisionsX, int numDivisionsY, int divisionSize, int unitX, int unitY, Form activeForm)
        {
            m_minCoord = minCoord;
            m_maxCoord = maxCoord;
            m_numDivisionsX = numDivisionsX;
            m_numDivisionsY = numDivisionsY;
            m_divisionSize = divisionSize;
            m_xAxisLength = maxCoord.X - minCoord.X;
            m_yAxisLength = maxCoord.Y - minCoord.Y;
            m_unitX = unitX;
            m_unitY = unitY;
            m_graph = activeForm.CreateGraphics();
        }
    }
}
