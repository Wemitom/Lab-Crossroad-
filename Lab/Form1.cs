using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//To-do list
//1. Add one more traffic light so that one regualtes first and third line and another one regulates second and fourth (DONE)
//2. Tweak the congestion levels in the table
//3. Add the current congestion level
//4. Add overall congestion statistics
//5. Add visualization (?)
//6. Maybe more if I can come up with it (?) 




namespace Lab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string TimeToString(int hours, int minutes)
        {
            string timeToString = null;

            if (hours == 0 || hours == 1 || hours == 2 || hours == 3 || hours == 4 || hours == 5
                || hours == 6 || hours == 7 || hours == 8 || hours == 9) timeToString += "0" + hours;
            else timeToString += hours;

            timeToString += ":";

            if (minutes == 0 || minutes == 1 || minutes == 2 || minutes == 3 || minutes == 4 || minutes == 5 
                || minutes == 6 || minutes == 7 || minutes == 8 || minutes == 9) timeToString += "0" + minutes;
            else timeToString += minutes;

            return timeToString;
        }

        public void DisplayCurDay(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Sunday: label2.Text = "Воскресенье"; break;
                case DayOfWeek.Monday: label2.Text = "Понедельник"; break;
                case DayOfWeek.Tuesday: label2.Text = "Вторник"; break;
                case DayOfWeek.Wednesday: label2.Text = "Среда"; break;
                case DayOfWeek.Thursday: label2.Text = "Четверг"; break;
                case DayOfWeek.Friday: label2.Text = "Пятница"; break;
                case DayOfWeek.Saturday: label2.Text = "Суббота"; break;
            }
        }

        private void DisplayTrafficLight(Colour curColour, int timeLeft, Label labelColour, Label labelTime)
        {
            switch (curColour)
            {
                case Colour.Colour_Red: labelColour.Text = "Красный"; break;
                case Colour.Colour_Green: labelColour.Text = "Зеленый"; break;
                case Colour.Colour_Yellow: labelColour.Text = "Желтый"; break;
            }

            string stringTimeLeft = null;
            stringTimeLeft += timeLeft;
            labelTime.Text = stringTimeLeft;
        }

        private void ShowCurWeekStat(int curDay)
        {
            int dayAverage;

            if (curDay != 0)
                dayAverage = g_averageValue.CurDayStatistics(curDay - 1);
            else
                dayAverage = g_averageValue.CurDayStatistics(curDay);

            switch (curDay)
            {
                case 0:
                    progressBar8.Value = dayAverage;
                    break;
                case 1:
                    progressBar2.Value = dayAverage;
                    break;
                case 2:
                    progressBar3.Value = dayAverage;
                    break;
                case 3:
                    progressBar4.Value = dayAverage;;
                    break;
                case 4:
                    progressBar5.Value = dayAverage;
                    break;
                case 5:
                    progressBar6.Value = dayAverage;
                    break;
                case 6:
                    progressBar7.Value = dayAverage;
                    break;
            }
        }

        //const int MAX_COORD_X = 1220;
        //const int MIN_COORD_X = 800;
        //const int MAX_COORD_Y = 420;
        //const int MIN_COORD_Y = 20;
        //const int X_AXIS_LENGTH = MAX_COORD_X - MIN_COORD_X;
        //const int Y_AXIS_LENGTH = MAX_COORD_Y - MIN_COORD_Y;
        //const int NUM_DIVISIONS_X = 24;
        //const int NUM_DIVISIONS_Y = 16;
        //const int DIVISION_SIZE = 5;

        //void DrawAxis()
        //{
        //    Graphics graph = Form1.ActiveForm.CreateGraphics();

        //    Pen blackPen;
        //    blackPen = new Pen(Color.Black);

        //    Point xAxisFirst = new Point(MAX_COORD_X, MAX_COORD_Y);
        //    Point xAxisSecond = new Point(MIN_COORD_X, MAX_COORD_Y);
        //    graph.DrawLine(blackPen, xAxisFirst, xAxisSecond);

        //    Point yAxisFirst = new Point(MIN_COORD_X, MAX_COORD_Y);
        //    Point yAxisSecond = new Point(MIN_COORD_X, MIN_COORD_Y);
        //    graph.DrawLine(blackPen, yAxisFirst, yAxisSecond);

        //    Point xAxis = new Point(MIN_COORD_X, MAX_COORD_Y);
        //    for (var i = 0; i < NUM_DIVISIONS_X; i++)
        //    {
        //        xAxis.X += X_AXIS_LENGTH / NUM_DIVISIONS_X;

        //        graph.DrawLine(blackPen, xAxis.X, xAxis.Y, xAxis.X, xAxis.Y + DIVISION_SIZE);
        //        graph.DrawLine(blackPen, xAxis.X, xAxis.Y, xAxis.X, xAxis.Y - DIVISION_SIZE);
        //    }

        //    Point yAxis = new Point(MIN_COORD_X, MIN_COORD_Y);
        //    for (var i = 0; i < NUM_DIVISIONS_Y; i++)
        //    {
        //        graph.DrawLine(blackPen, yAxis.X, yAxis.Y, yAxis.X + DIVISION_SIZE, yAxis.Y);
        //        graph.DrawLine(blackPen, yAxis.X, yAxis.Y, yAxis.X - DIVISION_SIZE, yAxis.Y);

        //        yAxis.Y += Y_AXIS_LENGTH / NUM_DIVISIONS_Y;
        //    }

        //    graph.Dispose();
        //}

        //private void PlotAGraph(int day)
        //{
        //    int width = 463, hight = 395;
        //}

        const int SECOND = 1000;

        const int DAYS_IN_WEEK = 7;

        static int secPerTick = 1;

        readonly Time g_timer = new Time();

        readonly TrafficLight g_firstTrafficLight = new TrafficLight(30, 60, Colour.Colour_Red);
        readonly TrafficLight g_secondTrafficLight = new TrafficLight(60, 30, Colour.Colour_Green);

        readonly Traffic g_traffic = new Traffic();

        readonly CongestionTable g_table = new CongestionTable();

        List<Graph> g_graphs = new List<Graph>();

        MeanValue g_averageValue = new MeanValue();

        readonly Random randNumber = new Random();

        DayOfWeek curDay = DayOfWeek.Monday;

        private void Timer1_Tick(object sender, EventArgs e)
        {
            for (var i = 0; i < secPerTick; i++)
            {
                bool didDayChange = g_timer.SecondPassed(out bool didHourChange);

                Colour firstCurColour = g_firstTrafficLight.SecondPassed(out int firstTimeLeft);
                Colour secondCurColour = g_secondTrafficLight.SecondPassed(out int secondTimeLeft);
                DisplayTrafficLight(firstCurColour, firstTimeLeft, label3, label4);
                DisplayTrafficLight(secondCurColour, secondTimeLeft, label6, label7);

                int carsInTraffic = g_traffic.GetNumCars(0);

                g_timer.GetTime(out int hours, out int minutes);

                string timeToString = TimeToString(hours, minutes);

                label1.Text = timeToString;

                int congestionLevel = g_table.table[(int)curDay, hours];

                if (randNumber.Next(1, congestionLevel) == 1)
                {
                    for (var j = 0; j < randNumber.Next(1, 5) + 1; j++)
                        g_traffic.AddToTraffic();
                }

                if (carsInTraffic != 0)
                {
                    if (g_traffic.GetNumCars(1) != 0)
                        g_traffic.LeaveTheTraffic(firstCurColour, firstTimeLeft, 1);
                    if (g_traffic.GetNumCars(2) != 0)
                        g_traffic.LeaveTheTraffic(secondCurColour, secondTimeLeft, 2);
                    if (g_traffic.GetNumCars(3) != 0)
                        g_traffic.LeaveTheTraffic(firstCurColour, firstTimeLeft, 3);
                    if (g_traffic.GetNumCars(4) != 0)
                        g_traffic.LeaveTheTraffic(secondCurColour, secondTimeLeft, 4);
                }

                int curMinAverage = g_averageValue.GetCurMinuteAverage();
                g_averageValue.SecondPassed(carsInTraffic);
                progressBar1.Value = curMinAverage;
                label9.Text = curMinAverage.ToString();

                string carsInTrafficString = null;
                carsInTrafficString += carsInTraffic;
                label5.Text = carsInTrafficString;

                if (didDayChange) 
                {
                    ShowCurWeekStat((int)curDay);

                    if (curDay != DayOfWeek.Saturday) curDay++;
                    else
                        curDay = 0;
                    DisplayCurDay(curDay);
                }

                if (didHourChange)
                    if (hours != 0)
                        g_graphs[(int)curDay].GetPoint(hours - 1, g_averageValue.GetHourAverage(hours - 1));
                    else
                        g_graphs[(int)curDay].GetPoint(hours, g_averageValue.GetHourAverage(hours));
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            secPerTick = 1;
            timer1.Interval = SECOND;
            //g_traffic.Test(100);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            secPerTick = 60;
            timer1.Interval = SECOND;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            secPerTick = 60;
            timer1.Interval = SECOND / 2;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            secPerTick = 60;
            timer1.Interval = SECOND / SECOND;
        }

        const int MAX_COORD_X = 1220;
        const int MIN_COORD_X = 800;
        const int MAX_COORD_Y = 420;
        const int MIN_COORD_Y = 20;
        const int NUM_DIVISIONS_X = 24;
        const int NUM_DIVISIONS_Y = 16;
        const int DIVISION_SIZE = 5;
        const int UNIT_X = 1;
        const int UNIT_Y = 10;

        private void Form1_Load(object sender, EventArgs e)
        {
            for (var i = 0; i < DAYS_IN_WEEK; i++)
            {
                g_graphs.Add(new Graph(new Point(MIN_COORD_X, MIN_COORD_Y), new Point(MAX_COORD_X, MAX_COORD_Y), NUM_DIVISIONS_X,
                NUM_DIVISIONS_Y, DIVISION_SIZE, UNIT_X, UNIT_Y, this));
            }
        }

        private void Label10_Click(object sender, EventArgs e)
        {
            Width = 1262;

            g_graphs[(int)curDay].Clear();
            g_graphs[(int)curDay].DrawAxis();
            g_graphs[(int)curDay].PlotAGraph();
        }

        private void Label11_Click(object sender, EventArgs e)
        {
            Width = 797;
            g_graphs[(int)curDay].Clear();
        }
    }
}
