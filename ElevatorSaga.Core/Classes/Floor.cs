using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSaga.Core.Classes
{
    public class FloorButtonPressedEventArgs : EventArgs
    {
        public readonly Direction Button;

        public FloorButtonPressedEventArgs(Direction b)
        {
            this.Button = b;
        }
    }

    public class FloorButtonStateChangedEventArgs : EventArgs
    {
        public readonly Dictionary<Direction, bool> ButtonStates;
        public readonly Direction ChangedDirection;
        public readonly bool ChangedValue;

        public FloorButtonStateChangedEventArgs(Dictionary<Direction, bool> bs, Direction dir, bool cv)
        {
            ButtonStates = bs;
            ChangedDirection = dir;
            ChangedValue = cv;
        }
    }

    public class Floor
    {
        private readonly Dictionary<Direction, bool> buttonStates = new Dictionary<Direction, bool>()
        {
            { Direction.Up, false },
            { Direction.Down, false }
        };

        public readonly int Level;

        public readonly bool IsTopFloor;
        public readonly bool IsBottomFloor;

        private List<User> awaitingUsers = new List<User>();

        public EventHandler<FloorButtonPressedEventArgs> OnButtonPressed;
        public EventHandler<FloorButtonStateChangedEventArgs> OnButtonStateChanged;

        public Floor(int level, bool it = false, bool ib = false)
        {
            Level = level;

            IsTopFloor = it;
            IsBottomFloor = ib;
        }

        private void ButtonPressed_Internal(Direction dir)
        {
            if (!buttonStates[dir])
            {
                buttonStates[dir] = true;

                if (OnButtonPressed != null)
                    OnButtonPressed(this, new FloorButtonPressedEventArgs(dir));
                if (OnButtonStateChanged != null)
                    OnButtonStateChanged.Invoke(this, new FloorButtonStateChangedEventArgs(buttonStates, dir, true));
            }
        }

        public void PressUpButton()
        {
            ButtonPressed_Internal(Direction.Up);
        }

        public void PressDownButton()
        {
            ButtonPressed_Internal(Direction.Down);
        }

        protected void OnEntranceAvailable(Elevator e)
        {
            Direction changeDir = Direction.None;
            if (buttonStates[Direction.Up] && e.GoingUpIndicator)
            {
                changeDir = Direction.Up;
                buttonStates[Direction.Up] = false;

            }
            else if (buttonStates[Direction.Down] && e.GoingDownIndicator)
            {
                changeDir = Direction.Down;
                buttonStates[Direction.Down] = false;
            }

            if (changeDir != Direction.None)
            {
                if (OnButtonStateChanged != null)
                    OnButtonStateChanged.Invoke(this, new FloorButtonStateChangedEventArgs(buttonStates, changeDir, false));
            }
        }

        private List<User> GetUsersToDirection(Direction dir)
        {
            return awaitingUsers.FindAll(user => dir == Direction.Up ? this.Level < user.DestinationFloor.Level : this.Level > user.DestinationFloor.Level).ToList<User>();
        }
    }
}
