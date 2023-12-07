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
    internal class ScrolllingBackground : Sprite
    {
        #region Texture
        private Texture2D _backgroundTexture;
        #endregion

        #region Properties
        private List<Rectangle> _currentAnimationFrames;
        private List<Rectangle> _backgroundAnimationFrames;

        private int _currentImage1;

        private int _currentImage2;

        private int _rows = 2;

        private int _columns = 3;

        public int _spriteWidth;

        public int _spriteHeight;

        private Vector2 _postion1, _postion2;

        private Vector2 _velocity;

        Random random = new Random();
        #endregion

        public ScrolllingBackground(Texture2D texture,float layer,Vector2 position) : base(texture,layer)
        {
            this._backgroundTexture = texture;
            this._velocity = new Vector2(0,2);
            this._currentImage1 = 0;
            this._currentImage2 = 1;
            var frameWidth = (texture.Width / 3);
            var frameHeight = (texture.Height / 2);
            this._spriteWidth= frameWidth;
            this._spriteHeight= frameHeight;

            _currentAnimationFrames = new List<Rectangle>();
            _backgroundAnimationFrames = new List<Rectangle>();

            for (int r = 0; r < _rows; r++)
            {
                for (int c = 0; c < _columns; c++)
                {
                    _backgroundAnimationFrames.Add(new Rectangle((c * frameWidth)  ,(r * frameHeight), frameWidth, frameHeight));
                }
            }

            _currentAnimationFrames = _backgroundAnimationFrames;

            this._postion1 = position;
            this._postion2 = new Vector2(_postion1.X,(_postion1.Y -_currentAnimationFrames[0].Height));
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            MoveBackground(gameTime);

            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_backgroundTexture, _postion1, _currentAnimationFrames[_currentImage2], Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(_backgroundTexture, _postion2, _currentAnimationFrames[_currentImage1], Color.White,0f,new Vector2(0,0), 1f, SpriteEffects.None, 0.0f);
        }

        private void MoveBackground(GameTime gameTime)
        {
            this._postion1 += _velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.05f;
            this._postion2 += _velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.05f;

            if (_postion1.Y > _currentAnimationFrames[0].Height)
            {
                _currentImage2 = random.Next(0, 5);
                _postion1.Y = _postion2.Y - _currentAnimationFrames[_currentImage2].Height;
            }

            if (_postion2.Y > _currentAnimationFrames[0].Height)
            {
                _currentImage1 = random.Next(0, 5);
                _postion2.Y = _postion1.X - _currentAnimationFrames[_currentImage1].Height;

            }
        }
    }
}
