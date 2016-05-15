using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSaga.Core.Classes
{
    public class IndicatorEventArgs : EventArgs
    {
        public readonly Direction Direction;
        public readonly bool Value;

        public IndicatorEventArgs(Direction dir, bool val)
        {
            Direction = dir;
            Value = val;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Elevator
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
        /// Event triggered, when some of indicator has been changed.
        /// </summary>
        public EventHandler<IndicatorEventArgs> IndicatorChanged;

        private int CurrentWeight { get { return _usersIn.Sum(x => x.Weigth); } }

        /// <summary>
        /// Returns the estimated user capacity of the elevator, based on avg weight, 80 kg, and elevator's maximum weight capacity.
        /// </summary>
        public int EstimatedCapacity
        {
            get { return (int)Math.Floor((decimal)MaxWeight / 80); }
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
        /// Returns the current weight load of the elevator in percentage.
        /// </summary>
        /// <returns>Elevators current load. Min is 0, max is 100</returns>
        public int GetCurrentLoad()
        {
            return (int)Math.Round((decimal)CurrentWeight / MaxWeight * 100);
        }

        /// <summary>
        /// Determines if the desired user can enter to the elevator based on user's weight, and elevator's current weight.
        /// </summary>
        /// <param name="user">User what is passed.</param>
        /// <returns>True if user can enter, false if not</returns>
        public bool CanUserEnter(User user)
        {
            if (user == null) throw new NullReferenceException("User cannot be null!");
            return CurrentWeight + user.Weigth < MaxWeight;
        }
    }
}
