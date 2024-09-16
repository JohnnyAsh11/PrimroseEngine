namespace Primrose.Interface
{
    /// <summary>
    /// Contains definitions for an object with a position and dimensions that is also collidable.
    /// </summary>
    public interface ICollide : IPosition, IDimensional
    {
        /// <summary>
        /// Checks a collision between two ICollid objects.
        /// </summary>
        /// <param name="collidable">The Collidable being checked against.</param>
        /// <returns>Whether or not a collision has occured.</returns>
        public bool CheckCollision(ICollide collidable);
    }
}
