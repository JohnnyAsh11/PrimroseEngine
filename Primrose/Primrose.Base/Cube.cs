using Primrose.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Primrose.GameCore;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace Primrose.Base
{
    /// <summary>
    /// Creates a Cube in space that defines a position, dimensions and collidability.
    /// </summary>
    public struct Cube : ICollide, ITransform
    {
        // Fields:
        private Vector3 _position;
        private float _width;
        private float _height;
        private float _length;
        private Renderer _renderer;
        private GraphicsDevice _graphics;

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
        /// Unofficial default constructor for the Cube struct.
        /// </summary>
        /// <param name="graphics">Graphics device object for setting the vertices.</param>
        public Cube(GraphicsDevice graphics)
        {
            _position = new Vector3(0, 0, 0);
            _width = 1;
            _height = 1;
            _length = 1;
            _renderer = null;
            _graphics = graphics;

            SetRenderVertices(graphics, Color.CornflowerBlue, VertexType.FilledWireFrame);
        }

        /// <summary>
        /// Constructs an instance a unit cube at the specified position.
        /// </summary>
        /// <param name="graphics">Graphics device object for setting the vertices.</param>
        /// <param name="position">Position of the cube.</param>
        public Cube(GraphicsDevice graphics, Vector3 position)
        {
            _position = position;
            _width = 1;
            _height = 1;
            _length = 1;
            _renderer = null;
            _graphics = graphics;

            SetRenderVertices(graphics, Color.CornflowerBlue, VertexType.FilledWireFrame);
        }

        /// <summary>
        /// Constructs the Cube struct with the passed in dimensions and a starting position of 0, 0, 0
        /// </summary>
        /// <param name="graphics">Graphics device object for setting the vertices.</param>
        /// <param name="width">The width of the cube.</param>
        /// <param name="height">The height of the cube.</param>
        /// <param name="length">The length of the cube.</param>
        public Cube(
            GraphicsDevice graphics, 
            float width, 
            float height, 
            float length)
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
            _renderer = null;
            _graphics = graphics;

            SetRenderVertices(graphics, Color.CornflowerBlue, VertexType.FilledWireFrame);
        }

        /// <summary>
        /// Constructs the Cube struct with the passed in dimensions and position.
        /// </summary>
        /// <param name="graphics">Graphics device object for setting the vertices.</param>
        /// <param name="width">The width of the cube.</param>
        /// <param name="height">The height of the cube.</param>
        /// <param name="length">The length of the cube.</param>
        /// <param name="position">The starting position of the Cube</param>
        public Cube(
            GraphicsDevice graphics,
            Vector3 position, 
            float width, 
            float height, 
            float length)
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
            _renderer = null;
            _graphics = graphics;

            SetRenderVertices(graphics, Color.CornflowerBlue, VertexType.FilledWireFrame);
        }

        /// <summary>
        /// Constructs the Cube struct with the passed in dimensions and position.
        /// </summary>
        /// <param name="graphics">Graphics device object for setting the vertices.</param>
        /// <param name="width">The width of the cube.</param>
        /// <param name="height">The height of the cube.</param>
        /// <param name="length">The length of the cube.</param>
        /// <param name="position">The starting position of the Cube</param>
        /// <param name="color">The color of the Cube primitive.</param>
        public Cube(
            GraphicsDevice graphics,
            Vector3 position,
            float width,
            float height,
            float length,
            Color color)
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
            _renderer = null;
            _graphics = graphics;

            SetRenderVertices(graphics, color, VertexType.FilledWireFrame);
        }

        // Methods:
        /// <summary>
        /// Checks if a collision occured between 2 ICollid objects.
        /// </summary>
        /// <param name="collidable">The other collidable object.</param>
        /// <returns>Whether or not a collision has occured.</returns>
        public bool CheckCollision(ICollide collidable)
        {
            if (collidable is Cube)
            {
                Cube cube = (Cube)collidable;

                // Performing the full AABB check.
                return cube.X < (this.X + this.Width) &&
                       (cube.X + cube.Width) > this.X &&
                       cube.Y < (this.Y + this.Height) &&
                       (cube.Y + cube.Height) > this.Y &&
                       cube.Z < (this.Z + this.Length) &&
                       (cube.Z + cube.Length) > this.Z;

            }

            return false;
        }

        /// <summary>
        /// Transforms an object by the transformation matrix.
        /// </summary>
        /// <param name="transformation">The transformation being applied to the primitive.</param>
        public void Transform(Matrix transformation)
        {
            // Making sure that there are vertices.
            if (_renderer.Vertices is null)
            {
                return;
            }

            // Looping through and appling the translation to all vertices.
            for (int i = 0; i < _renderer.Count; i++)
            {
                // Saving the current iteration as variables.
                VertexPositionColor vertex = _renderer.Vertices[i];

                // Applying the transformation.
                vertex.Position = Vector3.Transform(vertex.Position, transformation);

                // Setting the new vertex.
                _renderer.Vertices[i] = vertex;
            }

            if (_renderer.Lines is not null)
            {
                for (int i = 0; i < _renderer.Lines.Count; i++)
                {
                    // Setting a reference to the current line.
                    Line line = _renderer.Lines[i];

                    // Translating the line's start and end points.
                    line.Start = Vector3.Transform(line.Start, transformation);
                    line.End = Vector3.Transform(line.End, transformation);

                    // Sending it back to the list.
                    _renderer.Lines[i] = line;
                }
            }

            // Applying the translation to the Vector3 origin.
            _position = Vector3.Transform(_position, transformation);
        }

        /// <summary>
        /// Sets the vertices of the cube to draw.
        /// </summary>
        /// <param name="graphics">Graphics device used to render the cube's faces.</param>
        /// <param name="color">Color to render the cube as.</param>
        /// <param name="vertexType">Defines the style that the primitive is being drawn.</param>
        public void SetRenderVertices(GraphicsDevice graphics, Color color, VertexType vertexType)
        {
            List<VertexPositionColor> vertexList = new List<VertexPositionColor>();

            // Initializing the renderer;
            _renderer = new Renderer(graphics);

            // Creating the vertices to be put into the buffer for 
            //  rendering all faces of the cube.

            // First side.
            _renderer.AddQuad(
                new Vector3(_position.X, _position.Y, _position.Z),
                new Vector3(_position.X + _width, _position.Y, _position.Z),
                new Vector3(_position.X + _width, _position.Y + _height, _position.Z),
                new Vector3(_position.X, _position.Y + _height, _position.Z),
                color,
                vertexType);

            // Second side.
            _renderer.AddQuad(
                new Vector3(_position.X + _width, _position.Y, _position.Z),
                new Vector3(_position.X + _width, _position.Y, _position.Z + _length),
                new Vector3(_position.X + _width, _position.Y + _height, _position.Z + _length),
                new Vector3(_position.X + _width, _position.Y + _height, _position.Z),
                color,
                vertexType);

            // Third side.
            _renderer.AddQuad(
                new Vector3(_position.X + _width, _position.Y + _height, _position.Z + _length),
                new Vector3(_position.X + _width, _position.Y, _position.Z + _length),
                new Vector3(_position.X, _position.Y, _position.Z + _length),
                new Vector3(_position.X, _position.Y + _height, _position.Z + _length),
                color,
                vertexType);

            // Fourth side.
            _renderer.AddQuad(
                new Vector3(_position.X, _position.Y + _height, _position.Z),
                new Vector3(_position.X, _position.Y + _height, _position.Z + _length),
                new Vector3(_position.X, _position.Y, _position.Z + _length),
                new Vector3(_position.X, _position.Y, _position.Z),
                color,
                vertexType);

            // Top.
            _renderer.AddQuad(
                new Vector3(_position.X, _position.Y + _height, _position.Z),
                new Vector3(_position.X + _width, _position.Y + _height, _position.Z),
                new Vector3(_position.X + _width, _position.Y + _height, _position.Z + _length),
                new Vector3(_position.X, _position.Y + _height, _position.Z + _length),
                color,
                vertexType);

            // Bottom.
            _renderer.AddQuad(
                new Vector3(_position.X, _position.Y, _position.Z + _length),
                new Vector3(_position.X + _width, _position.Y, _position.Z + _length),
                new Vector3(_position.X + _width, _position.Y, _position.Z),
                new Vector3(_position.X, _position.Y, _position.Z),
                color,
                vertexType);
        }

        /// <summary>
        /// Renders the Cube's mesh to the window.
        /// </summary>
        /// <param name="camera">Camera used for matrices and renderer calls.</param>
        /// <param name="color">Color of the primitive in the world.</param>
        /// <param name="a_m4WorldMatrix">World matrix of the object being rendered.</param>
        public void Draw(Camera camera, Color color, Matrix a_m4WorldMatrix)
        {
            // Calling the renderer draw.
            _renderer.Draw(camera, color, a_m4WorldMatrix);
        }
    }
}
