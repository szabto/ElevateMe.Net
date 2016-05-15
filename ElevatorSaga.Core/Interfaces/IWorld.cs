using ElevatorSaga.Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSaga.Core.Interfaces
{
    /// <summary>
    /// Interface for create the dll main
    /// </summary>
    public interface IWorld
    {
        /// <summary>
        /// Author name
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Authors email address
        /// </summary>
        string AuthorEmail { get; }

        /// <summary>
        /// The Entry point of the dll
        /// </summary>
        void Initialize();

        /// <summary>
        /// Triggered when world generation is done.
        /// </summary>
        /// <param name="floors">Floor list.</param>
        /// <param name="elevators">Elevator list.</param>
        void WorldGenerated(List<Floor> floors, List<Elevator> elevators);
        
        /// <summary>
        /// Triggered on every changed frame
        /// </summary>
        /// <param name="frame">Frame number</param>
        /// <param name="world">World object</param>
        void Update(int frame, World world);
    }
}
