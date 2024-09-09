using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Primrose.Content;
using System;
using System.Diagnostics;

namespace Primrose
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private ViewFloor floor;
        private Camera camera;
        private Skybox skybox;
        private BasicEffect effect;
        private GameState gameState;

        private Model knight;
        private Texture2D knightTexture;

        private KeyboardState prevKBState;

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

            gameState = GameState.Update;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            skybox = new Skybox("Textures/Skybox", Content);

            knight = Content.Load<Model>("knight");
            knightTexture = Content.Load<Texture2D>("knight_texture");
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kbState = Keyboard.GetState();

            switch (gameState)
            {
                case GameState.Pause:

                    if (kbState.IsKeyDown(Keys.Escape) && prevKBState.IsKeyUp(Keys.Escape))
                    {
                        Exit();
                    }
                    if (kbState.IsKeyDown(Keys.Space) && prevKBState.IsKeyUp(Keys.Space))
                    {
                        gameState = GameState.Update;
                    }

                    break;
                case GameState.Update:

                    camera.Update(gameTime);

                    if (kbState.IsKeyDown(Keys.Space) && prevKBState.IsKeyUp(Keys.Space))
                    {
                        gameState = GameState.Pause;
                    }

                    break;
                case GameState.PermEnd:
                    break;
            }


            prevKBState = kbState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Globals.ChangeCullMode(_graphics.GraphicsDevice, CullMode.CullClockwiseFace);
            skybox.Draw(camera.View, camera.Projection, camera.Position);
            Globals.ChangeCullMode(_graphics.GraphicsDevice, CullMode.CullCounterClockwiseFace);

            floor.Draw(camera, effect);

            //knight.Draw(Matrix.Identity, camera.View, camera.Projection);
            Globals.DrawModel(knight, knightTexture, Matrix.Identity, camera.View, camera.Projection);            

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
