using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PongGame
{
    class Ball : GameObject
    {
        public Vector2 ballPosition;
        public Texture2D ballTexture;
        private Vector2 ballVelocity;
        private Vector2 ballAcceleration;
        private int windowSizeX;
        private int windowSizeY;
        public Rectangle boundingBox;
        Texture2D boundingBoxPixelData;

        public Ball(int spawnX, int spawnY, int WindowSizeX, int WindowSizeY) 
        {
            ballPosition.X = spawnX;
            ballPosition.Y = spawnY;
            ballVelocity.Y = 500f;
            ballVelocity.X = 300f;
            windowSizeX = WindowSizeX;
            windowSizeY = WindowSizeY;
            boundingBox = new Rectangle((int)ballPosition.X, (int)ballPosition.Y, 100, 100);
        }

        public override void LoadContent(ContentManager content, string textureName, GraphicsDevice device)
        {
            boundingBoxPixelData = new Texture2D(device, 1, 1, false, SurfaceFormat.Color);
            boundingBoxPixelData.SetData(new[] { Color.White });
            ballTexture = content.Load<Texture2D>(textureName);
        }

        public void UpdateBoundingBox()
        {
            boundingBox.X = (int)ballPosition.X - ballTexture.Width / 2;
            boundingBox.Y = (int)ballPosition.Y - ballTexture.Height / 2;
        }

        public override void Draw(SpriteBatch batch, bool drawBoundingBox)
        {
            if(drawBoundingBox)
            {
                DrawBorder(boundingBoxPixelData, batch, boundingBox, 2, Color.Blue);
            }
            batch.Draw(ballTexture, ballPosition, null, Color.White, 0, new Vector2(ballTexture.Width / 2, ballTexture.Height / 2), 1, SpriteEffects.None, 1);
        }

        public void CalculatePosition(float deltaTime)
        {
            UpdateBoundingBox();
            Console.WriteLine("BallTexture Bounds: " + boundingBox.ToString());
            if(boundingBox.Right >= windowSizeX || boundingBox.Left <= 0)
            {
                ballVelocity.X *= -1f;
            }
            if(boundingBox.Bottom >= windowSizeY || boundingBox.Top <= 0)
            {
                ballVelocity.Y *= -1f;
            }

            ballVelocity.X = ballVelocity.X + ballAcceleration.X * deltaTime;
            ballVelocity.Y = ballVelocity.Y + ballAcceleration.Y * deltaTime;

            ballPosition.X = ballPosition.X + ballVelocity.X * deltaTime;
            ballPosition.Y = ballPosition.Y + ballVelocity.Y * deltaTime;

        }
    }
}
