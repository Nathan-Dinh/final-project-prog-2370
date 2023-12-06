using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NDJPFinal.Source.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDJPFinal.Source.Manager
{
    internal class ShipCollisionManager : GameComponent
    {
        private Ship _ship;
        private ScrolllingBackground _scrollingBackground;
        private List<Sprite> _sprites;
        private HealthBar _healthBar;
        private float _time;

        public ShipCollisionManager(Game game, Ship ship, ScrolllingBackground scrolllingBackground,HealthBar healthBar,List<Sprite>sprites) : base(game)
        {
            this._ship = ship;
            this._scrollingBackground = scrolllingBackground;
            this._sprites = sprites;
            this._healthBar = healthBar;
        }

        public override void Update(GameTime gameTime)
        {
            _ship.previousKey = _ship.currentKey;
            _ship.currentKey = Keyboard.GetState();

            if (_ship.currentKey.IsKeyDown(Keys.Left) && _ship.Position.X > 0)
            {
                _ship.Position.X -= _ship.LinearVelcitoy;
            }

            if (_ship.currentKey.IsKeyDown(Keys.Right) && _ship.Position.X + _ship.TextureWidth < _scrollingBackground._frameWidth)
            {
                _ship.Position.X += _ship.LinearVelcitoy;
            }

            if (_ship.currentKey.IsKeyDown(Keys.Up) && _ship.Position.Y > 0)
            {
                _ship.Position.Y -= _ship.LinearVelcitoy;
            }

            if (_ship.currentKey.IsKeyDown(Keys.Down) && _ship.Position.Y + _ship.TextureHeight < _scrollingBackground._frameHeight)
            {
                _ship.Position.Y += _ship.LinearVelcitoy;
            }

            foreach (Sprite sprite in _sprites)
            {
                if (sprite is BossProjectileOne && sprite.Parent is BossOne && (gameTime.TotalGameTime.TotalSeconds - _time) > 2)
                {
                    BossProjectileOne bullet = (BossProjectileOne)sprite;
                    if (_ship.rectangle.Intersects(bullet.rectangle))
                    {
                        _healthBar.ChangeHealthBarState();
                        _ship.shipStatus = _healthBar.HealthBarStatus;
                        if (_healthBar.HealthBarStatus <= 0)
                        {
                            //_ship.IsRemoved= true;
                        }
                        _time = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                }
            }

            base.Update(gameTime);
        }
    }
}
