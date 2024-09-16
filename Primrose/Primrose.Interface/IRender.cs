using Microsoft.Xna.Framework;

namespace Primrose.Interface
{
    /// <summary>
    /// Contains the definitions of object that is rendered to the screen.
    /// </summary>
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
