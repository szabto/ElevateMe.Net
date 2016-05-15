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

    public class Elevator
    {
        private Direction _direction = Direction.None;
        public Direction Direction { get { return _direction; } }

        private bool _goingUpIndicator = true;
        public bool GoingUpIndicator { get { return _goingUpIndicator; } set { if (IndicatorChanged != null && _goingUpIndicator != value) IndicatorChanged(this, new IndicatorEventArgs(Direction.Up, value)); _goingUpIndicator = value; } }

        private bool _goingDownindicator = true;
        public bool GoingDownIndicator { get { return _goingDownindicator; } set { if (IndicatorChanged != null && _goingDownindicator != value) IndicatorChanged(this, new IndicatorEventArgs(Direction.Up, value)); _goingDownindicator = value; } }

        public EventHandler<IndicatorEventArgs> IndicatorChanged;

        private int CurrentWeight { get { return _usersIn.Sum(x => x.Weigth); } }

        public int EstimatedCapacity
        {
            get { return (int)Math.Floor((decimal)MaxWeight / 80); }
        }
        public readonly int MaxWeight;

        private readonly List<User> _usersIn = new List<User>();

        public Elevator(int maxWeight = 400)
        {
            MaxWeight = maxWeight;
        }

        public float GetCurrentLoad()
        {
            return CurrentWeight / MaxWeight;
        }

        public bool CanUserEnter(User u)
        {
            return CurrentWeight + u.Weigth < MaxWeight;
        }
    }
}
