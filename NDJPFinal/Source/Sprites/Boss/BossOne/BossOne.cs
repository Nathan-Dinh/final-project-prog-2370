using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NDJPFinal.Source.Sprites.Hero;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NDJPFinal.Source.Sprites.Boss.BossOne
{
    internal class BossOne : Sprite
    {
        private Texture2D texture;

        public Vector2 _direction;

        public bool _direactionChange;

        private List<Rectangle> _spriteSheetFrames;

        private int _spriteFrameTracker;

        public Rectangle rectangle
        {
            get
            {
                return new Rectangle((int)Position.X - 5,
                    (int)Position.Y - 5,
                    TextureWidth/2,
                    TextureHeight/2);
            }
        }

        private float _time;
        private float _spriteTime;

        public float _speed;

        public Bullet Bullet;
        public BossProjectileOne BossProjectileOne;

        public int TextureWidth;

        public int TextureHeight;
        public bool reveseTrue = false;
        public bool ifHit = false;
        public float BossStatus = 1;


        Random random = new Random();
        public BossOne(Texture2D texture, float layer, int frames) : base(texture, layer)
        {
            this.texture = texture;
            _direction = new Vector2(3, 0);
            _speed = random.Next(0, 5);

            TextureWidth = texture.Width / frames;
            TextureHeight = texture.Height;

            _spriteSheetFrames = new List<Rectangle>();

            for (int x = 0; x < frames; x++)
            {
                _spriteSheetFrames.Add(new Rectangle(x * TextureWidth, 0, TextureWidth, TextureHeight));
            }
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {

            if (!ifHit)
            {
                if (BossStatus <= 0.75f)
                {
                    AddBullet(sprites);
                }
                else if (BossStatus <= 1)
                {
                    Position += _direction;

                    if (gametime.TotalGameTime.TotalSeconds - _time > 0.50)
                    {
                        AddBullet(sprites);
                        _time = (float)gametime.TotalGameTime.TotalSeconds;
                    }

                    if (gametime.TotalGameTime.TotalSeconds - _spriteTime > 0.4)
                    {
                        if (!reveseTrue && _spriteFrameTracker == _spriteSheetFrames.Count() - 1)
                        {
                            _spriteFrameTracker = 0;
                        }

                        _spriteFrameTracker++;
                        _spriteTime = (float)gametime.TotalGameTime.TotalSeconds;
                    }
                }
            }

            base.Update(gametime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, _spriteSheetFrames[_spriteFrameTracker], Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, _layer);
        }


        private void AddBullet(List<Sprite> sprites)
        {
            var bullet = BossProjectileOne.Clone() as BossProjectileOne;
            bullet.Position = Position + new Vector2(TextureWidth / 2, TextureHeight / 2);
            bullet.LinearVelcitoy = LinearVelcitoy * (random.Next(1, 3) * -1);
            bullet.LifeSpan = 2f;
            bullet.Parent = this;
            sprites.Add(bullet);
        }
    }
}
