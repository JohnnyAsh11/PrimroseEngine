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

        public static void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    // Setting the matrices.
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                }

                // Draw call.
                mesh.Draw();
            }
        }
        public static void DrawModel(Model model, Texture2D texture, Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    // Setting up textures.
                    effect.Texture = texture;
                    effect.TextureEnabled = true;

                    // Setting up lighting.
                    effect.LightingEnabled = true;
                    effect.DirectionalLight0.Enabled = true;
                    effect.DirectionalLight0.DiffuseColor = Vector3.One;
                    effect.DirectionalLight0.Direction = new Vector3(-1, -1, -1);
                    effect.AmbientLightColor = Vector3.One * 0.2f;

                    // Setting the matrices.
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                }

                // Draw call.
                mesh.Draw();
            }
        }

    }
}
