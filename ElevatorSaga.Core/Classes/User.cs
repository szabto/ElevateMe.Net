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
        public readonly Floor DestinationFloor;

        public bool IsDone { get; private set; }

        /// <summary>
        /// Creates a user with waiting floor, and a destination floor
        /// </summary>
        /// <param name="currentFloor">The current floor, user is waiting on</param>
        /// <param name="destinationFloor">The destination floor, where user wants to go</param>
        public User(Floor currentFloor, Floor destinationFloor)
        {
            CurrentFloor = currentFloor;
            DestinationFloor = destinationFloor;
        }

        private void OnEntranceAvailable(Elevator e)
        {
            if (e.CanUserEnter(this))
            {

            }
        }
    }

    /// <summary>
    /// Child, has not a big weight.
    /// </summary>
    public class Child : User
    {
        public Child(Floor cf, Floor df) : base(cf, df) { }

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
        public Man(Floor cf, Floor df) : base(cf, df) { }

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
        public Women(Floor cf, Floor df) : base(cf, df) { }

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
        public WheelChaired(Floor cf, Floor df) : base(cf, df) { }

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
