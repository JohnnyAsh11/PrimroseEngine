using Microsoft.Xna.Framework;

namespace Primrose.Interface
{
    public interface IPosition
    {
        public Vector3 Position { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}
