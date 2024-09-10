﻿using Microsoft.Xna.Framework;

namespace Primrose.Interface
{
    public interface IUpdate
    {
        /// <summary>
        /// Frame to frame logic update method for updatable objects.
        /// </summary>
        /// <param name="gametime">GameTime likely coming from Game1's reference.</param>
        public void Update(GameTime gametime);
    }
}