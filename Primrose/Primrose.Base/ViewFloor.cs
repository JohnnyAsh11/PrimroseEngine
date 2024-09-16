using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Primrose.GameCore;

namespace Primrose.Base
{
    /// <summary>
    /// Renders the development floor to the world space.
    /// </summary>
    public class ViewFloor
    {

        // Fields:
        private int _width;
        private int _height;
        private Renderer _renderer;
        private Color[] _floorColors = new Color[5] 
        { 
            Color.Red, 
            Color.Orange,
            Color.Yellow,
            Color.Green,
            Color.Blue
        };

        // Properties: - NONE -

        // Constructors:
        /// <summary>
        /// Constructs an instance of the ViewFloor object.
        /// </summary>
        /// <param name="graphics">Graphics device used for rendering the floor planes</param>
        /// <param name="width">Width of the ViewFloor</param>
        /// <param name="height">'Height' of the ViewFloor.  Functinonally acts as the Width.</param>
        public ViewFloor(GraphicsDevice graphics, int width, int height)
        {
            this._width = width;
            this._height = height;
            this._renderer = new Renderer(graphics);

            BuildFloorBuffer();
        }

        // Methods:
        /// <summary>
        /// Populates the renderer based off of the passed in offsets.
        /// </summary>
        /// <param name="xOffset">The X offset from 0.</param>
        /// <param name="zOffset">The Z offset from 0.</param>
        /// <param name="tileColor">The current color of the floor tiles.</param>
        private void CreateVertices(int xOffset, int zOffset, Color tileColor)
        {
            _renderer.Add(new Vector3(0 + xOffset, 0, 0 + zOffset), tileColor);
            _renderer.Add(new Vector3(1 + xOffset, 0, 0 + zOffset), tileColor);
            _renderer.Add(new Vector3(0 + xOffset, 0, 1 + zOffset), tileColor);
            _renderer.Add(new Vector3(1 + xOffset, 0, 0 + zOffset), tileColor);
            _renderer.Add(new Vector3(1 + xOffset, 0, 1 + zOffset), tileColor);
            _renderer.Add(new Vector3(0 + xOffset, 0, 1 + zOffset), tileColor);
        }
        
        /// <summary>
        /// Properly builds the floor VertexBuffer for future rendering.
        /// </summary>
        private void BuildFloorBuffer()
        {
            List<VertexPositionColor> vertexList = new List<VertexPositionColor>();
            int counter = 0;

            // First looping through the specified width.
            for (int x = 0; x < _width; x++)
            {
                // Increment the counter.
                counter++;

                // looping through the specified height amount per width iteration.
                for (int z = 0; z < _height; z++)
                {
                    counter++;

                    // Creating the vertices.
                    CreateVertices(x, z, _floorColors[counter % 5]);
                }
            }
        }
        
        /// <summary>
        /// Draws the floor on the screen.
        /// </summary>
        /// <param name="camera">Camera object for the world.</param>
        public void Draw(Camera camera)
        {
            _renderer.Draw(camera);
        }
    }
}
