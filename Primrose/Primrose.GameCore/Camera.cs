
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Primrose.Interface;

namespace Primrose.GameCore
{
    public class Camera : IUpdate
    {

        // Fields:
        private GraphicsDevice _graphics;
        private Matrix _projection;
        private Vector3 _camPosition;
        private Vector3 _camRotation;
        private Vector3 _camLookAt;
        private Vector3 _mouseRotationBuffer;
        private MouseState _currentMouseState;
        private MouseState _previousMouseState;
        private float _cameraSpeed;

        // Properties:
        /// <summary>
        /// Get/Set for the position vector.  Modified set to update LookAt vector.
        /// </summary>
        public Vector3 Position
        {
            get { return _camPosition; }
            set
            {
                _camPosition = value;
                UpdateLookAt();
            }
        }

        /// <summary>
        /// Get/Set for the rotation vector.  Modified set to update LookAt vector.
        /// </summary>
        public Vector3 Rotation
        {
            get { return _camRotation; }
            set
            {
                _camRotation = value;
                UpdateLookAt();
            }
        }

        /// <summary>
        /// Allowing the Projection Matrix to be viewed publicly.
        /// </summary>
        public Matrix Projection
        {
            get { return _projection; }
        }

        /// <summary>
        /// Creates a view matrix based off of the current position and look vectors.
        /// </summary>
        public Matrix View
        { 
            get { return Matrix.CreateLookAt(_camPosition, _camLookAt, Vector3.Up); } 
        }

        // Constructors:
        /// <summary>
        /// Parameterized constructor for the Camera class.
        /// </summary>
        /// <param name="graphics">reference to the GraphicsDevice.</param>
        /// <param name="position">Starting position vector.</param>
        /// <param name="rotation">Starting rotation vector.</param>
        /// <param name="speed">Camera movement speed.</param>
        public Camera(GraphicsDevice graphics, Vector3 position, Vector3 rotation, float speed)
        {
            _cameraSpeed = speed;
            _graphics = graphics;

            // Setup projection marix.
            _projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                graphics.Viewport.AspectRatio,
                0.05f,
                1000.0f);

            // Setting the position and rotation vectors properly.
            MoveTo(position, rotation);

            // Setting the previous mouse state.
            _previousMouseState = Mouse.GetState();
        }

        // Methods:
        /// <summary>
        /// Simple Helper method.  Sets position and rotation vectors with their properties to also call UpdateLookAt.
        /// </summary>
        /// <param name="position">New position vector.</param>
        /// <param name="rotation">New rotation vector.</param>
        private void MoveTo(Vector3 position, Vector3 rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        /// <summary>
        /// Updates the _camLookAt vector3 based on current values.
        /// </summary>
        private void UpdateLookAt()
        {
            // Creating a rotation matrix for the updated position/rotation vectors.
            Matrix rotationMatrix = Matrix.CreateRotationX(_camRotation.X) *
                Matrix.CreateRotationY(_camRotation.Y);

            // Calculating the look offset.
            Vector3 lookAtOffset = Vector3.Transform(Vector3.UnitZ, rotationMatrix);

            // Finding the LookAt position based on the sum of both Vectors.
            _camLookAt = _camPosition + lookAtOffset;
        }

        /// <summary>
        /// Helper method. Creates the move vector for the camera based off of the new positional data.
        /// Can also be used for debug viewing of next positions.
        /// </summary>
        /// <param name="velocity">The speed and direction in which the camera is moving to.</param>
        /// <returns>The new position.</returns>
        private Vector3 PreviewMove(Vector3 velocity)
        {
            // Creting the rotation matrix based on the Y of the rotation vector.
            Matrix rotate = Matrix.CreateRotationY(_camRotation.Y);

            // Copying the velocity Vector.
            Vector3 movement = new Vector3(velocity.X, velocity.Y, velocity.Z);

            // Transforming the velocity by the rotation matrix.
            movement = Vector3.Transform(movement, rotate);

            // Adding the movement to the camera position.
            return _camPosition + movement;
        }

        /// <summary>
        /// Helper Method.  Actually moves the camera with the calculated values.
        /// </summary>
        /// <param name="velocity">The speed and direction in which the camera is moving to.</param>
        private void Move(Vector3 velocity)
        {
            MoveTo(PreviewMove(velocity), Rotation);
        }

        /// <summary>
        /// Logic update method for the camera class.
        /// </summary>
        /// <param name="gameTime">reference to the current GameTime object.</param>
        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState kbState = Keyboard.GetState();
            Vector3 direction = Vector3.Zero;
            Vector3 velocity = Vector3.Zero;
            float deltaX;
            float deltaY;

            // - - Movement transformations - -
            // Checking keyboard input.
            if (kbState.IsKeyDown(Keys.W))
            {
                direction.Z = 1;
            }
            if (kbState.IsKeyDown(Keys.S))
            {
                direction.Z = -1;
            }
            if (kbState.IsKeyDown(Keys.A))
            {
                direction.X = 1;
            }
            if (kbState.IsKeyDown(Keys.D))
            {
                direction.X = -1;
            }
            if (kbState.IsKeyDown(Keys.LeftShift))
            {
                direction.Y = -1;
            }
            if (kbState.IsKeyDown(Keys.Space))
            {
                direction.Y = 1;
            }

            // If the direction has changed in any way.
            if (direction != Vector3.Zero)
            {
                // Normalize the vector.
                direction = Vector3.Normalize(direction);

                // Calculate the velocity.
                velocity = direction * (dt * _cameraSpeed);

                // And move according to that velocity vector.
                Move(velocity);
            }

            // - - Mouse Transformations - -
            // Getting the current mouse state.
            _currentMouseState = Mouse.GetState();

            if (_currentMouseState != _previousMouseState)
            {
                // Calculating the change in mouse position.
                deltaX = _currentMouseState.X - (_graphics.Viewport.Width / 2);
                deltaY = _currentMouseState.Y - (_graphics.Viewport.Height / 2);

                // Calculating the rotation buffers.
                _mouseRotationBuffer.X -= 0.05f * deltaX * dt;
                _mouseRotationBuffer.Y -= 0.05f * deltaY * dt;
                
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
                Rotation = new Vector3(
                    -MathHelper.Clamp(_mouseRotationBuffer.Y, MathHelper.ToRadians(-75.0f), MathHelper.ToRadians(75.0f)),
                    MathHelper.WrapAngle(_mouseRotationBuffer.X),
                    0);
            }

            // Setting the mouse position to be static in the center of the screen.
            Mouse.SetPosition(_graphics.Viewport.Width / 2, _graphics.Viewport.Height / 2);

            // Setting the previous mouse state.
            _previousMouseState = _currentMouseState;
        }
    }
}
