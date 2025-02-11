using Microsoft.Xna.Framework;

namespace Primrose.Interface
{
    /// <summary>
    /// Defines methods for an object that has transformations.
    /// </summary>
    public interface ITransform
    {

        /// <summary>
        /// Transforms the ITransform object according to the matrix.
        /// </summary>
        /// <param name="transformation">The transformation matrix being applied to the object.</param>
        public void Transform(Matrix transformation);

    }
}
