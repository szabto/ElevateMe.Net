using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSaga.Core.Classes
{
    public abstract class User
    {
        public abstract int Weigth { get; }

        public Floor CurrentFloor { get; private set; }

        public readonly Floor DestinationFloor;

        public bool IsDone { get; private set; }

        public User(Floor cf, Floor df)
        {
            CurrentFloor = cf;
            DestinationFloor = df;
        }

        public void OnEntranceAvailable(Elevator e)
        {
            if (e.CanUserEnter(this))
            {

            }
        }
    }

    public class Child : User
    {
        public Child(Floor cf, Floor df) : base(cf, df)
        {
        }

        public override int Weigth
        {
            get
            {
                return 30;
            }
        }
    }

    public class Man : User
    {
        public Man(Floor cf, Floor df) : base(cf, df)
        {
        }

        public override int Weigth
        {
            get
            {
                return 100;
            }
        }
    }

    public class Women : User
    {
        public Women(Floor cf, Floor df) : base(cf, df)
        {
        }

        public override int Weigth
        {
            get
            {
                return 60;
            }
        }
    }

    public class WheelChaired : User
    {
        public WheelChaired(Floor cf, Floor df) : base(cf, df)
        {
        }

        public override int Weigth
        {
            get
            {
                return 80;
            }
        }
    }
}
