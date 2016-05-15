using ElevatorSaga.Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSaga.Core.Interfaces
{
    public interface IWorld
    {
        string Author { get; }

        string AuthorEmail { get; }

        

        void Initialize();

        void WorldGenerated(List<Floor> floors, List<Elevator> elevators);

        void Update(int frame, World world);
    }
}
