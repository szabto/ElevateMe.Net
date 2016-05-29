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
        public readonly Floor Floor;

        public FloorButtonPressedEventArgs(Floor f, Direction b)
        {
            this.Floor = f;
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

    /// <summary>
    /// TODO
    /// </summary>
    public class Floor
    {
        private readonly Dictionary<Direction, bool> buttonStates = new Dictionary<Direction, bool>()
        {
            { Direction.Up, false },
            { Direction.Down, false }
        };

        /// <summary>
        /// Shows the floor's level.
        /// </summary>
        public readonly int Level;

        /// <summary>
        /// Determines if current floor is Top
        /// </summary>
        public readonly bool IsTopFloor;
        /// <summary>
        /// Determines if current floor is bottom.
        /// </summary>
        public readonly bool IsBottomFloor;

        private List<User> awaitingUsers = new List<User>();

        /// <summary>
        /// TODO
        /// </summary>
        public User[] AwaitingUsers { get { User[] list = null; lock (awaitingUsers) { list = awaitingUsers.ToArray(); } return list; } }

        /// <summary>
        /// Event for button presses on the floor. This triggered by awaiting users, or GUI interaction.
        /// </summary>
        public EventHandler<FloorButtonPressedEventArgs> OnButtonPressed;

        /// <summary>
        /// Event for button state changes on the floor. This triggered by awaiting users, GUI interaction, or Elevator arrival, or Elevator Direction change.
        /// </summary>
        public EventHandler<FloorButtonStateChangedEventArgs> OnButtonStateChanged;

        /// <summary>
        /// Floor constructor. 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="it"></param>
        /// <param name="ib"></param>
        public Floor(int level, bool it = false, bool ib = false)
        {
            Level = level;

            IsTopFloor = it;
            IsBottomFloor = ib;

            World.Instance.ElevatorAdded += OnElevatorAdded;
        }

        private void OnElevatorAdded(object sender, ElevatorAddedEventArgs eargs)
        {
            eargs.Elevator.StoppedAtFloor += OnElevatorStopped;
        }

        private void OnElevatorStopped(object sender, FloorEventArgs eargs)
        {
            if (eargs.Floor == this.Level)
            {
                OnEntranceAvailable(eargs.Elevator);
            }
        }

        private void ButtonPressed_Internal(Direction dir)
        {
            if (!buttonStates[dir])
            {
                buttonStates[dir] = true;

                if (OnButtonPressed != null)
                    OnButtonPressed(this, new FloorButtonPressedEventArgs(this, dir));
                if (OnButtonStateChanged != null)
                    OnButtonStateChanged.Invoke(this, new FloorButtonStateChangedEventArgs(buttonStates, dir, true));
            }
        }

        Random rnd = new Random();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gt"></param>
        public void Update(int gt)
        {
            awaitingUsers.ForEach(x => x.Update(gt));

            if(rnd.Next(100) > 95)
            {
                lock(awaitingUsers)
                {
                    User u = User.GetRandom(this);
                    awaitingUsers.Add(u);
                }
            }
        }

        /// <summary>
        /// Press up botton in the floor. (Can pressed only once.)
        /// </summary>
        public void PressUpButton()
        {
            ButtonPressed_Internal(Direction.Up);
        }

        /// <summary>
        /// Press down botton in the floor. (Can pressed only once.)
        /// </summary>
        public void PressDownButton()
        {
            ButtonPressed_Internal(Direction.Down);
        }

        private void OnEntranceAvailable(Elevator e)
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
                    OnButtonStateChanged(this, new FloorButtonStateChangedEventArgs(buttonStates, changeDir, false));
            }

            Inner_EntranceAvailable(e);
        }

        private void Inner_EntranceAvailable(Elevator e)
        {
            User[] usersToDir;
            if (e.GoingDownIndicator != e.GoingUpIndicator)
            {
                usersToDir = GetUsersToDirection(e.GoingDownIndicator ? Direction.Down : Direction.Up).ToArray();
            }
            else
            {
                usersToDir = awaitingUsers.ToArray();
            }

            foreach (User u in usersToDir)
            {
                if (!u.OnEntranceAvailable(e)) // when user could not enter to elevator, press again the direction button.
                {
                    u.PressButton();
                }
            }
        }

        private List<User> GetUsersToDirection(Direction dir)
        {
            return awaitingUsers.FindAll(user => dir == Direction.Up ? this.Level < user.DestinationFloor : this.Level > user.DestinationFloor).ToList<User>();
        }
    }
}
