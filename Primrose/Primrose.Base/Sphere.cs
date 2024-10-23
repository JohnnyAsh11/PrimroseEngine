using Primrose.Interface;
using Microsoft.Xna.Framework;

namespace Primrose.Base
{
    /// <summary>
    /// Creates a Sphere in space that defines a position, dimensions and collidability.
    /// </summary>
    public struct Sphere : ICollide 
    {

        // Fields:
        private Vector3 _position;
        private float _radius;
        private float _diameter;
        private Renderer _renderer;

        // Properties:
        /// <inheritdoc />
        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        /// <inheritdoc />
        public float X
        {
            get { return _position.X; }
            set { _position.X = value; }
        }
        /// <inheritdoc />
        public float Y
        {
            get { return _position.Y; }
            set { _position.Y = value; }
        }
        /// <inheritdoc />
        public float Z
        {
            get { return _position.Z; }
            set { _position.Z = value; }
        }

        public float Width 
        { 
            get { return _diameter; } 
            set { _diameter = value; }
        }
        public float Height 
        { 
            get { return _diameter; }
            set { _diameter = value; }
        }
        public float Length 
        { 
            get { return _diameter; }
            set { _diameter = value; }
        }

        // Constructors:

        // Methods:
        /// <inheritdoc />
        public bool CheckCollision(ICollide collidable)
        {
            return false;
        }

    }
}
