using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Primrose.Interface;

namespace Primrose.GameCore
{
    public abstract class PhysicsObject : GameObject, IPhysical
    {

        // Fields:
        protected Vector3 _totalForce;
        private Vector3 _maxForce;

        private float _mass;
        private float _maxSpeed;
        private Vector3 _acceleration;
        private Vector3 _velocity;
        private Vector3 _direction;

        // Properties:
        /// <summary>
        /// Get property for the _direction field.
        /// </summary>
        public Vector3 Direction
        {
            get { return _direction; }
        }

        // Constructors:
        #region Constructors
        /// <summary>
        /// Constructs an instance of a PhysicsObject with a collider, asset, starting position, max force, max speed and mass.
        /// </summary>
        /// <param name="collider">Collider for the PhysicsObject.</param>
        /// <param name="asset">Asset object for the PhysicsObject.</param>
        /// <param name="position">The Vector3 starting position of the PhysicsObject.</param>
        /// <param name="maxForce">The Vector3 max force that this PhysicsObject can have applied to it.</param>
        /// <param name="mass">The float mass of the PhysicsObject.</param>
        /// <param name="mass">The float mass of the PhysicsObject.</param>
        /// <param name="maxSpeed">The float max speed of the PhysicsObject.</param>
        public PhysicsObject(ICollid collider, Asset asset, Vector3 position, Vector3 maxForce, float mass, float maxSpeed)
            : base(collider, asset, position)
        {
            _totalForce = Vector3.Zero;
            _maxForce = maxForce;
            _mass = mass;
            _maxSpeed = maxSpeed;

            _acceleration = Vector3.Zero;
            _velocity = Vector3.Zero;
            _direction = Vector3.Zero;
        }

        /// <summary>
        /// Constructs an instance of a PhysicsObject with a collider, asset, starting position, max force and mass.
        /// </summary>
        /// <param name="collider">Collider for the PhysicsObject.</param>
        /// <param name="asset">Asset object for the PhysicsObject.</param>
        /// <param name="position">The Vector3 starting position of the PhysicsObject.</param>
        /// <param name="maxForce">The Vector3 max force that this PhysicsObject can have applied to it.</param>
        /// <param name="mass">The float mass of the PhysicsObject.</param>
        public PhysicsObject(ICollid collider, Asset asset, Vector3 position, Vector3 maxForce, float mass)
            : base(collider, asset, position)
        {
            _totalForce = Vector3.Zero;
            _maxForce = maxForce;
            _mass = mass;
            _maxSpeed = 15f;

            _acceleration = Vector3.Zero;
            _velocity = Vector3.Zero;
            _direction = Vector3.Zero;
        }

        /// <summary>
        /// Constructs an instance of a PhysicsObject with a collider, asset, starting position, and max force.
        /// </summary>
        /// <param name="collider">Collider for the PhysicsObject.</param>
        /// <param name="asset">Asset object for the PhysicsObject.</param>
        /// <param name="position">The Vector3 starting position of the PhysicsObject.</param>
        /// <param name="maxForce">The Vector3 max force that this PhysicsObject can have applied to it</param>
        public PhysicsObject(ICollid collider, Asset asset, Vector3 position, Vector3 maxForce)
            : base(collider, asset, position)
        {
            _totalForce = Vector3.Zero;
            _maxForce = maxForce;
            _mass = 10f;
            _maxSpeed = 15f;

            _acceleration = Vector3.Zero;
            _velocity = Vector3.Zero;
            _direction = Vector3.Zero;
        }

        /// <summary>
        /// Constructs an instance of a PhysicsObject with a collider, asset and starting position.
        /// </summary>
        /// <param name="collider">Collider for the PhysicsObject.</param>
        /// <param name="asset">Asset object for the PhysicsObject.</param>
        /// <param name="position">The Vector3 starting position of the PhysicsObject.</param>
        public PhysicsObject(ICollid collider, Asset asset, Vector3 position)
            : base(collider, asset, position)
        {
            _totalForce = Vector3.Zero;
            _maxForce = new Vector3(5.0f, 5.0f, 5.0f);
            _mass = 10f;
            _maxSpeed = 15f;

            _acceleration = Vector3.Zero;
            _velocity = Vector3.Zero;
            _direction = Vector3.Zero;
        }

        /// <summary>
        /// Constructs an instance of a PhysicsObject with only a collider and asset.
        /// </summary>
        /// <param name="collider">Collider for the PhysicsObject.</param>
        /// <param name="asset">Asset object for the PhysicsObject.</param>
        public PhysicsObject(ICollid collider, Asset asset)
            : base(collider, asset)
        {
            _totalForce = Vector3.Zero;
            _maxForce = new Vector3(5.0f, 5.0f, 5.0f);
            _mass = 10f;
            _maxSpeed = 15f;

            _acceleration = Vector3.Zero;
            _velocity = Vector3.Zero;
            _direction = Vector3.Zero;
        }
        #endregion

        // Methods:
        /// <summary>
        /// Calculates the total force for an instance of a PhysicsObject.
        /// </summary>
        public abstract void CalcSteeringForces();

        /// <summary>
        /// PhysicsObject's implementation of the Draw method.  Likely want to override in child classes.
        /// </summary>
        /// <param name="view">The camera instance's view matrix.</param>
        /// <param name="projection">The camera instances's projection matrix.</param>
        /// <param name="cameraPosition">The camera instance's position.</param>
        public override void Draw(Matrix view, Matrix projection, Vector3 cameraPosition)
        {
            _asset.Draw(view, projection, cameraPosition);
        }

        /// <summary>
        /// Per frame update method for the PhysicsObject.
        /// </summary>
        /// <param name="gameTime">GameTime reference likely from Game1's Update method.</param>
        public override void Update(GameTime gameTime)
        {
            // Zeroing out the force.
            _totalForce = Vector3.Zero;

            // Calculating the new totalForce.
            CalcSteeringForces();

            // Clamps the total force to a particular amount.
            _totalForce = Vector3.Clamp(_totalForce, Vector3.Zero, _maxForce);

            // Applying the total force to the acceleration.
            ApplyForce(_totalForce);
        }

        /// <summary>
        /// Applies a force to the acceleration of the PhysicsObject.
        /// </summary>
        /// <param name="force">The force being applied to the object.</param>
        private void ApplyForce(Vector3 force)
        {
            _acceleration = force / _mass;
        }

        #region Starter Physics Methods
        /// <summary>
        /// Calculates a Seek steering force to a target position
        /// </summary>
        /// <param name="targetPosition">Position at which the Agent is seeking</param>
        /// <returns>the correct force to travel to the target</returns>
        protected Vector3 Seek(Vector3 targetPosition)
        {
            Vector3 seekingForce;

            //calculating the desired velocity which will be straight to the target
            Vector3 desiredVelocity = targetPosition - _position;
            desiredVelocity = Vector3.Normalize(desiredVelocity) * _maxSpeed;

            //calculating the proper force required to smoothly travel to the desired velocity
            seekingForce = desiredVelocity - _velocity;

            return seekingForce;
        }

        /// <summary>
        /// Overload of Seek Method to use a general GameObject instead of a Vector3
        /// </summary>
        /// <param name="target">Target being seeked</param>
        /// <returns>a Seek force to the target</returns>
        protected Vector3 Seek(GameObject target)
        {
            return Seek(target.Position);
        }

        /// <summary>
        /// Calculates a Flee steering force away from a target position
        /// </summary>
        /// <param name="targetPosition">Position being fleed from</param>
        /// <returns>The correct force to travel away from the target position</returns>
        protected Vector3 Flee(Vector3 targetPosition)
        {
            Vector3 seekingForce;

            //calcualting the opposite desired velocity to that of seek
            Vector3 desiredVelocity = _position - targetPosition;
            desiredVelocity = Vector3.Normalize(desiredVelocity) * _maxSpeed;

            //calculating the proper force required to smoothly travel to the desired velocity
            seekingForce = desiredVelocity - _velocity;

            return seekingForce;
        }

        /// <summary>
        /// Overload of Flee Method to use a general GameObject instead of a Vector3
        /// </summary>
        /// <param name="target">Target being fleed from</param>
        /// <returns>a Flee force away from the target</returns>
        protected Vector3 Flee(GameObject target)
        {
            return Flee(target.Position);
        }
        #endregion
    }
}
