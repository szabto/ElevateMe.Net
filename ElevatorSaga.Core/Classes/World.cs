using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSaga.Core.Classes
{
    public class FloorEventArgs : EventArgs
    {
        public readonly Floor Floor;

        public FloorEventArgs(Floor f)
        {
            Floor = f;
        }
    }

    public class ElevatorEventArgs : EventArgs
    {
        public readonly Elevator Elevator;

        public ElevatorEventArgs(Elevator e)
        {
            Elevator = e;
        }
    }

    public class World
    {
        private readonly List<Elevator> _elevators = new List<Elevator>();
        private readonly List<Floor> _floors = new List<Floor>();

        public EventHandler<FloorEventArgs> FloorAdded;
        public EventHandler<ElevatorEventArgs> ElevatorAdded;
    
        public void Generate()
        {
            int maxFloors = 4;
            for (int i = 0; i < maxFloors; i++)
            {
                Floor cf = new Floor(i, i == maxFloors-1, i == 0);

                _floors.Add(cf);
                if (FloorAdded != null)
                    FloorAdded(this, new FloorEventArgs(cf));
            }

            for (int i = 0; i < 2; i++)
            {
                Elevator ce = new Elevator(800);

                _elevators.Add(ce);
                if (ElevatorAdded != null)
                    ElevatorAdded(this, new ElevatorEventArgs(ce));
            }
        }

        public void LoadUserCode()
        {

        }
    }
}
