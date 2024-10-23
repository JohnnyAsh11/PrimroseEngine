namespace Primrose.Base
{
    /// <summary>
    /// Enumeration for tracking all of the states of the game.
    /// </summary>
    public enum GameState
    {
        Pause,
        Update,
        PermEnd
    }

    /// <summary>
    /// Dictates the order in which math should be done.
    /// </summary>
    public enum MathOrder
    {
        RotationFirst,
        TranslationFirst
    }

    /// <summary>
    /// Dictates what type of vertex data will be added into the vertex buffer.
    /// </summary>
    public enum VertexType
    {
        FilledWireFrame,
        Filled,
        WireFrame
    }
}
