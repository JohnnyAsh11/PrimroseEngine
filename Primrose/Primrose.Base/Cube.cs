using Primrose.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Primrose.GameCore;
using System.Collections.Generic;
using System;

namespace Primrose.Base
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

        /// <summary>
        /// Get/Set property for the X position.
        /// </summary>
        public float X 
        {
            get { return _position.X; }
            set { _position.X = value; }
        }

        /// <summary>
        /// Get/Set property for the Y position.
        /// </summary>
        public float Y
        {
            get { return _position.Y; }
            set { _position.Y = value; }
        }

        /// <summary>
        /// Get/Set property for the Z position.
        /// </summary>
        public float Z
        {
            get { return _position.Z; }
            set { _position.Z = value; }
        }

        /// <summary>
        /// Get/Set property for the _position field.
        /// </summary>
        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
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
        /// Constructs an instance a unit cube at the specified position.
        /// </summary>
        /// <param name="position">Position of the cube.</param>
        public Cube(Vector3 position)
        {
            _position = position;
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
            if (width < 0 ||
                height < 0 ||
                length < 0)
            {
                throw new Exception("Can not have negative cube dimensions.");
            }

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
            if (width < 0 ||
                height < 0 ||
                length < 0)
            {
                throw new Exception("Can not have negative cube dimensions.");
            }

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
            //{
            //
            //}

            return false;
        }

        /// <summary>
        /// Draws the dimensions of the of the cube to view for debugging purposes.
        /// </summary>
        /// <param name="graphics">Graphics device used to render the cube's faces.</param>
        /// <param name="camera">Camera used for view and projection matrices.</param>
        /// <param name="tint">Color to render the cube as.</param>
        public void DebugDraw(GraphicsDevice graphics, Camera camera, Color tint)
        {
            List<VertexPositionColor> vertexList = new List<VertexPositionColor>();

            Renderer renderVertices = new Renderer(graphics);

            // Creating the vertices to be put into the buffer for 
            //  rendering all faces of the cube.
            #region first side
            renderVertices.Add(new Vector3(_position.X, _position.Y, _position.Z), tint);
            renderVertices.Add(new Vector3(_position.X + _width, _position.Y, _position.Z), tint);
            renderVertices.Add(new Vector3(_position.X, _position.Y + _height, _position.Z), tint);
            renderVertices.Add(new Vector3(_position.X, _position.Y + _height, _position.Z), tint);
            renderVertices.Add(new Vector3(_position.X + _width, _position.Y, _position.Z), tint);
            renderVertices.Add(new Vector3(_position.X + _width, _position.Y + _height, _position.Z), tint);
            #endregion
            #region second side
            renderVertices.Add(new Vector3(_position.X + _width, _position.Y, _position.Z), tint);
            renderVertices.Add(new Vector3(_position.X + _width, _position.Y, _position.Z + _length), tint);
            renderVertices.Add(new Vector3(_position.X + _width, _position.Y + _height, _position.Z), tint);
            renderVertices.Add(new Vector3(_position.X + _width, _position.Y + _height, _position.Z), tint);
            renderVertices.Add(new Vector3(_position.X + _width, _position.Y, _position.Z + _length), tint);
            renderVertices.Add(new Vector3(_position.X + _width, _position.Y + _height, _position.Z + _length), tint);
            #endregion
            #region third side
            renderVertices.Add(new Vector3(_position.X + _width, _position.Y + _height, _position.Z + _length), tint);
            renderVertices.Add(new Vector3(_position.X + _width, _position.Y, _position.Z + _length), tint);
            renderVertices.Add(new Vector3(_position.X, _position.Y, _position.Z + _length), tint);
            renderVertices.Add(new Vector3(_position.X, _position.Y, _position.Z + _length), tint);
            renderVertices.Add(new Vector3(_position.X, _position.Y + _height, _position.Z + _length), tint);
            renderVertices.Add(new Vector3(_position.X + _width, _position.Y + _height, _position.Z + _length), tint);
            #endregion
            #region fourth side
            renderVertices.Add(new Vector3(_position.X, _position.Y + _height, _position.Z), tint);
            renderVertices.Add(new Vector3(_position.X, _position.Y, _position.Z + _length), tint);
            renderVertices.Add(new Vector3(_position.X, _position.Y, _position.Z), tint);
            renderVertices.Add(new Vector3(_position.X, _position.Y, _position.Z + _length), tint);
            renderVertices.Add(new Vector3(_position.X, _position.Y + _height, _position.Z), tint);
            renderVertices.Add(new Vector3(_position.X, _position.Y + _height, _position.Z + _length), tint);
            #endregion            
            #region top
            renderVertices.Add(new Vector3(_position.X, _position.Y + _height, _position.Z), tint);
            renderVertices.Add(new Vector3(_position.X + _width, _position.Y + _height, _position.Z), tint);
            renderVertices.Add(new Vector3(_position.X, _position.Y + _height, _position.Z + _length), tint);
            renderVertices.Add(new Vector3(_position.X + _width, _position.Y + _height, _position.Z + _length), tint);
            renderVertices.Add(new Vector3(_position.X, _position.Y + _height, _position.Z + _length), tint);
            renderVertices.Add(new Vector3(_position.X + _width, _position.Y + _height, _position.Z), tint);
            #endregion            
            #region bottom
            renderVertices.Add(new Vector3(_position.X, _position.Y, _position.Z + _length), tint);
            renderVertices.Add(new Vector3(_position.X + _width, _position.Y, _position.Z + _length), tint);
            renderVertices.Add(new Vector3(_position.X + _width, _position.Y, _position.Z), tint);
            renderVertices.Add(new Vector3(_position.X + _width, _position.Y, _position.Z), tint);
            renderVertices.Add(new Vector3(_position.X, _position.Y, _position.Z), tint);
            renderVertices.Add(new Vector3(_position.X, _position.Y, _position.Z + _length), tint);
            #endregion

            renderVertices.Draw(camera);
        }

    }
}
