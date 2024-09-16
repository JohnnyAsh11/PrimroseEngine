namespace Primrose.Interface
{
    /// <summary>
    /// Contains properties for the dimensions of a 3D object.
    /// </summary>
    public interface IDimensional
    {
        /// <summary>
        /// Tracks the width of an object with dimensions.
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// Tracks the length of an object with dimensions.
        /// </summary>
        public float Length { get; set; }

        /// <summary>
        /// Tracks the length of an object with dimensions.
        /// </summary>
        public float Height { get; set; }
    }
}
