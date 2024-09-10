using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Primrose.GameCore;
using Primrose.Base;
using Primrose.Interface;
using System;

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

        private Asset knight;

        private KeyboardState prevKBState;

        /// <summary>
        /// Default constructor for the Game.
        /// </summary>
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

        /// <summary>
        /// Initialization method for the game.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            camera = new Camera(_graphics.GraphicsDevice, new Vector3(10.0f, 1.0f, 5.0f), Vector3.Zero, 5.0f);
            floor = new ViewFloor(_graphics.GraphicsDevice, 20, 20);
            effect = new BasicEffect(_graphics.GraphicsDevice);

            gameState = GameState.Update;
        }

        /// <summary>
        /// Hub for loading assets and game content in.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            skybox = new Skybox("Textures/Skybox", Content);

            knight = new Asset(
                Content.Load<Model>("knight"),
                Content.Load<Texture2D>("knight_texture"));
        }

        /// <summary>
        /// Per frame logic updating method for the Game.
        /// </summary>
        /// <param name="gameTime">GameTime object reference.</param>
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

        /// <summary>
        /// Main render method for the Game.
        /// </summary>
        /// <param name="gameTime">GameTime object reference.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Rendering the skybox.
            Helper.ChangeCullMode(_graphics.GraphicsDevice, CullMode.CullClockwiseFace);
            skybox.Draw(camera.View, camera.Projection, camera.Position);
            Helper.ChangeCullMode(_graphics.GraphicsDevice, CullMode.CullCounterClockwiseFace);

            // Rendering the floor.
            floor.Draw(camera, effect);

            // Rendering the knight 3D model.s
            knight.Draw(camera.View, camera.Projection, camera.Position);            

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
