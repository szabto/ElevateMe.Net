using ElevatorSaga.Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElevatorSaga.Core.Challenges
{
    class Challenge
    {
        private readonly List<Elevator> _elevators = new List<Elevator>();
        public List<Elevator> Elevators { get { return _elevators; } }

        private readonly List<Floor> _floors = new List<Floor>();
        public List<Floor> Floors { get { return _floors; } }

        private readonly int _spawnInterval = 1;
        public int SpawnInterval { get { return _spawnInterval; } }


        public Challenge(int elevatorCount, int maxWeight, int floorNum, int spawnInterval = 1)
        {
            for (int i = 0; i < elevatorCount; i++)
            {
                _elevators.Add(new Elevator(maxWeight));
            }

            for (int i = 0; i < floorNum; i++)
            {
                _floors.Add(new Floor(i, i == floorNum - 1, i == 0));
            }

            _spawnInterval = spawnInterval;
        }
    }

    class Challenges
    {
        public static readonly Dictionary<int, Challenge> CONTAINER = new Dictionary<int, Challenge>()
        {
            { 1, new Challenge(1, 800, 4, 1) },
            { 2, new Challenge(1, 720, 5, 1) }

        };
    }
}
