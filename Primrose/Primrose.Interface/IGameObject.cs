namespace Primrose.Interface
{
    /// <summary>
    /// Combines both IRender and IUpdate into one interface.
    /// </summary>
    public interface IGameObject : IRender, IUpdate
    {}
}
