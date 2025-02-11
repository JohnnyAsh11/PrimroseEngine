using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Primrose.Primrose.Base
{
    /// <summary>
    /// Handles the transformation logic for instances of objects in the world.
    /// </summary>
    public class Transform
    {

        // Fields:
        private Matrix m_m4WorldMatrix;
        private Vector3 m_v3Position;
        private float m_fScale;
        private Vector3 m_v3Rotation;
        private bool m_bIsDirty;

        // Properties:
        /// <summary>
        /// Gets the world matrix for the transform.
        /// </summary>
        public Matrix WorldMatrix
        {
            get 
            { 
                // Returning the clean data.
                if (!m_bIsDirty) return m_m4WorldMatrix;

                // Setting the dirty data variable to false.
                m_bIsDirty = false;

                // Calculating the individual matrix values.
                Matrix sc = Matrix.CreateScale(m_fScale);
                Matrix ro = Matrix.CreateFromYawPitchRoll(
                    m_v3Rotation.X, 
                    m_v3Rotation.Y, 
                    m_v3Rotation.Z);
                Matrix tr = Matrix.CreateTranslation(m_v3Position);

                // Creating the new world matrix.
                m_m4WorldMatrix = (sc * ro * tr);

                // Returning the now clean data.
                return m_m4WorldMatrix;
            }
        }
        
        /// <summary>
        /// Gets and sets the values for the Transform's position.
        /// </summary>
        public Vector3 Position
        {
            get { return m_v3Position; }
            set 
            {
                m_v3Position = value;
                m_bIsDirty = true;
            }
        }

        /// <summary>
        /// Gets and sets the values for the Transform's rotations.
        /// </summary>
        public float Scale
        {
            get { return m_fScale; }
            set 
            { 
                m_fScale = value;
                m_bIsDirty = true;
            }
        }

        /// <summary>
        /// Gets and sets the values for the Transform's rotations.
        /// </summary>
        public Vector3 Rotation
        {
            get { return m_v3Rotation; }
            set 
            { 
                m_v3Rotation = value;
                m_bIsDirty = true;
            }
        }

        // Constructors:
        /// <summary>
        /// Instantiates an instance of a Transform.
        /// </summary>
        public Transform()
        {
            m_v3Position = Vector3.Zero;
            m_fScale = 0.0f;
            m_v3Rotation = Vector3.Zero;
            m_bIsDirty = false;
        }

        // Methods: - NONE -
    }
}
