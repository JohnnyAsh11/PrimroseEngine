using Primrose.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using Primrose.GameCore;
using System.Transactions;

namespace Primrose.Base
{
    /// <summary>
    /// Creates a Sphere in space that defines a position, dimensions and collidability.
    /// </summary>
    public struct Sphere : ICollide, ITransform 
    {

        // Fields:
        private Vector3 _origin;
        private float _radius;
        private float _diameter;
        private Renderer _renderer;
        private GraphicsDevice _graphics;

        /// <summary>
        /// Defines a dimension of the sphere.
        /// </summary>
        private float _width, _height, _length;

        // Properties:
        /// <inheritdoc />
        public Vector3 Position
        {
            get { return _origin; }
            set 
            {
                // If there was no value change.
                if (_origin == value) return;

                // Determining the _origin is changing.
                Vector3 deltaPosition = value - _origin;

                // Transform according to the change in position.
                Transform(Matrix.CreateTranslation(deltaPosition));

                // Set the value.
                _origin = value; 
            }
        }
        /// <inheritdoc />
        public float X
        {
            get { return _origin.X; }
            set 
            {
                // If there was no value change.
                if (_origin.X == value) return;

                // Calculating the change in X.
                float deltaX = value - _origin.X;

                // Transforming according to the change.
                Transform(Matrix.CreateTranslation(
                    new Vector3(deltaX, 0.0f, 0.0f)));

                // Setting the new value.
                _origin.X = value;
            }
        }
        /// <inheritdoc />
        public float Y
        {
            get { return _origin.Y; }
            set
            {
                // If there was no value change.
                if (_origin.Y == value) return;

                // Calculating the change in Y.
                float deltaY = value - _origin.Y;

                // Transforming according to the change.
                Transform(Matrix.CreateTranslation(
                    new Vector3(0.0f, deltaY, 0.0f)));

                // Setting the new value.
                _origin.Y = value;
            }
        }
        /// <inheritdoc />
        public float Z
        {
            get { return _origin.Z; }
            set
            {
                // If there was no value change.
                if (_origin.Z == value) return;

                // Calculating the change in Z.
                float deltaZ = value - _origin.Z;

                // Transforming according to the change.
                Transform(Matrix.CreateTranslation(
                    new Vector3(0.0f, 0.0f, deltaZ)));

                // Setting the new value.
                _origin.Z = value;
            }
        }

        /// <inheritdoc />
        public float Width 
        { 
            get { return _width; } 
            set { _width = value; }
        }
        /// <inheritdoc />
        public float Height 
        { 
            get { return _height; }
            set { _height = value; }
        }
        /// <inheritdoc />
        public float Length 
        { 
            get { return _length; }
            set { _length = value; }
        }

        // Constructors:
        /// <summary>
        /// Constructs a Sphere at position 0, 0, 0 with a diameter of 1.
        /// </summary>
        /// <param name="graphics">Reference to the graphics object for rendering.</param>
        public Sphere(GraphicsDevice graphics)
        {
            _origin = Vector3.Zero;
            _radius = 0.5f;
            _diameter = _radius * 2.0f;

            _width = _diameter;
            _height = _diameter;
            _length = _diameter;

            _graphics = graphics;
            _renderer = new Renderer(graphics);
            SetSpheroidVertices(Color.CornflowerBlue);
        }
        
        /// <summary>
        /// Constructs a Sphere at position 0, 0, 0 with a dynamic radius.
        /// </summary>
        /// <param name="graphics">Reference to the graphics object for rendering.</param>
        /// <param name="radius">The radius of the sphere.</param>
        public Sphere(GraphicsDevice graphics, float radius)
        {
            _origin = Vector3.Zero;
            _radius = radius;
            _diameter = _radius * 2.0f;

            _width = _diameter;
            _height = _diameter;
            _length = _diameter;

            _graphics = graphics;
            _renderer = new Renderer(graphics);
            SetSpheroidVertices(Color.CornflowerBlue);
        }

        /// <summary>
        /// Constructs a Sphere with an inputted position and radius.
        /// </summary>
        /// <param name="graphics">Reference to the graphics object for rendering.</param>
        /// <param name="radius">Reference to the graphics object for rendering.</param>
        /// <param name="position">Vector3 position of the Sphere.</param>
        public Sphere(GraphicsDevice graphics, float radius, Vector3 position)
        {
            _origin = position;
            _radius = radius;
            _diameter = _radius * 2.0f;

            _width = _diameter;
            _height = _diameter;
            _length = _diameter;

            _graphics = graphics;
            _renderer = new Renderer(graphics);
            SetSpheroidVertices(Color.CornflowerBlue);
        }
        
        /// <summary>
        /// Constructs a Sphere with an inputted position and radius.
        /// </summary>
        /// <param name="graphics">Reference to the graphics object for rendering.</param>
        /// <param name="radius">Reference to the graphics object for rendering.</param>
        /// <param name="position">Vector3 position of the Sphere.</param>
        /// <param name="color">Defines the color of the Sphere.</param>
        public Sphere(GraphicsDevice graphics, float radius, Vector3 position, Color color)
        {
            _origin = position;
            _radius = radius;
            _diameter = _radius * 2.0f;

            _width = _diameter;
            _height = _diameter;
            _length = _diameter;

            _graphics = graphics;
            _renderer = new Renderer(graphics);
            SetSpheroidVertices(color);
        }

        // Methods:
        /// <summary>
        /// Creates all of the vertices for the Sphere in the world.
        /// </summary>
        /// <param name="color">The color of the sphere.</param>
        private void SetSpheroidVertices(Color color)
        {
            const int Subdivisions = 16;

            _renderer.SetSphereVertices(
                _origin,               
                color,                 
                Subdivisions,
                Subdivisions,                     
                _radius,               
                VertexType.FilledWireFrame); 
        }

        /// <summary>
        /// Transforms the sphere around the game world.
        /// </summary>
        /// <param name="transformation">Matrix transformation for the sphere.</param>
        public void Transform(Matrix transformation)
        {
            // Making sure that there are vertices.
            if (_renderer.Vertices is null)
            {
                return;
            }

            // Looping through and applying the translation to all vertices.
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
            _origin = Vector3.Transform(_origin, transformation);
        }

        /// <summary>
        /// Renders the Sphere to the game world.
        /// </summary>
        /// <param name="cam">Reference to the active camera used by the player.</param>
        /// <param name="color">Color to render the sphere with.</param>
        public void DrawSphere(Camera cam, Color color)
        {
            _renderer.Draw(cam, color);
        }

        /// <inheritdoc />
        public bool CheckCollision(ICollide a_iCollidable)
        {
            // If the collidable is a sphere:
            if (a_iCollidable is Sphere)
            {
                // Casting the collidable.
                Sphere other = (Sphere)a_iCollidable;

                // Finding the squared distance between the two origins.
                float distanceSqrd = 
                    (float)Math.Pow((_origin.X - other.X), 2) +
                    (float)Math.Pow((_origin.Y - other.Y), 2) +
                    (float)Math.Pow((_origin.Z - other.Z), 2);

                // Finding the sum of their radii.
                float sumOfRadii = other._radius + _radius;

                // Comparing the distances between and the sum of the radii.
                return distanceSqrd < Math.Pow(sumOfRadii, 2);
            }

            // If the collidable is a Cube:
            else if (a_iCollidable is Cube)
            {
                // Casting the collidable.
                Cube other = (Cube)a_iCollidable;
            }

            // If there is no case for the collidable, return false.
            return false;
        }

    }
}
