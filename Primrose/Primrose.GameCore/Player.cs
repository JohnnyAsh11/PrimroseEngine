using Microsoft.Xna.Framework;
using Primrose.Interface;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Primrose.GameCore
{
    /// <summary>
    /// 
    /// </summary>
    public class Player : GameObject, IPosition, IDimensional
    {

        // Fields:
        private const float _MovementSpeed = 5f;
        private Vector3 _direction;
        private Vector3 _velocity;

        private Camera _camera;

        private KeyboardState _kbState;
        private KeyboardState _prevKBState;

        // Properties:
        /// <summary>
        /// Get/Set property for x position.
        /// </summary>
        public float X
        {
            get { return _position.X; }
            set { _position.X = value; }
        }
        /// <summary>
        /// Get/Set property for y position.
        /// </summary>
        public float Y
        {
            get { return _position.Y; }
            set { _position.Y = value; }
        }
        /// <summary>
        /// Get/Set property for z position.
        /// </summary>
        public float Z
        {
            get { return _position.Z; }
            set { _position.Z = value; }
        }

        /// <summary>
        /// Get/Set property for width.
        /// </summary>
        public float Width
        {
            get { return _collider.Width; }
            set { _collider.Width = value; }
        }
        /// <summary>
        /// Get/Set property for length.
        /// </summary>
        public float Length
        {
            get { return _collider.Length; }
            set { _collider.Length = value; }
        }
        /// <summary>
        /// Get/Set property for height.
        /// </summary>
        public float Height
        {
            get { return _collider.Height; }
            set { _collider.Height = value; }
        }

        /// <summary>
        /// Get property for the camera connected to the player.
        /// </summary>
        public Camera Camera { get { return _camera; } }

        // Constructors:
        /// <summary>
        /// Parameterized constructor for the Player class.
        /// </summary>
        /// <param name="collider">The standard collider object for the player.</param>
        /// <param name="asset">The asset being used for the player</param>
        public Player(ICollide collider, Asset asset, GraphicsDevice graphics)
            : base(collider, asset, Vector3.Zero)
        {
            _camera = new Camera(graphics, Vector3.Zero, Vector3.Zero, 5.0f);
            _direction = Vector3.Zero;
        }
        /// <summary>
        /// Parameterized constructor for the Player class.
        /// </summary>
        /// <param name="collider">The standard collider object for the player.</param>
        /// <param name="asset">The asset being used for the player</param>
        /// <param name="position">The starting position of the player in the world.</param>
        public Player(ICollide collider, Asset asset, GraphicsDevice graphics, Vector3 position)
            : base(collider, asset, position)
        {
            _camera = new Camera(graphics, position, Vector3.Zero, 5.0f); 
            _direction = Vector3.Zero;
        }

        // Methods:
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _kbState = Keyboard.GetState();
            _direction = Vector3.Zero;

            if (_kbState.IsKeyDown(Keys.W))
            {
                _direction.Z = 1;
            }
            if (_kbState.IsKeyDown(Keys.A))
            {
                _direction.X = 1;
            }
            if (_kbState.IsKeyDown(Keys.S))
            {
                _direction.Z = -1;
            }
            if (_kbState.IsKeyDown(Keys.D))
            {
                _direction.X = -1;
            }

            // If the direction has changed in any way.
            if (_direction != Vector3.Zero)
            {
                // Normalize the vector.
                _direction = Vector3.Normalize(_direction);

                // Calculate the velocity.
                _velocity = _direction * (deltaTime * _MovementSpeed);

                // And move according to that velocity vector.
                _camera.Move(_velocity);
            }


            _camera.Update(gameTime);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Draw()
        {

        }
    }
}
