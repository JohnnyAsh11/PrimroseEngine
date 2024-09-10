using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primrose.Interface
{
    public interface IRender
    {
        /// <summary>
        /// Renders an object to the main window.
        /// </summary>
        /// <param name="view">The camera's view matrix.</param>
        /// <param name="projection">The camera's projection matrix.</param>
        /// <param name="cameraPosition">The camera's current position.</param>
        public void Draw(Matrix view, Matrix projection, Vector3 cameraPosition);
    }
}
