using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Primrose.GameCore;
using System.Collections.Generic;
using System;
using Primrose.Interface;
using System.Security.Cryptography;

namespace Primrose.Base
{
    /// <summary>
    /// Creates a VertexBuffer and renders its vertices to the game window.
    /// </summary>
    public class Renderer
    {
        // Fields:
        private List<VertexPositionColor> _vertices;
        private BasicEffect _shader;
        private VertexBuffer _buffer;
        private GraphicsDevice _graphics;
        private List<Line> _lines;

        // Properties:
        /// <summary>
        /// Get property for the Vertex count.
        /// </summary>
        public int Count
        {
            get { return _vertices.Count; }
        }
        
        // Constructors:
        /// <summary>
        /// Default constructor for the Renderer class.
        /// </summary>
        public Renderer(GraphicsDevice graphics)
        {
            _vertices = new List<VertexPositionColor>();
            _graphics = graphics;
            _shader = new BasicEffect(_graphics);
            _lines = new List<Line>();
        }

        // Public Methods:
        /// <summary>
        /// Adds a Vertex Position Color to the Vertex list.
        /// </summary>
        /// <param name="position">position of the vertex.</param>
        /// <param name="color">color of the vertex.</param>
        public void Add(Vector3 position, Color color)
        {
            _vertices.Add(
                new VertexPositionColor(position, color));
        }

        /// <summary>
        /// Adds a triangle to the renderer's vertex buffer.
        /// </summary>
        /// <param name="p1">The first vertex position of the triangle</param>
        /// <param name="p2">The second vertex position of the triangle</param>
        /// <param name="p3">The third vertex position of the triangle</param>
        /// <param name="color">color of the vertex.</param>
        public void AddTriangle(
            Vector3 p1,
            Vector3 p2,
            Vector3 p3,
            Color color)
        {
            // Creating the triangle.
            _vertices.Add(
                new VertexPositionColor(p1, color));
            _vertices.Add(
                new VertexPositionColor(p2, color));
            _vertices.Add(
                new VertexPositionColor(p3, color));

            _lines.Add(new Line(_graphics, p1, p2));
            _lines.Add(new Line(_graphics, p2, p3));
            _lines.Add(new Line(_graphics, p3, p1));
        }

        /// <summary>
        /// Creates a quad with the 4 given vertices.
        /// </summary>
        /// <param name="p1">The first position of the vertex.</param>
        /// <param name="p2">The second position of the vertex.</param>
        /// <param name="p3">The third position of the vertex.</param>
        /// <param name="p4">The fourth position of the vertex.</param>
        /// <param name="color">The color of the quad.</param>
        public void AddQuad(
            Vector3 p1,
            Vector3 p2,
            Vector3 p3,
            Vector3 p4,
            Color color)
        {
            // First triangle.
            _vertices.Add(
                new VertexPositionColor(p1, color));
            _vertices.Add(
                new VertexPositionColor(p2, color));
            _vertices.Add(
                new VertexPositionColor(p4, color));

            // Second triangle.
            _vertices.Add(
                new VertexPositionColor(p2, color));
            _vertices.Add(
                new VertexPositionColor(p3, color));
            _vertices.Add(
                new VertexPositionColor(p4, color));

            _lines.Add(new Line(_graphics, p1, p2));
            _lines.Add(new Line(_graphics, p2, p3));
            _lines.Add(new Line(_graphics, p4, p3));
            _lines.Add(new Line(_graphics, p4, p1));
        }

        /// <summary>
        /// Clears out all 
        /// </summary>
        public void Clear()
        {
            _vertices.Clear();
        }

        /// <summary>
        /// Renders the vertex buffer to the screen.
        /// </summary>
        /// <param name="graphics">Graphics devices used to render to the window.</param>
        /// <param name="cam">Camera object used for matrices.</param>
        public void Draw(Camera cam)
        {
            // Initializing the Buffer object.
            InitBuffer(_graphics);

            // Setting the BasicEffect shader.
            SetShaderEffects(cam);

            // Actually Rendering the floor.
            foreach (EffectPass pass in _shader.CurrentTechnique.Passes)
            {
                // Appling the shader
                pass.Apply();

                // Setting the VertexBuffer
                _graphics.SetVertexBuffer(_buffer);

                // Rendering the Primitive triangles.
                _graphics.DrawPrimitives(PrimitiveType.TriangleList, 0, _buffer.VertexCount / 3);
            }

            // Rendering the lines.
            foreach (Line line in _lines)
            {
                line.Draw(Color.Magenta);
            }
        }

        // Private Methods:
        /// <summary>
        /// Sets all of the shader's fields.
        /// </summary>
        /// <param name="cam">Camera to pull matrix data from.</param>
        private void SetShaderEffects(Camera cam)
        {
            _shader.VertexColorEnabled = true;
            _shader.View = cam.View;
            _shader.Projection = cam.Projection;
            _shader.World = Matrix.Identity;
        }

        /// <summary>
        /// Creates the VertexBuffer object.
        /// </summary>
        /// <param name="graphics">GraphicsDevices used to create a VertexBuffer.</param>
        private void InitBuffer(GraphicsDevice graphics)
        {
            // Creating the actual VertexBuffer.
            _buffer = new VertexBuffer(
                graphics,
                VertexPositionColor.VertexDeclaration,
                _vertices.Count,
                BufferUsage.None);

            // Setting the data as an actual buffer array.
            _buffer.SetData<VertexPositionColor>(_vertices.ToArray());
        }

        /// <summary>
        /// Calculates and creates the vertices for a circle mesh.
        /// </summary>
        /// <param name="origin">Origin point of the circle.</param>
        /// <param name="color">Color of the circle being rendered.</param>
        /// <param name="subdivisions">The number of subdivisions for the circle</param>
        /// <param name="radius">Radius of the circle being rendered.</param>
        public void SetCircleVertices(Vector3 origin, Color color, int subdivisions, float radius)
        {
            // Clear the vertices before setting them.
            if (_vertices.Count > 0)
            {
                _vertices.Clear();
            }

            // Making sure that there is a valid number of subdivisions
            if (subdivisions < 4)
            {
                subdivisions = 4;
            }

            // Declaring some general use variables for the next part of the process.
            List<Vector3> tempVertices = new List<Vector3>();
            float deltaAngle = (2.0f * MathHelper.Pi) / subdivisions;
            float xCalc = 0;
            float yCalc = 0;

            // Looping through all of the subdivisions and calculating the circle locations.
            for (int i = 0; i < subdivisions; i++)
            {
                xCalc = (float)Math.Cos(deltaAngle * i) * radius;
                yCalc = (float)Math.Sin(deltaAngle * i) * radius;

                tempVertices.Add(new Vector3(
                        xCalc + origin.X, 
                        yCalc + origin.Y, 
                        0.00f + origin.Z));
            }

            // Adding all of the vertices in the proper order to the _vertices list.
            for (int i = 0; i < subdivisions; i++)
            {
                AddTriangle(
                    origin,
                    tempVertices[i],
                    tempVertices[(i + 1) % subdivisions],
                    color);
            }
        }
    }
}
