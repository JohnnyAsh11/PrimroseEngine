using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Primrose.Interface;

namespace Primrose.GameCore
{
    /// <summary>
    /// Wraps the a Model object with logic for textures and rendering.
    /// </summary>
    public class Asset : IRender
    {

        // Fields:
        private Model _content;
        private Texture2D _texture;

        // Properties: - NONE -

        // Constructors:
        /// <summary>
        /// Constructs the Asset object with a model and without a texture.
        /// </summary>
        /// <param name="content">The 3D model content being loaded in.x</param>
        public Asset(Model content)
        {
            _content = content;
            _texture = null!;
        }

        /// <summary>
        /// Constructs the Asset object with a Model and Texture
        /// </summary>
        /// <param name="content">The 3D model content being loaded in.</param>
        /// <param name="texture">The 3D model's texture mesh.</param>
        public Asset(Model content, Texture2D texture)
        {
            _content = content;
            _texture = texture;
        }

        // Methods:
        /// <summary>
        /// Renders the passed in model to the game window.
        /// </summary>
        /// <param name="view">The camera instance's view matrix.</param>
        /// <param name="projection">The camera instance's projection matrix.</param>
        /// <param name="cameraPosition">The camera instance's position.</param>
        public void Draw(Matrix view, Matrix projection, Vector3 cameraPosition)
        {
            if (_texture is null)
            {
                DrawModel(
                    _content,
                    Matrix.Identity,
                    view,
                    projection);
            }
            else
            {
                DrawModel(
                    _content,
                    _texture,
                    Matrix.Identity, 
                    view, 
                    projection);
            }
        }
        /// <summary>
        /// Renders the passed in model to the game window.
        /// </summary>
        /// <param name="view">The camera instance's view matrix.</param>
        /// <param name="projection">The camera instance's projection matrix.</param>
        /// <param name="cameraPosition">The camera instance's position.</param>
        /// <param name="rotationMatrix">The rotation matrix of the asset.</param>
        public void Draw(
            Matrix view,
            Matrix projection,
            Vector3 cameraPosition,
            Matrix rotationMatrix)
        {
            Matrix world = rotationMatrix;

            if (_texture is null)
            {
                DrawModel(
                    _content,
                    world,
                    view,
                    projection);
            }
            else
            {
                DrawModel(
                    _content,
                    _texture,
                    world, 
                    view, 
                    projection);
            }
        }

        /// <summary>
        /// Renders a 3D model without a texture.
        /// </summary>
        /// <param name="model">3D model being rendered to the screen.</param>
        /// <param name="world">World matrix - likely passing in Matrix.Identity.</param>
        /// <param name="view">The camera instance's view matrix.</param>
        /// <param name="projection">The camera instance's projection matrix.</param>
        private static void DrawModel(
            Model model, 
            Matrix world, 
            Matrix view, 
            Matrix projection)
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

        /// <summary>
        /// Renders a 3D model object with a passed in texture.
        /// </summary>
        /// <param name="model">Model object being rendered.</param>
        /// <param name="texture">Texture being applied to that object.</param>
        /// <param name="world">World matrix - likely passing in Matrix.Identity.</param>
        /// <param name="view">The camera instance's view matrix.</param>
        /// <param name="projection">The camera instance's projection matrix.</param>
        private static void DrawModel(
            Model model, 
            Texture2D texture, 
            Matrix world,
            Matrix view,
            Matrix projection)
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
