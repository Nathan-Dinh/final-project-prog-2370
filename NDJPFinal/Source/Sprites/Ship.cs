using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NDJPFinal.Source.Sprites
{
    internal class Ship : Sprite
    {
        #region Sprites
        public Texture2D _currentTexture;
        public Texture2D _texture;
        public Texture2D _deathAnimation;
        public Bullet Bullet;
        public Rocket Rocket;
        #endregion

        #region Properties
        private List<Rectangle> _spriteSheetFrames;
        private List<Rectangle> _deathAnimationFrames;
        private List<Rectangle> _currentFrames;
        public float shipStatus;
        private int _spriteFrameTracker;
        private int _weaponsAlternationTracker;
        public int TextureWidth;
        public int TextureHeight;
        public int TextureWidth2;
        public int TextureHeight2;
        public bool Death = false;
        public float _time;
        
        public Rectangle rectangle
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

        public Ship(Texture2D texture,Texture2D deathAnimation, float layer,int frames) : base(texture,layer)
        {
            //Sets the sprite width and height
            this.TextureWidth = texture.Width / frames;
            this.TextureHeight = texture.Height;

            this.TextureWidth2 = deathAnimation.Width / 5;
            this.TextureHeight2 = deathAnimation.Height/ 5;

            this.shipStatus = 1f;

            this._texture = texture;
            this._deathAnimation = deathAnimation;

            //Stores the sprite sheet frames
            _spriteSheetFrames = new List<Rectangle>();

            //Chops up the sprite sheet into frames
            for (int x = 0; x < frames; x++)
            {
                _spriteSheetFrames.Add(new Rectangle(x * TextureWidth , 0, TextureWidth, TextureHeight));
            }

            _deathAnimationFrames= new List<Rectangle>();

            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    _deathAnimationFrames.Add(new Rectangle(c * TextureWidth2, r * TextureHeight2, TextureWidth2, TextureHeight2));
                }
            }

            _currentFrames = _spriteSheetFrames;
            _currentTexture = _texture;
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            _time = (float)gametime.TotalGameTime.TotalSeconds;

            #region Inputes
            //Gets the the state of the keyboard and stores it
            previousKey = currentKey;
            currentKey = Keyboard.GetState();

            if (currentKey.IsKeyDown(Keys.Space) && (gametime.TotalGameTime.TotalSeconds - LastSpacebarPressTime) > 0.25)
            {
                AddBullet(sprites);
                //Resets the last time the space bar is pressed adding a deley on each press to action time
                LastSpacebarPressTime = (float)gametime.TotalGameTime.TotalSeconds;
                //Alternates in the AddBullet function what is sprite is created
                _weaponsAlternationTracker++;

            }
            #endregion

            //Loops throught the sprite sheet frames stored in _spriteFrameTracker
            if (shipStatus <= 0)
            {
                if (!Death)
                {
                    PlayDeathAnimation();
                    Death = true;
                }

                if (gametime.TotalGameTime.Seconds - _time < 4)
                {
                    if (_spriteFrameTracker >= _currentFrames.Count() - 1)
                    {
                        IsRemoved = true;
                    }
                    else
                    {
                        _spriteFrameTracker += 1;
                    }
                }
            }
            else if (_spriteFrameTracker != _currentFrames.Count() - 1)
            {
                _spriteFrameTracker = 0;
            }
            else
            {
                _spriteFrameTracker += 1;
            }
            

            base.Update(gametime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._currentTexture, this.Position, _currentFrames[_spriteFrameTracker], Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, this._layer);
        }

        private void PlayDeathAnimation()
        {
            _currentFrames = _deathAnimationFrames;
            _currentTexture = _deathAnimation;
        }

        private void AddBullet(List<Sprite> sprites)
        {
            var bullet = Bullet.Clone() as Bullet;
            bullet.Position = this.Position + new Vector2(6, 0);
            bullet.LinearVelcitoy = this.LinearVelcitoy * 2;
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
