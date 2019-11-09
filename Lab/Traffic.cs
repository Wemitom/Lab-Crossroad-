using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    class Traffic
    {
        public class Car
        {
            public int m_speed;
            public int m_lane;

            public bool m_isCarLeaving = false;
            public int m_secondsLeft = 0;
            public Car(int speed, int lane)
            {
                m_speed = speed;
                m_lane = lane;
            }
        }

        private Queue<Car> m_carsInFirstLane = new Queue<Car>();
        private Queue<Car> m_carsInSecondLane = new Queue<Car>();
        private Queue<Car> m_carsInThirdLane = new Queue<Car>();
        private Queue<Car> m_carsInFourthLane = new Queue<Car>();

        private readonly Random randNumber = new Random();
        private Car GetRandomCar()
        {
            return new Car(randNumber.Next(1, 7), randNumber.Next(1, 5));
        }

        public bool AddToTraffic()
        {
            const int MAX_CARS_IN_LANE = 40;

            Car car = GetRandomCar();

            switch (car.m_lane)
            {
                case 1:
                    {
                        if (GetNumCars(1) != MAX_CARS_IN_LANE)
                            m_carsInFirstLane.Enqueue(car);
                        else return false;
                        break;
                    }
                case 2:
                    {
                        if (GetNumCars(2) != MAX_CARS_IN_LANE)
                            m_carsInSecondLane.Enqueue(car);
                        else return false;
                        break;
                    }
                case 3:
                    {
                        if (GetNumCars(3) != MAX_CARS_IN_LANE)
                            m_carsInThirdLane.Enqueue(car);
                        else return false;
                        break;
                    }
                case 4:
                    {
                        if (GetNumCars(4) != MAX_CARS_IN_LANE)
                            m_carsInFourthLane.Enqueue(car);
                        else return false;
                        break;
                    }
            }
            return true;
        }

        List<Car> m_carsAtCrossroad = new List<Car>();

        public void LeaveTheCrossroad(Car car)
        {
        const int SLOW_SPEED = 7;
        const int NORMAL_SPEED = 5;
        const int FAST_SPEED = 3;

            if (!car.m_isCarLeaving)
            {
                switch (car.m_speed)
                {
                    case 1: car.m_secondsLeft = SLOW_SPEED; break;
                    case 2: car.m_secondsLeft = NORMAL_SPEED; break;
                    case 3: car.m_secondsLeft = NORMAL_SPEED; break;
                    case 4: car.m_secondsLeft = NORMAL_SPEED; break;
                    case 5: car.m_secondsLeft = FAST_SPEED; break;
                    case 6: car.m_secondsLeft = FAST_SPEED; break;
                }
                car.m_isCarLeaving = true;
            }
            else
                for (var curCarIndex = 0; curCarIndex < m_carsAtCrossroad.Count; curCarIndex++ )
                {
                    if (m_carsAtCrossroad[curCarIndex].m_secondsLeft != 0)
                       m_carsAtCrossroad[curCarIndex].m_secondsLeft--;
                    else
                    {
                        m_carsAtCrossroad[curCarIndex].m_isCarLeaving = false;
                        m_carsAtCrossroad.RemoveAt(curCarIndex);
                    }
                }
        }

        public void EnterTheCrossroad(Car car)
        {
            const int MAX_CARS_AT_CROSSROAD = 6;

            if (m_carsAtCrossroad.Count != MAX_CARS_AT_CROSSROAD)
            {
                m_carsAtCrossroad.Add(car);

                switch (car.m_lane)
                {
                    case 1: m_carsInFirstLane.Dequeue(); break;
                    case 2: m_carsInSecondLane.Dequeue(); break;
                    case 3: m_carsInThirdLane.Dequeue(); break;
                    case 4: m_carsInFourthLane.Dequeue(); break;
                }
            }
        }

        public void LeaveTheTraffic(Colour colour, int timeLeft, int lane)
        {
            Car car = null;
            switch (lane)
            {
                case 1: car = m_carsInFirstLane.Peek(); break;
                case 2: car = m_carsInSecondLane.Peek(); break;
                case 3: car = m_carsInThirdLane.Peek(); break;
                case 4: car = m_carsInFourthLane.Peek(); break;
            }

            if ((colour == Colour.Colour_Green || (colour == Colour.Colour_Yellow && car.m_speed != 1)) || car.m_isCarLeaving)
            {
                EnterTheCrossroad(car);
                LeaveTheCrossroad(car);
            }
        }

        public int GetNumCars(int lane)
        {
            int result;

            switch (lane)
            {
                case 1: result = m_carsInFirstLane.Count; break;
                case 2: result = m_carsInSecondLane.Count; break;
                case 3: result = m_carsInThirdLane.Count; break;
                case 4: result = m_carsInFourthLane.Count; break;
                default: result = m_carsInFirstLane.Count + m_carsInSecondLane.Count + m_carsInThirdLane.Count + m_carsInFourthLane.Count; break;
            }

            return result;
        }

        public void Test(int numCars)
        {
            for (int i = 0; i < numCars; i++) AddToTraffic();
        }
    }
}
