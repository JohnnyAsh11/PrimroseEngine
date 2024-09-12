
namespace Primrose.Interface
{
    public interface ICollid : IPosition
    {
        /// <summary>
        /// Checks a collision between two ICollid objects.
        /// </summary>
        /// <param name="collidable">The Collidable being checked against.</param>
        /// <returns>Whether or not a collision has occured.</returns>
        public bool CheckCollision(ICollid collidable);
    }
}
