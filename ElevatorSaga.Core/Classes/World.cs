using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSaga.Core.Classes
{
    /// <summary>
    /// Event for adding floors to world.
    /// </summary>
    public class FloorAddedEventArgs : EventArgs
    {
        /// <summary>
        /// The added floor.
        /// </summary>
        public readonly Floor Floor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="floor"></param>
        public FloorAddedEventArgs(Floor floor)
        {
            Floor = floor;
        }
    }

    /// <summary>
    /// Event for elevator adding to world.
    /// </summary>
    public class ElevatorAddedEventArgs : EventArgs
    {
        /// <summary>
        /// The added elevator.
        /// </summary>
        public readonly Elevator Elevator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="elevator"></param>
        public ElevatorAddedEventArgs(Elevator elevator)
        {
            Elevator = elevator;
        }
    }

    /// <summary>
    /// Main world. This will do everything.
    /// </summary>
    public class World
    {
        private readonly List<Elevator> _elevators = new List<Elevator>();
        private readonly List<Floor> _floors = new List<Floor>();

        /// <summary>
        /// Event in world, triggered when a new floor added
        /// </summary>
        public EventHandler<FloorAddedEventArgs> FloorAdded;

        /// <summary>
        /// Event in world, triggered when new elevator added
        /// </summary>
        public EventHandler<ElevatorAddedEventArgs> ElevatorAdded;
    
        /// <summary>
        /// Function to generate the World.
        /// </summary>
        public void Generate()
        {
            int maxFloors = 4;
            for (int i = 0; i < maxFloors; i++)
            {
                Floor cf = new Floor(i, i == maxFloors-1, i == 0);

                _floors.Add(cf);
                if (FloorAdded != null) FloorAdded(this, new FloorAddedEventArgs(cf));
            }

            for (int i = 0; i < 2; i++)
            {
                Elevator ce = new Elevator(800);

                _elevators.Add(ce);
                if (ElevatorAdded != null) ElevatorAdded(this, new ElevatorAddedEventArgs(ce));
            }
        }

        /// <summary>
        /// Loads the user's dll
        /// </summary>
        public void LoadUserCode()
        {

        }
    }
}
