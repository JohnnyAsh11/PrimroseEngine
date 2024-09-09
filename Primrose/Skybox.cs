using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primrose.Content
{
    public class Skybox
    {

        // Fields:
        private Model _skybox;
        private TextureCube _skyboxTexture;
        private Effect _skyboxEffect;
        private float _size;

        // Constructors:
        /// <summary>
        /// Constructs the skybox for the world.
        /// </summary>
        /// <param name="skyboxTexture">Skybox texture filepath.</param>
        /// <param name="content">Content instance for the Game.</param>
        public Skybox(string skyboxTexture, ContentManager content)
        {
            _skybox = content.Load<Model>("Models/cube");
            _skyboxEffect = content.Load<Effect>("Shaders/Skybox");
            _skyboxTexture = content.Load<TextureCube>(skyboxTexture);
            _size = 50f;
        }

        // Methods:
        /// <summary>
        /// Actually draws the skybox in game.
        /// </summary>
        /// <param name="view">The view matrix for the effect.</param>
        /// <param name="projection">The projection matrix for the effect.</param>
        /// <param name="cameraPosition">The position of the camera.</param>
        public void Draw(Matrix view, Matrix projection, Vector3 cameraPosition)
        {
            // Go through each pass in the effect.  There should be only one.
            foreach (EffectPass pass in _skyboxEffect.CurrentTechnique.Passes)
            {
                // Draw all of the components of the mesh.
                foreach (ModelMesh mesh in _skybox.Meshes)
                {
                    // Assign the appropriate values to each of the parameters.
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = _skyboxEffect;
                        part.Effect.Parameters["World"].SetValue(
                            Matrix.CreateScale(_size) * Matrix.CreateTranslation(cameraPosition));
                        part.Effect.Parameters["View"].SetValue(view);
                        part.Effect.Parameters["Projection"].SetValue(projection);
                        part.Effect.Parameters["SkyBoxTexture"].SetValue(_skyboxTexture);
                        part.Effect.Parameters["CameraPosition"].SetValue(cameraPosition);
                    }

                    // Draw the mesh with the skybox effect.
                    mesh.Draw();
                }
            }
        }

    }
}
