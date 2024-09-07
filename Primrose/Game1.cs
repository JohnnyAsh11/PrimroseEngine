using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Primrose
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private ViewFloor floor;
        private Camera camera;
        private BasicEffect effect;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Allowing the user to resize the window and
            // subscribing the callback method for window resizing.
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;
        }

        protected override void Initialize()
        {
            base.Initialize();

            camera = new Camera(_graphics.GraphicsDevice, new Vector3(10.0f, 1.0f, 5.0f), Vector3.Zero, 5.0f);
            floor = new ViewFloor(_graphics.GraphicsDevice, 20, 20);
            effect = new BasicEffect(_graphics.GraphicsDevice);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            camera.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            floor.Draw(camera, effect);

            base.Draw(gameTime);
        }


        /// <summary>
        /// Callback method for when the window is resized
        /// </summary>
        private void OnResize(Object sender, EventArgs e)
        {
            //Applying changes to the viewport if the window has changed dimensions
            if ((_graphics.PreferredBackBufferWidth != _graphics.GraphicsDevice.Viewport.Width) ||
                (_graphics.PreferredBackBufferHeight != _graphics.GraphicsDevice.Viewport.Height))
            {
                _graphics.PreferredBackBufferWidth = _graphics.GraphicsDevice.Viewport.Width;
                _graphics.PreferredBackBufferHeight = _graphics.GraphicsDevice.Viewport.Height;
                _graphics.ApplyChanges();
            }
        }
    }
}
