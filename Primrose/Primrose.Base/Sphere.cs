using Primrose.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using Primrose.GameCore;

namespace Primrose.Base
{
    /// <summary>
    /// Creates a Sphere in space that defines a position, dimensions and collidability.
    /// </summary>
    public struct Sphere : ICollide 
    {

        // Fields:
        private Vector3 _origin;
        private float _radius;
        private float _diameter;
        private Renderer _renderer;
        private GraphicsDevice _graphics;

        /// <summary>
        /// Defines 
        /// </summary>
        private float _width, _height, _length;

        // Properties:
        /// <inheritdoc />
        public Vector3 Position
        {
            get { return _origin; }
            set { _origin = value; }
        }
        /// <inheritdoc />
        public float X
        {
            get { return _origin.X; }
            set { _origin.X = value; }
        }
        /// <inheritdoc />
        public float Y
        {
            get { return _origin.Y; }
            set { _origin.Y = value; }
        }
        /// <inheritdoc />
        public float Z
        {
            get { return _origin.Z; }
            set { _origin.Z = value; }
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
        /// Constructs a Sphere with an inputed position and radius.
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
        /// Constructs a Sphere with an inputed position and radius.
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
        /// Translates the sphere around the game world.
        /// </summary>
        /// <param name="translation">Matrix translation for the sphere.</param>
        public void Translate(Matrix translation)
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
                Vector3 position = _renderer.Vertices[i].Position;
                Color color = _renderer.Vertices[i].Color;

                // Applying the transformation.
                position = Vector3.Transform(position, translation);

                // Setting the new vertex.
                _renderer.Vertices[i] = new VertexPositionColor(position, color);
            }

            if (_renderer.Lines is not null)
            {
                for (int i = 0; i < _renderer.Lines.Count; i++)
                {
                    // Setting a reference to the current line.
                    Line line = _renderer.Lines[i];

                    // Translating the line's start and end points.
                    Vector3 start = Vector3.Transform(line.Start, translation);
                    Vector3 end = Vector3.Transform(line.End, translation);

                    // Setting the start and end lines.
                    line.Start = start;
                    line.End = end;

                    // Sending it back to the list.
                    _renderer.Lines[i] = line;
                }
            }

            // Applying the translation to the Vector3 origin.
            _origin = Vector3.Transform(_origin, translation);
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
        public bool CheckCollision(ICollide collidable)
        {
            if (collidable is Sphere)
            {
                Sphere other = (Sphere)collidable;

                float distanceSqrd = 
                    (float)Math.Pow((_origin.X - other.X), 2) +
                    (float)Math.Pow((_origin.Y - other.Y), 2) +
                    (float)Math.Pow((_origin.Z - other.Z), 2);

                float sumOfRadii = other._radius + _radius;

                return distanceSqrd < Math.Pow(sumOfRadii, 2);
            }
            else if (collidable is Cube)
            {
                Cube other = (Cube)collidable;
            }

            return false;
        }

    }
}
