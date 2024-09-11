
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Primrose.GameCore;

namespace Primrose.Base
{
    public class ViewFloor
    {

        // Fields:
        private int width;
        private int height;
        private VertexBuffer floorBuffer;
        private GraphicsDevice graphics;
        private Color[] floorColors = new Color[5] 
        { 
            Color.Red, 
            Color.Orange,
            Color.Yellow,
            Color.Green,
            Color.Blue
        };

        // Properties:


        // Constructors:
        /// <summary>
        /// Constructs an instance of the ViewFloor object.
        /// </summary>
        /// <param name="graphics">Graphics device used for rendering the floor planes</param>
        /// <param name="width">Width of the ViewFloor</param>
        /// <param name="height">'Height' of the ViewFloor.  Functinonally acts as the Width.</param>
        public ViewFloor(GraphicsDevice graphics, int width, int height)
        {
            this.graphics = graphics;
            this.width = width;
            this.height = height;
            BuildFloorBuffer();
        }

        // Methods:
        /// <summary>
        /// Populates a List of VertexPositionColors based off of the passed in offsets.
        /// </summary>
        /// <param name="xOffset">The X offset from 0.</param>
        /// <param name="zOffset">The Z offset from 0.</param>
        /// <param name="tileColor">The current color of the floor tiles.</param>
        /// <returns>A list of VertexPositionColor data.</returns>
        private List<VertexPositionColor> FloorTile(int xOffset, int zOffset, Color tileColor)
        {
            List<VertexPositionColor> vertexList = new List<VertexPositionColor>();

            // Creating all necessary VertexPositionColor structs for this current offset.
            vertexList.Add(new VertexPositionColor(new Vector3(0 + xOffset, 0, 0 + zOffset), tileColor));
            vertexList.Add(new VertexPositionColor(new Vector3(1 + xOffset, 0, 0 + zOffset), tileColor));
            vertexList.Add(new VertexPositionColor(new Vector3(0 + xOffset, 0, 1 + zOffset), tileColor));
            vertexList.Add(new VertexPositionColor(new Vector3(1 + xOffset, 0, 0 + zOffset), tileColor));
            vertexList.Add(new VertexPositionColor(new Vector3(1 + xOffset, 0, 1 + zOffset), tileColor));
            vertexList.Add(new VertexPositionColor(new Vector3(0 + xOffset, 0, 1 + zOffset), tileColor));

            return vertexList;
        }
        
        /// <summary>
        /// Properly builds the floor VertexBuffer for future rendering.
        /// </summary>
        private void BuildFloorBuffer()
        {
            List<VertexPositionColor> vertexList = new List<VertexPositionColor>();
            int counter = 0;

            // First looping through the specified width.
            for (int x = 0; x < width; x++)
            {
                // Increment the counter.
                counter++;

                // looping through the specified height amount per width iteration.
                for (int z = 0; z < height; z++)
                {
                    counter++;

                    // Then looping through the returned VertexPositionColors
                    foreach (VertexPositionColor vertex in FloorTile(x, z, floorColors[counter % 5]))
                    {
                        // And adding them to the vertexList.
                        vertexList.Add(vertex);
                    }
                }
            }

            // Creating the actual VertexBuffer.
            floorBuffer = new VertexBuffer(
                graphics,
                VertexPositionColor.VertexDeclaration,
                vertexList.Count,
                BufferUsage.None);

            // Setting the data as an actual buffer array.
            floorBuffer.SetData<VertexPositionColor>(vertexList.ToArray());
        }
        
        /// <summary>
        /// Draws the floor on the screen.
        /// </summary>
        /// <param name="camera">Camera object for the world.</param>
        /// <param name="effect">Shader object used for the rendering process.</param>
        public void Draw(Camera camera, BasicEffect effect)
        {
            // Setting the necessary properties of the BasicEffect shader object.
            effect.VertexColorEnabled = true;
            effect.View = camera.View;
            effect.Projection = camera.Projection;
            effect.World = Matrix.Identity;

            // Actually Rendering the floor.
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                // Appling the shader
                pass.Apply();

                // Setting the VertexBuffer
                graphics.SetVertexBuffer(floorBuffer);

                // Rendering the Primitive triangles.
                graphics.DrawPrimitives(PrimitiveType.TriangleList, 0, floorBuffer.VertexCount / 3);
            }
        }
    }
}
