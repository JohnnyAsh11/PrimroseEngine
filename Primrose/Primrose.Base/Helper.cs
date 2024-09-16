using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Primrose.Base
{
    /// <summary>
    /// Contains all of my helper methods for either testing or general use.
    /// </summary>
    public static class Helper
    {

        // Fields:
        private static Dictionary<string, Texture2D> _gameTextures;
        private static SpriteFont _font;
        private static SpriteBatch _spriteBatch;

        // Properties:
        /// <summary>
        /// Get/Set property for the SpriteFont used to debug.
        /// </summary>
        public static SpriteFont Font
        {
            get { return _font; }
            set { _font = value; }
        }

        /// <summary>
        /// Get/Set property for the SpriteBatch used to debug draw.
        /// </summary>
        public static SpriteBatch SpriteBatch
        {
            get { return _spriteBatch; }
            set { _spriteBatch = value; }
        }

        // Methods:
        /// <summary>
        /// Alters the CullMode in the GraphicsDevice object.
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

        /// <summary>
        /// Renders a message to the game window
        /// </summary>
        /// <param name="message">Message being written to the screen.</param>
        /// <param name="location">The location that the message is being rendered.</param>
        /// <param name="spriteBatch">SpriteBatch reference to render the message.</param>
        public static void DrawString(string message, Vector2 location, Color tint)
        {
            if (_spriteBatch is null)
            {
                throw new Exception("Helper's SpriteBatch reference is null.");
            }

            _spriteBatch.Begin();

            _spriteBatch.DrawString(
                _font,
                message,
                location,
                tint);

            _spriteBatch.End();
        }

    }
}
