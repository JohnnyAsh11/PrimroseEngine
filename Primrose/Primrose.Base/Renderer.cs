using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Primrose.GameCore;
using System;
using System.Collections.Generic;

namespace Primrose.Base
{
    public class Renderer
    {
        // Fields:
        private List<VertexPositionColor> _vertices;
        private BasicEffect _shader;
        private VertexBuffer _buffer;
        private GraphicsDevice _graphics;

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
        }

        // Methods:
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
            SetShader(cam);

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
        }

        /// <summary>
        /// Sets all of the shader's fields.
        /// </summary>
        /// <param name="cam">Camera to pull matrix data from.</param>
        private void SetShader(Camera cam)
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
    }
}
