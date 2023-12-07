using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDJPFinal.Source.Sprites.Hero
{
    internal class Bullet : Sprite
    {
        #region Properties
        private float _timer;
        private List<Rectangle> _projectileAnimation;
        private List<Rectangle> _currentAnimationFrames;
        private int _tracker;
        public int TextureWidth;
        public int TextureHeight;
        public Rectangle SpriteBoundry
        {
            get
            {
                return new Rectangle((int)Position.X - TextureWidth, (int)Position.Y - TextureHeight, 0, 0);
            }
        }
        #endregion

        public Bullet(Texture2D texture, float layer) : base(texture, layer)
        {
            TextureWidth = texture.Width / 4;
            TextureHeight = texture.Height;

            _currentAnimationFrames = new List<Rectangle>();
            _projectileAnimation = new List<Rectangle>();

            for (int x = 0; x < 4; x++)
            {
                _projectileAnimation.Add(new Rectangle(x * TextureWidth, 0, TextureWidth, TextureHeight));
            }

            _currentAnimationFrames = _projectileAnimation;
        }

        public Bullet(Texture2D texture, float layer, float rotation) : base(texture, layer)
        {
            TextureWidth = texture.Width / 4;
            TextureHeight = texture.Height;
            _rotation = rotation;

            _currentAnimationFrames = new List<Rectangle>();
            _projectileAnimation = new List<Rectangle>();

            for (int x = 0; x < 4; x++)
            {
                _projectileAnimation.Add(new Rectangle(x * TextureWidth, 0, TextureWidth, TextureHeight));
            }

            _currentAnimationFrames = _projectileAnimation;
        }


        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            _timer += (float)gametime.ElapsedGameTime.TotalSeconds;

            if (_tracker == 3)
            {
                _tracker = 0;
            }

            if (_timer > LifeSpan)
            {
                IsRemoved = true;
            }

            Position.Y -= LinearVelcitoy;

            if (_timer > 0.25)
            {
                _tracker += 1;
                _timer = 0;
            }
            base.Update(gametime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, _currentAnimationFrames[_tracker], Color.White, _rotation, new Vector2(0, 0), 1f, SpriteEffects.None, _layer);
        }

    }
}
