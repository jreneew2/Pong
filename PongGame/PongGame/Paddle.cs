using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PongGame
{
    class Paddle : GameObject
    {
        public Vector2 paddlePosition;
        public Texture2D paddleTexture;
        private Vector2 paddleVelocity;
        private int windowSizeX;
        private int windowSizeY;
        public Rectangle boundingBox;
        Texture2D boundingBoxPixelData;
        private float spriteScale;

        public Paddle(int spawnX, int spawnY, int WindowSizeX, int WindowSizeY, float scale=1)
        {
            paddlePosition.X = spawnX;
            paddlePosition.Y = spawnY;
            windowSizeX = WindowSizeX;
            windowSizeY = WindowSizeY;
            spriteScale = scale;
        }

        public override void LoadContent(ContentManager content, string textureName, GraphicsDevice device)
        {
            boundingBoxPixelData = new Texture2D(device, 1, 1, false, SurfaceFormat.Color);
            boundingBoxPixelData.SetData(new[] { Color.White });
            paddleTexture = content.Load<Texture2D>(textureName);
            boundingBox = new Rectangle((int)paddlePosition.X, (int)paddlePosition.Y, (int)(paddleTexture.Width * spriteScale), (int)(paddleTexture.Height * spriteScale));
            UpdateBoundingBox();
        }

        public void UpdateBoundingBox()
        {
            boundingBox.X = (int)paddlePosition.X - (int)(paddleTexture.Width * spriteScale) / 2;
            boundingBox.Y = (int)paddlePosition.Y - (int)(paddleTexture.Height * spriteScale) / 2;
        }

        public override void Draw(SpriteBatch batch, bool drawBoundingBox)
        {
            if (drawBoundingBox)
            {
                DrawBorder(boundingBoxPixelData, batch, boundingBox, 2, Color.Blue);
            }
            batch.Draw(paddleTexture, paddlePosition, null, Color.White, 0, new Vector2(paddleTexture.Width / 2, paddleTexture.Height / 2), spriteScale, SpriteEffects.None, 1);
        }

        public void CalculatePosition(float deltaTime, bool moveDown, bool moveUp, float speed)
        {
            //if not touching walls
            if (moveDown)
            {
                paddleVelocity.Y = speed;
            }
            else if (moveUp)
            {
                paddleVelocity.Y = -speed;
            }
            else
            {
                paddleVelocity.Y = 0;
            }

            //only let us move down or up depending on what side we are touching
            if (boundingBox.Bottom >= windowSizeY)
            {
                paddleVelocity.Y = 0;
                if(moveUp)
                {
                    paddleVelocity.Y = -speed;
                }
            }
            else if(boundingBox.Top <= 0)
            {
                paddleVelocity.Y = 0;
                if(moveDown)
                {
                    paddleVelocity.Y = speed;
                }
            }

            paddlePosition.Y = paddlePosition.Y + paddleVelocity.Y * deltaTime;

            UpdateBoundingBox();
        }
    }
}
