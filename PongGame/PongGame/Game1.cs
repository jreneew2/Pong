using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace PongGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D pixel;
        Ball ball;
        Paddle player1;
        Paddle player2;
        int paddleToWallDist = 100;
        float paddleSpeed = 700f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ball = new Ball(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, .25f);
            player1 = new Paddle(paddleToWallDist, GraphicsDevice.Viewport.Height / 2, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, .3f);
            player2 = new Paddle(GraphicsDevice.Viewport.Width - paddleToWallDist, GraphicsDevice.Viewport.Height / 2, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, .3f);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ball.LoadContent(this.Content, "BallTexture", GraphicsDevice);
            player1.LoadContent(this.Content, "PaddleTexture", GraphicsDevice);
            player2.LoadContent(this.Content, "PaddleTexture", GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
            float p1 = player1.paddlePosition.Y; //gets position of paddle
            float p2 = player2.paddlePosition.Y;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            ball.CalculatePosition(dt, p1, p2);
            player1.CalculatePosition(dt, Keyboard.GetState().IsKeyDown(Keys.S), Keyboard.GetState().IsKeyDown(Keys.W), paddleSpeed);
            player2.CalculatePosition(dt, Keyboard.GetState().IsKeyDown(Keys.Down), Keyboard.GetState().IsKeyDown(Keys.Up), paddleSpeed);
            base.Update(gameTime);
            //Console.WriteLine(player1.paddlePosition.Length());
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            ball.Draw(spriteBatch, false);
            player1.Draw(spriteBatch, false);
            player2.Draw(spriteBatch, false);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
