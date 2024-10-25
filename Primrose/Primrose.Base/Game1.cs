using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Primrose.GameCore;
using Primrose.Base;
using System;
using System.Collections.Generic;
using System.Threading;

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
        private Skybox skybox;

        private GameState gameState;
        private KeyboardState prevKBState;

        private Player player;
        private Cube cube;
        private Sphere sphere1;
        private Sphere sphere2;
        private Color color1;
        private Color color2;

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

            floor = new ViewFloor(_graphics.GraphicsDevice, 20, 20);
            gameState = GameState.Update;
        }

        /// <summary>
        /// Hub for loading assets and game content in.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            skybox = new Skybox("Textures/Skybox", Content);

            player = new Player(
                new Cube(),
                new Asset(
                    Content.Load<Model>("knight"),
                    Content.Load<Texture2D>("knight_texture")),
                _graphics.GraphicsDevice,
                new Vector3(10f, 2.0f, 10f));

            cube = new Cube(new Vector3(0, 1, 0), 1.0f, 1.0f, 1.0f);

            color1 = Color.Green;
            color1 = Color.DarkOrange;
            sphere1 = new Sphere(
                _graphics.GraphicsDevice,
                1.5f,
                new Vector3(10, 2, 20),
                Color.DarkOrange);
            sphere2 = new Sphere(
                _graphics.GraphicsDevice,
                1.5f,
                new Vector3(15, 2, 20),
                Color.Green);

            // Setting the debug Helper class.
            Helper.Font = Content.Load<SpriteFont>("Arial40");
            Helper.SpriteBatch = _spriteBatch;
        }

        private bool done = false;

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

                    player.Update(gameTime);

                    Vector3 position = new Vector3(0.01f, 0.0f, 0.0f);
                    Matrix translation = Matrix.CreateTranslation(position);
                    sphere1.Translate(translation);

                    if (sphere1.CheckCollision(sphere2))
                    {
                        color1 = Color.Red;
                        color2 = Color.Red;
                    }
                    else
                    {
                        color1 = Color.Green;
                        color2 = Color.DarkOrange;
                    }

                    if (kbState.IsKeyDown(Keys.E) && prevKBState.IsKeyUp(Keys.E))
                    {
                        gameState = GameState.Pause;
                    }

                    break;
                case GameState.PermEnd:

                    // Empty case at the moment.

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
            skybox.Draw(player.Camera.View, player.Camera.Projection, player.Camera.Position);
            Helper.ChangeCullMode(_graphics.GraphicsDevice, CullMode.CullCounterClockwiseFace);

            cube.Draw(_graphics.GraphicsDevice, player.Camera, Color.Cyan, VertexType.FilledWireFrame);
            sphere1.DrawSphere(player.Camera, color2);
            sphere2.DrawSphere(player.Camera, color1);

            // Rendering the floor.
            floor.Draw(player.Camera);

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
