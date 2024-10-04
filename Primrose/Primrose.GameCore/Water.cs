using Microsoft.Xna.Framework;
using Primrose.Base;
using Primrose.Interface;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primrose.GameCore
{
    public class Water
    {

        // Fields:
        private Renderer _renderer;
        private Vector3 _vertex;
        private Noise _noise;

        // Properties: - NONE -

        // Constructors:
        public Water(GraphicsDevice graphics)
        {
            _renderer = new Renderer(graphics);
            _noise = new Noise();
            _vertex = new Vector3(2, 4, 0);

            _renderer.Add(new Vector3(1, 0, 0), Color.Blue);
            _renderer.Add(_vertex, Color.Blue);
            _renderer.Add(new Vector3(3, 0, 0), Color.Blue);
        }

        // Methods:
        public void Update(GameTime gameTime, ref double number)
        {
            number = (float)_noise.NextNoise();
            _vertex.Y = (float)number;

            _renderer.Clear();
            _renderer.Add(new Vector3(1, -2, 0), Color.Blue);
            _renderer.Add(_vertex, Color.Blue);
            _renderer.Add(new Vector3(3, -2, 0), Color.Blue);
        }

        public void Draw(Camera cam)
        {
            _renderer.Draw(cam);
        }

    }
}
