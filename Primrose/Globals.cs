using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Primrose
{
    public static class Globals
    {

        // Fields:
        private static Dictionary<string, Texture2D> _gameTextures;

        // Properties:

        // Methods:
        /// <summary>
        /// Flips back and forth between CullModes in the reference to the GraphicsDevice object.
        /// </summary>
        /// <param name="graphics">Reference to the GraphicsDevice object.</param>
        /// <param name="cullMode">Cull Mode being changed to.</param>
        public static void ChangeCullMode(GraphicsDevice graphics, CullMode cullMode)
        {
            // Creating a new RasterizerState.
            RasterizerState rasterizerState = new RasterizerState();

            // Changing the state of the Rasterizer.
            rasterizerState.CullMode = cullMode;

            // Setting the new rasterizer state to the GraphicsDevice.
            graphics.RasterizerState = rasterizerState;
        }

    }
}
