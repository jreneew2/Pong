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

        private float maxVelocity = 200;
        private float maxAcceleration = 2000;

        public Ball(int spawnX, int spawnY)
        {
            ballPosition.X = spawnX;
            ballPosition.Y = spawnY;
            ballAcceleration.Y = 50f;
        }

        public override void LoadContent(ContentManager content, string textureName)
        {
            ballTexture = content.Load<Texture2D>(textureName);
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(ballTexture, ballPosition, null, Color.White, 0, new Vector2(ballTexture.Width / 2, ballTexture.Height / 2), 1, SpriteEffects.None, 1);
        }

        public void CalculatePosition(float deltaTime)
        {
            ballVelocity.X = ballVelocity.X + ballAcceleration.X * deltaTime;
            ballVelocity.Y = ballVelocity.Y + ballAcceleration.Y * deltaTime;

            ballPosition.X = ballPosition.X + ballVelocity.X * deltaTime;
            ballPosition.Y = ballPosition.Y + ballVelocity.Y * deltaTime;
        }
    }
}
