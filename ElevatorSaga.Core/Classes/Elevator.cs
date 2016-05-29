using ElevatorSaga.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatorSaga.Core.Classes
{

    public class ElevatorStoppedEventArgs : EventArgs
    {
        public readonly Elevator Elevator;
        public readonly int Floor;

        public ElevatorStoppedEventArgs(Elevator el, int fl)
        {
            Elevator = el;
            Floor = fl;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class IndicatorEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly Direction Direction;
        /// <summary>
        /// 
        /// </summary>
        public readonly bool Value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="val"></param>
        public IndicatorEventArgs(Direction dir, bool val)
        {
            Direction = dir;
            Value = val;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Elevator : IElevatorGui
    {
        private Direction _direction = Direction.None;
        /// <summary>
        /// Move direction. If the elevator is stopped, then Direction will be Direction.None
        /// </summary>
        public Direction Direction { get { return _direction; } }

        private bool _goingUpIndicator = true;
        /// <summary>
        /// Indicates when elevator can go up. Will affect User's behavior.
        /// </summary>
        public bool GoingUpIndicator { get { return _goingUpIndicator; } set { if (IndicatorChanged != null && _goingUpIndicator != value) IndicatorChanged(this, new IndicatorEventArgs(Direction.Up, value)); _goingUpIndicator = value; } }

        private bool _goingDownindicator = true;
        /// <summary>
        /// Indicates when elevator can go down. Will affect User's behavior.
        /// </summary>
        public bool GoingDownIndicator { get { return _goingDownindicator; } set { if (IndicatorChanged != null && _goingDownindicator != value) IndicatorChanged(this, new IndicatorEventArgs(Direction.Up, value)); _goingDownindicator = value; } }

        /// <summary>
        /// Floor list with destination levels. Not sorted automatically
        /// </summary>
        public List<int> DestinationQueue = new List<int>();

        private bool waitingForUsers = false;
        private bool isMoving = false;

        private int NextLevel = 0;

        /// <summary>
        /// Event triggered, when some of indicator has been changed.
        /// </summary>
        public EventHandler<IndicatorEventArgs> IndicatorChanged;

        /// <summary>
        /// Event when a not queued floor is passed
        /// </summary>
        public EventHandler<EventArgs> PassFloor;

        /// <summary>
        /// Event for stopping at floor.
        /// </summary>
        public EventHandler<ElevatorStoppedEventArgs> StoppedAtFloor;

        public EventHandler<EventArgs> Idle;

        private int CurrentWeight { get { return _usersIn.Sum(x => x.Weigth); } }

        /// <summary>
        /// Returns the estimated user capacity of the elevator, based on avg weight, 80 kg, and elevator's maximum weight capacity.
        /// </summary>
        public int EstimatedCapacity
        {
            get { return (int)Math.Floor((decimal)MaxWeight / 80); }
        }

        private float _position = 0;
        public float Positinon
        {
            get
            {
                return _position;
            }
        }

        /// <summary>
        /// Return the elevator's maximum weight capacity.
        /// </summary>
        public readonly int MaxWeight;

        private readonly List<User> _usersIn = new List<User>();

        /// <summary>
        /// Elevator constructor. A max weight can passed.
        /// </summary>
        /// <param name="maxWeight">Maximum weight what elevator can lift.</param>
        public Elevator(int maxWeight = 400)
        {
            MaxWeight = maxWeight;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="goFirst"></param>
        public void GoToFloor(int floor, bool goFirst = false)
        {
            if (goFirst) DestinationQueue.Insert(0, floor);
            else DestinationQueue.Add(floor);
        }

        /// <summary>
        /// Returns the current weight load of the elevator in percentage.
        /// </summary>
        /// <returns>Elevators current load. Min is 0, max is 100</returns>
        public int GetCurrentLoad()
        {
            return (int)Math.Round((decimal)CurrentWeight / MaxWeight * 100);
        }

        private int idleSince = 0;
        bool idleTriggered = false;

        public void Update(int gt)
        {
            if (!waitingForUsers)
            {
                if (!isMoving && DestinationQueue.Count > 0)
                {
                    NextLevel = DestinationQueue[0];
                    _direction = NextLevel < Positinon ? Direction.Down : Direction.Up;
                    isMoving = true;
                    idleTriggered = false;
                    idleSince = 0;
                }
                if (!isMoving)
                {
                    idleSince++; //TODO set idle time configurable
                    if (idleSince > 20 && !idleTriggered)
                    {
                        idleTriggered = true;
                        if (Idle != null) Idle(this, new EventArgs());
                    }
                }
            }

            if (isMoving && Math.Abs(Positinon - NextLevel) > 0.05)
            {
                if (_direction == Direction.Down)
                    _position -= 0.1f;
                else
                    _position += 0.1f;
            }
            else if (isMoving && Math.Abs(Positinon - NextLevel) <= 0.05)
            {
                DestinationQueue.RemoveAt(0);
                isMoving = false;
                _direction = Direction.None;
                if (StoppedAtFloor != null) StoppedAtFloor(this, new ElevatorStoppedEventArgs(this, (int)Math.Round(Positinon)));
                waitingForUsers = true;

                ExitUsers();
            }
        }

        private void ExitUsers()
        {
            new Thread(() =>
            {
                Thread.Sleep(1000); //TODO change this
                waitingForUsers = false;
            }).Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool EnterUser(User user)
        {
            if (user == null) throw new NullReferenceException("Parameter User cannot be null.");
            bool success = false;

            if (CanUserEnter(user))
            {
                _usersIn.Add(user);
                success = true;
            }

            return success;
        }

        /// <summary>
        /// Determines if the desired user can enter to the elevator based on user's weight, and elevator's current weight.
        /// </summary>
        /// <param name="user">User what is passed.</param>
        /// <returns>True if user can enter, false if not</returns>
        public bool CanUserEnter(User user)
        {
            if (user == null) throw new NullReferenceException("Parameter User cannot be null!");
            return CurrentWeight + user.Weigth < MaxWeight;
        }
    }
}
