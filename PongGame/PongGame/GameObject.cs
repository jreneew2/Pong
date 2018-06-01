using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongGame
{
    public abstract class GameObject
    {



        public abstract void LoadContent(ContentManager content, string textureName);
        public abstract void Draw(SpriteBatch batch);
    }
}
