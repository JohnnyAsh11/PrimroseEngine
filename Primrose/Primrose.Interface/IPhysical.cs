namespace Primrose.Interface
{
    /// <summary>
    /// an IUpdate object that also contains definitions for physics objects.
    /// </summary>
    public interface IPhysical : IUpdate
    {
        /// <summary>
        /// Calculates the steering force for the IPhysical object.
        /// </summary>
        public void CalcSteeringForces();

    }
}
