using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;


namespace NDJPFinal.Source.Sprites.Hero
{
    internal class Hero : Sprite
    {
        #region Sprites
        public Bullet Bullet;
        public Rocket Rocket;
        #endregion
        #region Animation
        private Texture2D _currentTexture;
        private Texture2D _heroTexture;
        private Texture2D _deathTexture;
        #endregion
        #region Properties
        private List<Rectangle> _heroAnimationFrames;
        private List<Rectangle> _deathAnimationFrames;
        private List<Rectangle> _currentAnimationFrames;
        private int _spriteFrameTracker;
        public float HeroStatus;
        //private int _weaponsAlternationTracker;
        private float _time;
        public int TextureWidth;
        public int TextureHeight;
        public int DeathAnimationTextureWidth;
        public int DeathAnimationTextureHeight;
        public bool IsDead = false;

        public Rectangle SpriteBoundry
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, TextureWidth, TextureHeight);
            }
        }
        #endregion

        #region Inputes
        float LastSpacebarPressTime;
        #endregion

        public Hero(Texture2D texture, Texture2D deathAnimation, float layer, int frames, int secondaryFrames) : base(texture, layer)
        {
            TextureWidth = texture.Width / frames;
            TextureHeight = texture.Height;

            DeathAnimationTextureWidth = deathAnimation.Width / secondaryFrames;
            DeathAnimationTextureHeight = deathAnimation.Height / secondaryFrames;

            HeroStatus = 1f;

            _heroTexture = texture;
            _deathTexture = deathAnimation;

            _heroAnimationFrames = new List<Rectangle>();

            for (int x = 0; x < frames; x++)
            {
                _heroAnimationFrames.Add(new Rectangle(x * TextureWidth, 0, TextureWidth, TextureHeight));
            }

            _deathAnimationFrames = new List<Rectangle>();

            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    _deathAnimationFrames.Add(new Rectangle(c * DeathAnimationTextureWidth, r * DeathAnimationTextureHeight, DeathAnimationTextureWidth, DeathAnimationTextureHeight));
                }
            }

            _currentAnimationFrames = _heroAnimationFrames;
            _currentTexture = _heroTexture;
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            _time = (float)gametime.TotalGameTime.TotalSeconds;

            previousKey = currentKey;
            currentKey = Keyboard.GetState();

            if (currentKey.IsKeyDown(Keys.Space) && gametime.TotalGameTime.TotalSeconds - LastSpacebarPressTime > 0.25)
            {
                AddBullet(sprites);
                LastSpacebarPressTime = (float)gametime.TotalGameTime.TotalSeconds;
            }

            AnimationTracker(gametime);

            base.Update(gametime, sprites);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_currentTexture, Position, _currentAnimationFrames[_spriteFrameTracker], Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, _layer);
        }

        private void AnimationTracker(GameTime gametime)
        {
            if (HeroStatus <= 0)
            {
                if (!IsDead)
                {
                    SetDeathAnimation();
                    IsDead = true;
                }

                if (gametime.TotalGameTime.Seconds - _time < 4)
                {
                    if (_spriteFrameTracker >= _currentAnimationFrames.Count() - 1)
                    {
                        IsRemoved = true;
                    }
                    else
                    {
                        _spriteFrameTracker += 1;
                    }
                }
            }
            else if (_spriteFrameTracker != _currentAnimationFrames.Count() - 1)
            {
                _spriteFrameTracker = 0;
            }
            else
            {
                _spriteFrameTracker += 1;
            }
        }

        private void SetDeathAnimation()
        {
            _currentAnimationFrames = _deathAnimationFrames;
            _currentTexture = _deathTexture;
        }

        private void AddBullet(List<Sprite> sprites)
        {
            var bullet = Bullet.Clone() as Bullet;
            bullet.Position = Position + new Vector2(6, 0);
            bullet.LinearVelcitoy = LinearVelcitoy * 2;
            bullet.LifeSpan = 2f;
            bullet.Parent = this;
            sprites.Add(bullet);

            /*var rocket = Rocket.Clone() as Rocket;
            rocket.Position = this.Position + new Vector2(TextureWidth / 2, 0);
            rocket.LinearVelcitoy = this.LinearVelcitoy * 2;
            rocket.LifeSpan = 2f;
            rocket.Parent = this;
            sprites.Add(rocket);

            rocket = Rocket.Clone() as Rocket;
            rocket.Position = this.Position;
            rocket.LinearVelcitoy = this.LinearVelcitoy * 2;
            rocket.LifeSpan = 2f;
            rocket.Parent = this;
            sprites.Add(rocket);*/

            /* if (_weaponsAlternationTracker == 3)
             {
                 var rocket = Rocket.Clone() as Rocket;
                 rocket.Position = this.Position + new Vector2(15, 0);
                 rocket.LinearVelcitoy = this.LinearVelcitoy * 2;
                 rocket.LifeSpan = 2f;
                 rocket.Parent = this;
                 sprites.Add(rocket);

                 _weaponsAlternationTracker = 0;
             }
             else
             {
                 var bullet = Bullet.Clone() as Bullet;
                 bullet.Position = this.Position + new Vector2(6,0);
                 bullet.LinearVelcitoy = this.LinearVelcitoy * 2;
                 bullet.LifeSpan = 2f;
                 bullet.Parent = this;
                 sprites.Add(bullet);
             }*/
        }
    }
}
