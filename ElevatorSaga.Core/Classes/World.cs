using ElevatorSaga.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        private int currentChallengeIndex = 1;

        public const int FPS = 20;
        public static int UpdateTime { get { return 1000 / FPS; } }
        private int gameTime = 0;


        private readonly Timer MainTimer;

        /// <summary>
        /// Event in world, triggered when a new floor added
        /// </summary>
        public EventHandler<FloorAddedEventArgs> FloorAdded;

        /// <summary>
        /// Event in world, triggered when new elevator added
        /// </summary>
        public EventHandler<ElevatorAddedEventArgs> ElevatorAdded;

        public World()
        {
            MainTimer = new Timer(Update, null, UpdateTime, UpdateTime);
        }
        
        private  void Update(object state)
        {
            gameTime++;

            _elevators.ForEach(x => x.Update(gameTime));
            _floors.ForEach(x => x.Update(gameTime));
        }

        /// <summary>
        /// Function to generate the World.
        /// </summary>
        public void Generate()
        {
            Challenges.Challenge chall = GetNextChallenge();

            if (chall != null)
            {
                foreach (Floor f in chall.Floors)
                {
                    _floors.Add(f);
                    if (FloorAdded != null) FloorAdded(this, new FloorAddedEventArgs(f));
                }

                foreach (Elevator el in chall.Elevators)
                {
                    _elevators.Add(el);
                    if (ElevatorAdded != null) ElevatorAdded(this, new ElevatorAddedEventArgs(el));
                }
            }
        }

        private Challenges.Challenge GetNextChallenge()
        {
            if (Challenges.Challenges.CONTAINER.ContainsKey(currentChallengeIndex++))
            {
                return Challenges.Challenges.CONTAINER[currentChallengeIndex];
            }

            return null;
        }

        /// <summary>
        /// Loads the user's dll
        /// </summary>
        public void LoadUserCode(Type t)
        {
            IWorld customWorld = (IWorld)Activator.CreateInstance(t, _elevators, _floors);
            
        }
    }
}
