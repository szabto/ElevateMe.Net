using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSaga.Core.Classes
{

    public abstract class User
    {
        /// <summary>
        /// User's weight. Important for elevator capacity calculation.
        /// </summary>
        public abstract int Weigth { get; }

        /// <summary>
        /// Determines on which floor is currently waiting the user.
        /// </summary>
        public Floor CurrentFloor { get; private set; }

        /// <summary>
        /// Determines where the user want to go
        /// </summary>
        public int DestinationFloor { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDone { get; private set; }

        /// <summary>
        /// Stores the user's current elevator. If user on floor, this will be null.
        /// </summary>
        public Elevator CurrentElevator { get; private set; }

        private readonly object tickLock = new object();

        /// <summary>
        /// Returns the user's mood
        /// </summary>
        public Mood Mood = Mood.Happy;

        private int _serviceLevel = 100;
        /// <summary>
        /// Contain's the service level. It decreases every 2 seconds of wait.
        /// </summary>
        public int ServiceLevel
        {
            get { return _serviceLevel; }
            set
            {
                _serviceLevel = value;
                Mood = MoodHelper.GetMoodBySla(value);
            }
        }

        private int _waitingTime = 0;

        /// <summary>
        /// Returns the waiting time of user on floor.
        /// </summary>
        public int WaitingTime
        {
            get { return _waitingTime; }
            set
            {
                _waitingTime = value;
                if (ServiceLevel > 0 && WaitingTime % 2 == 0)
                    ServiceLevel--;
            }
        }

        private static Random rnd = new Random();

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="currentFloor"></param>
        /// <returns></returns>
        public static User GetRandom(Floor currentFloor)
        {
            int rndVal = rnd.Next(4);
            User u = null;
            if (rndVal == 0) u = new Child(currentFloor);
            else if (rndVal == 1) u = new Man(currentFloor);
            else if (rndVal == 2) u = new Women(currentFloor);
            else if (rndVal == 3) u = new WheelChaired(currentFloor);

            return u;
        }
        

        /// <summary>
        /// Creates a user with waiting floor, and a destination floor
        /// </summary>
        /// <param name="currentFloor">The current floor, user is waiting on</param>
        /// <param name="destinationFloor">The destination floor, where user wants to go</param>
        public User(Floor currentFloor)
        {
            CurrentFloor = currentFloor;
            PickRandomDestination();
        }

        private void PickRandomDestination()
        {
            int floors = 4; //TODO get floor count
            int dstFloor = -1;
            do
            {
                dstFloor = rnd.Next(floors);
            }
            while (dstFloor == CurrentFloor.Level);

            DestinationFloor = dstFloor;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(int gameTime)
        {
            if (gameTime % World.FPS == 0)
            {
                lock (tickLock)
                {
                    if (CurrentElevator == null && !IsDone)
                        WaitingTime++;
                }
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public void PressButton()
        {
            if (CurrentFloor != null)
            {
                if (CurrentFloor.Level < DestinationFloor) CurrentFloor.PressUpButton();
                else CurrentFloor.PressDownButton();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elevator"></param>
        /// <returns></returns>
        public bool OnEntranceAvailable(Elevator elevator)
        {
            bool enter = false;
            if (elevator.CanUserEnter(this))
            {
                enter = elevator.EnterUser(this);
                if (enter)
                {
                    CurrentElevator = elevator;
                    CurrentFloor = null;
                }
            }
            return enter;
        }
    }

    /// <summary>
    /// Child, has not a big weight.
    /// </summary>
    public class Child : User
    {
        public Child(Floor cf) : base(cf) { }

        /// <summary>
        /// Children usually light. 
        /// In current situation will be 30 kg.
        /// </summary>
        public override int Weigth
        {
            get
            {
                return 30;
            }
        }
    }

    /// <summary>
    /// Average adult man. Has a big weight
    /// </summary>
    public class Man : User
    {
        public Man(Floor cf) : base(cf) { }

        /// <summary>
        /// This adult man, has 100kg weight. Uhh.
        /// </summary>
        public override int Weigth
        {
            get
            {
                return 100;
            }
        }
    }

    /// <summary>
    /// Average adult female
    /// </summary>
    public class Women : User
    {
        public Women(Floor cf) : base(cf) { }

        /// <summary>
        /// Female, she is 60kg.
        /// </summary>
        public override int Weigth
        {
            get
            {
                return 60;
            }
        }
    }

    /// <summary>
    /// A wheelchaired person.
    /// </summary>
    public class WheelChaired : User
    {
        public WheelChaired(Floor cf) : base(cf) { }

        /// <summary>
        /// Has 80 kg weight.
        /// </summary>
        public override int Weigth
        {
            get
            {
                return 80;
            }
        }
    }
}
