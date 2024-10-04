using Microsoft.Xna.Framework;
using Primrose.Interface;

namespace Primrose.GameCore
{
    /// <summary>
    /// Abstract GameObject class that defines the logic for objects that exist in the world space.
    /// </summary>
    public abstract class GameObject : IGameObject
    {

        // Fields:
        protected ICollide _collider;
        protected Asset _asset;
        protected Vector3 _position;

        // Properties:
        /// <summary>
        /// Get/Set property for the position of the GameObject.
        /// </summary>
        public ICollide Collider
        {
            get { return _collider; }
            set { _collider = value; }
        }

        /// <summary>
        /// Get/Set for the Vector3 position.
        /// </summary>
        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        // Constructors:
        /// <summary>
        /// Constructs the base GameObject with a position and asset.
        /// </summary>
        /// <param name="collider">The collision detector for an instance of a GameObject.</param>
        /// <param name="asset">The rendering asset for an instance of a GameObject.</param>
        /// <param name="position">The position of the GameObject.</param>
        public GameObject(ICollide collider, Asset asset, Vector3 position)
        {
            _collider = collider;
            _asset = asset;
            _position = position;
        }

        /// <summary>
        /// Constructs the base GameObject with a position and asset.
        /// </summary>
        /// <param name="collider">The collision detector for an instance of a GameObject.</param>
        /// <param name="asset">The rendering asset for an instance of a GameObject.</param>
        public GameObject(ICollide collider, Asset asset)
        {
            _collider = collider;
            _asset = asset;
            _position = Vector3.Zero;
        }

        // Methods:
        /// <summary>
        /// Frame to frame update method for the GameObject class.
        /// </summary>
        /// <param name="gameTime">Reference to the GameTime object coming from Game1.</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Rendering method for the GameObject class.
        /// </summary>
        /// <param name="view">The camera's view matrix.</param>
        /// <param name="projection">The camera's projection matrix.</param>
        /// <param name="cameraPosition">The camera's current position.</param>
        public virtual void Draw(Matrix view, Matrix projection, Vector3 cameraPosition) { }

    }
}
