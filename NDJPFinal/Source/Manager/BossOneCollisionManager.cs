using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NDJPFinal.Source.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDJPFinal.Source.Manager
{
    internal class BossOneCollisionManager : GameComponent
    {
        private Bullet _bullet;
        private BossOne _bossOne;
        private ScrolllingBackground _scrolllingBackground;
        private BossOneHealthBar _bossOneHealthBar;
        Random random = new Random();
        private List<Sprite> _sprites;

        public BossOneCollisionManager(Game game, ScrolllingBackground scrolllingBackground,BossOne bossOne,BossOneHealthBar bossOneHealthBar, List<Sprite> sprite) : base(game)
        {
            this._scrolllingBackground = scrolllingBackground;
            this._bossOne = bossOne;
            this._sprites = sprite;
            this._bossOneHealthBar= bossOneHealthBar;
        }

        public override void Update(GameTime gameTime)
        {

            if (_bossOne.Position.X + _bossOne.TextureWidth/2 < 0)
            {
                _bossOne._speed = random.Next(0, 5);
                _bossOne._direction = new Vector2(_bossOne._speed, 0);
            }

            if (_bossOne.Position.Y < 0)
            {
                _bossOne._speed = random.Next(0, 5);
                _bossOne._speed = _bossOne._speed * -1;
                _bossOne._direction = new Vector2(_bossOne._speed, 2);
            }

            if (_bossOne.Position.X + _bossOne.TextureWidth > _scrolllingBackground._frameWidth)
            {
                _bossOne._speed = random.Next(0, 5);
                _bossOne._speed = _bossOne._speed * -1;
                _bossOne._direction = new Vector2(_bossOne._speed, 2);
            }

            if (_bossOne.Position.Y + _bossOne.TextureHeight / 2 > _scrolllingBackground._frameWidth/2)
            {
                _bossOne._speed = random.Next(0, 5);
                _bossOne._speed = _bossOne._speed * -1;
                _bossOne._direction = new Vector2(_bossOne._speed, -5);
            }

            foreach (Sprite sprite in _sprites)
            {
                if (sprite is Bullet && sprite.Parent is Ship)
                {
                    Bullet bullet = (Bullet)sprite;

                    if (_bossOne.rectangle.Intersects(bullet.rectangle))
                    {
                        sprite.IsRemoved = true;
                        
                        if (_bossOneHealthBar.HealthBarStatus <= 0)
                        {
                            _bossOne.IsRemoved = true;    
                        }
                        _bossOneHealthBar.ChangeHealthBarState();
                        _bossOne.BossStatus = _bossOneHealthBar.HealthBarStatus;
                    }
                }
            }

            base.Update(gameTime);
        }
    }
}
