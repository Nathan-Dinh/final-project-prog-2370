using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDJPFinal.Source.Sprites
{
    internal class Bullet : Sprite
    {
        private float _timer;

        private List<Rectangle> _sourceRectangles = new List<Rectangle>();

        public int textureWidth;

        public int textureHeight;

        public int tracker;

        public Rectangle rectangle
        {
            get
            {
                return new Rectangle((int)Position.X - textureWidth, (int)Position.Y - textureHeight,0 , 0);
            }
        }

        public Bullet(Texture2D texture,float layer) : base(texture, layer)
        {
            this.textureWidth = texture.Width/4;
            this.textureHeight = texture.Height;
        

            for (int x = 0; x < 4; x++)
            {
                _sourceRectangles.Add(new Rectangle(x * textureWidth, 0, textureWidth, textureHeight));
            }
        }

        public Bullet(Texture2D texture, float layer, float rotation) : base(texture, layer)
        {
            this.textureWidth = texture.Width / 4;
            this.textureHeight = texture.Height;
            this._rotation = rotation;

            for (int x = 0; x < 4; x++)
            {
                _sourceRectangles.Add(new Rectangle(x * textureWidth, 0, textureWidth, textureHeight));
            }
        }


        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            _timer += (float)gametime.ElapsedGameTime.TotalSeconds;

            if (tracker == 3)
            {
                tracker = 0;
            }

            if (_timer > LifeSpan)
            {
                IsRemoved = true;
            }

            Position.Y -= LinearVelcitoy;

            if(_timer > 0.25)
            {
              tracker += 1;
              _timer = 0;
            }
            base.Update(gametime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._texture, this.Position, _sourceRectangles[tracker], Color.White, this._rotation, new Vector2(0, 0), 1f, SpriteEffects.None, this._layer);
        }

    }
}
