using Microsoft.Xna.Framework;

namespace Primrose.Interface
{
    /// <summary>
    /// Contains definitions for something with a position in space.
    /// </summary>
    public interface IPosition
    {
        /// <summary>
        /// The Vector3 position of the positioned object.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// The X position of the positioned object.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// The Y position of the positioned object.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// The Z position of the positioned object.
        /// </summary>
        public float Z { get; set; }
    }
}
