using Primrose.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Primrose.Primrose.Base
{
    public struct Cube : ICollid
    {

        // Fields:
        private Vector3 _position;
        private float _width;
        private float _height;
        private float _length;

        // Properties:
        /// <summary>
        /// Get/Sets the width for the Cube
        /// </summary>
        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// Get/Sets the height for the Cube
        /// </summary>
        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// Get/Sets the length for the Cube
        /// </summary>
        public float Length
        {
            get { return _length; }
            set { _length = value; }
        }

        // Constructors:
        /// <summary>
        /// Default constructor for the Cube struct.
        /// </summary>
        public Cube()
        {
            _position = new Vector3(0, 0, 0);
            _width = 1;
            _height = 1;
            _length = 1;
        }

        /// <summary>
        /// Constructs the Cube struct with the passed in dimensions and a starting position of 0, 0, 0
        /// </summary>
        /// <param name="width">The width of the cube.</param>
        /// <param name="height">The height of the cube.</param>
        /// <param name="length">The length of the cube.</param>
        public Cube(float width, float height, float length)
        {
            _position = new Vector3(0, 0, 0);
            _width = width;
            _height = height;
            _length = length;
        }

        /// <summary>
        /// Constructs the Cube struct with the passed in dimensions and position.
        /// </summary>
        /// <param name="width">The width of the cube.</param>
        /// <param name="height">The height of the cube.</param>
        /// <param name="length">The length of the cube.</param>
        /// <param name="position">The starting position of the Cube</param>
        public Cube(Vector3 position, float width, float height, float length)
        {
            _position = position;
            _width = width;
            _height = height;
            _length = length;
        }

        // Methods:
        /// <summary>
        /// Checks if a collision occured between 2 ICollid objects.
        /// </summary>
        /// <param name="collidable">The other collidable object.</param>
        /// <returns>Whether or not a collision has occured.</returns>
        public bool CheckCollision(ICollid collidable)
        {
            //if ()

            return false;
        }

    }
}
