using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primrose.Interface
{
    public interface IPhysical : IUpdate
    {
        /// <summary>
        /// Calculates the steering force for the IPhysical object.
        /// </summary>
        public void CalcSteeringForces();

    }
}
