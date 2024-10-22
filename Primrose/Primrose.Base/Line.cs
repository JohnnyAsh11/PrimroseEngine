using Primrose.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Net;

namespace Primrose.Base
{
    /// <summary>
    /// Creates a line in the world.
    /// </summary>
    public struct Line
    {

        // Fields:
        private Vector3 _start;
        private Vector3 _end;
        private BasicEffect _shader;
        private GraphicsDevice _graphics;

        // Properties:

        // Constructors:
        /// <summary>
        /// Default constructor for the Line struct.  Still needs a reference to the Graphics device.
        /// </summary>
        /// <param name="graphics">Reference to the graphics devices object.</param>
        public Line(GraphicsDevice graphics)
        {
            _graphics = graphics;
            _start = Vector3.Zero;
            _end = Vector3.Zero;
            _shader = new BasicEffect(graphics);

            _shader.View = Matrix.CreateLookAt(
                new Vector3(50, 50, 50), 
                new Vector3(25, 25, 25), 
                Vector3.Up);

            _shader.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45f), 
                graphics.Viewport.AspectRatio, 
                1f, 
                1000f);
        }

        /// <summary>
        /// Parameterized constructor for the Line struct.
        /// </summary>
        /// <param name="graphics">Reference to the graphics devices object.</param>
        /// <param name="start">Starting point of the Line.</param>
        /// <param name="end">End point of the Line.</param>
        public Line(GraphicsDevice graphics, Vector3 start, Vector3 end)
        {
            _graphics = graphics;
            _start = start;
            _end = end;
            _shader = new BasicEffect(graphics);

            _shader.View = Matrix.CreateLookAt(
                new Vector3(50, 50, 50),
                new Vector3(0, 0, 0),
                Vector3.Up);

            _shader.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45f),
                graphics.Viewport.AspectRatio,
                1f,
                1000f);
        }

        // Methods:
        /// <summary>
        /// Renders the line in the world.
        /// </summary>
        /// <param name="color">Color of the line drawn.</param>
        public void Draw(Color color)
        {
            // Creating the vertex buffer array.
            VertexPositionColor[] vertices = new VertexPositionColor[] 
            { 
                new VertexPositionColor(_start, color), 
                new VertexPositionColor(_end, color) 
            };

            // Using the graphics object to render to the world.
            _graphics.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);
        }

    }
}
