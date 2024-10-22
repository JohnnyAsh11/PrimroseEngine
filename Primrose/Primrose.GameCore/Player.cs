﻿using Microsoft.Xna.Framework;
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
    /// Controls whether or not the Camera is connected to an asset.
    /// </summary>
    public enum CameraState
    {
        Free,
        Attached
    }

    /// <summary>
    /// The controls hub for navigating within the simulation.
    /// </summary>
    public class Player : GameObject, IPosition, IDimensional
    {

        // Fields:
        private const float _MovementSpeed = 5f;
        private Vector3 _direction;
        private Vector3 _velocity;

        private GraphicsDevice _graphics;
        private Camera _camera;

        private CameraState _camState;
        private KeyboardState _kbState;
        private KeyboardState _prevKBState;
        private MouseState _mState;
        private MouseState _prevMState;

        private Vector3 _mouseRotationBuffer;

        // Properties:
        #region Properties
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
        #endregion

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
            _graphics = graphics;

            _camState = CameraState.Attached;
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
            _graphics = graphics;

            _camState = CameraState.Attached;
        }

        // Methods:
        /// <summary>
        /// Per frame logic update method for the Player class.
        /// </summary>
        /// <param name="gameTime">Current time in game.</param>
        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _kbState = Keyboard.GetState();
            _direction = Vector3.Zero;

            // Updating the camera positioning.
            UpdateKeyInput(deltaTime);
            UpdateMouseInput(deltaTime);
        }

        public CameraState CamState { get { return _camState; } }
        /// <summary>
        /// Updates logic based on key inputs.
        /// </summary>
        /// <param name="deltaTime">The change in time between frames.</param>
        private void UpdateKeyInput(float deltaTime)
        {
            // Disconnecting the player from the asset.
            if (_kbState.IsKeyDown(Keys.LeftShift))
            {
                _camState = CameraState.Free;
            }
            else
            {
                _camState = CameraState.Attached;
            }

            // WASD movement controls.
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

            if (_kbState.IsKeyDown(Keys.LeftShift))
            {
                _direction.Y = -1;
            }
            else if (_kbState.IsKeyDown(Keys.Space))
            {
                _direction.Y = 1;
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
        }

        /// <summary>
        /// Updates logic based on mouse inputs.
        /// </summary>
        /// <param name="deltaTime">The change in time between frames.</param>
        private void UpdateMouseInput(float deltaTime)
        {
            float deltaX;
            float deltaY;

            // Getting the current mouse state.
            _mState = Mouse.GetState();

            if (_mState != _prevMState)
            {
                // Calculating the change in mouse position.
                deltaX = _mState.X - (_graphics.Viewport.Width / 2);
                deltaY = _mState.Y - (_graphics.Viewport.Height / 2);

                // Calculating the rotation buffers.
                _mouseRotationBuffer.X -= 0.05f * deltaX * deltaTime;
                _mouseRotationBuffer.Y -= 0.05f * deltaY * deltaTime;

                // Clamping the rotation buffers to avoid crazy screen movement.
                if (_mouseRotationBuffer.Y < MathHelper.ToRadians(-75.0f))
                {
                    _mouseRotationBuffer.Y =
                        _mouseRotationBuffer.Y -
                        (_mouseRotationBuffer.Y - MathHelper.ToRadians(-75.0f));
                }
                if (_mouseRotationBuffer.Y > MathHelper.ToRadians(75.0f))
                {
                    _mouseRotationBuffer.Y =
                        _mouseRotationBuffer.Y -
                        (_mouseRotationBuffer.Y - MathHelper.ToRadians(75.0f));
                }

                // Calculating the new rotation vector based off of the calculated values.
                _camera.Rotation = new Vector3(
                    -MathHelper.Clamp(
                        _mouseRotationBuffer.Y,
                        MathHelper.ToRadians(-75.0f), 
                        MathHelper.ToRadians(75.0f)),
                    MathHelper.WrapAngle(_mouseRotationBuffer.X),
                    0);
            }

            // Setting the mouse position to be static in the center of the screen.
            Mouse.SetPosition(_graphics.Viewport.Width / 2, _graphics.Viewport.Height / 2);

            // Setting the previous mouse state.
            _prevMState = _mState;
        }

        /// <summary>
        /// Renders the asset one the world axis local to the Player.
        /// </summary>
        public void Draw()
        {
            Matrix world = Matrix.Identity;

            // Creating the proper world matrix for the asset.
            if (_camState == CameraState.Attached)
            {
                // Local to the player.
                world = Matrix.CreateWorld(_camera.Position, Vector3.Forward, Vector3.Up);
                
            }
            else if (_camState == CameraState.Free)
            {
                // According to the Player position.
                world = Matrix.CreateWorld(_position, Vector3.Backward, Vector3.Up);
            }

            // Rendering the asset.
            _asset.Draw(
                    _camera.View,
                    _camera.Projection,
                    _camera.Position,
                    world);
        }
    }
}
