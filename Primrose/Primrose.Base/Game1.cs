using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Primrose.GameCore;
using Primrose.Base;
using System;

namespace Primrose
{
    /// <summary>
    /// Creates instances of the actual game itself.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private ViewFloor floor;
        private Camera camera;
        private Skybox skybox;

        private GameState gameState;

        private Asset knight;

        private KeyboardState prevKBState;

        private Cube cube;
        private Cube cube2;

        private Renderer circleRenderer;
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

            cube = new Cube(new Vector3(0, 1, 0), 1, 1, 1);
            cube2 = new Cube(new Vector3(20, 1, 10), 4, 4, 4);

            gameState = GameState.Update;

            circleRenderer = new Renderer(_graphics.GraphicsDevice);
            circleRenderer.SetCircleVertices(new Vector3(10, 0, 15), Color.CornflowerBlue, 8, 2);
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

            // Setting the debug Helper class.
            Helper.Font = Content.Load<SpriteFont>("Arial40");
            Helper.SpriteBatch = _spriteBatch;
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
                    if (kbState.IsKeyDown(Keys.E) && prevKBState.IsKeyUp(Keys.E))
                    {
                        gameState = GameState.Update;
                    }

                    break;
                case GameState.Update:

                    camera.Update(gameTime);

                    if (kbState.IsKeyDown(Keys.E) && prevKBState.IsKeyUp(Keys.E))
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

            // Rendering the knight 3D models.
            knight.Draw(camera.View, camera.Projection, camera.ThirdPersonPosition);

            //cube.DebugDraw(_graphics.GraphicsDevice, camera, Color.Purple);
            cube2.DebugDraw(_graphics.GraphicsDevice, camera, Color.BlueViolet);

            circleRenderer.Draw(camera);
            //knight.Draw(camera.View, camera.Projection, camera.Position);

            cube.DebugDraw(_graphics.GraphicsDevice, camera, Color.Purple);
            cube2.DebugDraw(_graphics.GraphicsDevice, camera, Color.BlueViolet);

            // Rendering the floor.
            floor.Draw(camera);
                Helper.DrawString("PAUSED", new Vector2(30, 30), Color.Black);
            }

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
