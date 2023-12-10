using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDJPFinal.Source.Sprites.Boss.BossOne
{
    public class BossProjectileOne : Sprite
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
                return new Rectangle((int)Position.X, (int)Position.Y, 0, 0);
            }
        }

        public BossProjectileOne(Texture2D texture, float layer) : base(texture, layer)
        {
            textureWidth = texture.Width / 10;
            textureHeight = texture.Height;

            for (int i = 0; i < 10; i++)
            {
                _sourceRectangles.Add(new Rectangle(textureWidth * i, 0, textureWidth, textureHeight));
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

            if (_timer > 0.25)
            {
                tracker += 1;
                _timer = 0;
            }

            base.Update(gametime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, _sourceRectangles[tracker], Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, _layer);
        }
    }
}
