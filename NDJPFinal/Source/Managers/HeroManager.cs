using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NDJPFinal.Source.Sprites;
using NDJPFinal.Source.Sprites.Hero;
using NDJPFinal.Source.Sprites.Boss.BossOne;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using NDJPFinal.Source.Scenes.Stages;
using NDJPFinal.Source.Global;

namespace NDJPFinal.Source.Manager
{
    public class HeroManager : GameComponent
    {
        private Hero _ship;
        private ScrolllingBackground _scrollingBackground;
        private List<Sprite> _sprites;
        private HeroHealthBar _healthBar;
        private float _time;
        private SoundEffect _gettingHit;
        private SoundEffect _deathSound;

        public HeroManager(Game game, Hero ship, ScrolllingBackground scrolllingBackground,HeroHealthBar healthBar,List<Sprite>sprites) : base(game)
        {
            this._ship = ship;
            this._scrollingBackground = scrolllingBackground;
            this._sprites = sprites;
            this._healthBar = healthBar;
            _gettingHit = game.Content.Load<SoundEffect>("Sound/hurt_c_08-102842");
            _deathSound = game.Content.Load<SoundEffect>("Sound/retro-video-game-death-95730");
        }

        public override void Update(GameTime gameTime)
        {
            _ship.previousKey = _ship.currentKey;
            _ship.currentKey = Keyboard.GetState();

            if (_ship.currentKey.IsKeyDown(Keys.Left) && _ship.Position.X > 0)
            {
                _ship.Position.X -= _ship.LinearVelcitoy;
            }

            if (_ship.currentKey.IsKeyDown(Keys.Right) && _ship.Position.X + _ship.TextureWidth < _scrollingBackground._spriteWidth)
            {
                _ship.Position.X += _ship.LinearVelcitoy;
            }

            if (_ship.currentKey.IsKeyDown(Keys.Up) && _ship.Position.Y > 0)
            {
                _ship.Position.Y -= _ship.LinearVelcitoy;
            }

            if (_ship.currentKey.IsKeyDown(Keys.Down) && _ship.Position.Y + _ship.TextureHeight < _scrollingBackground._spriteHeight)
            {
                _ship.Position.Y += _ship.LinearVelcitoy;
            }

            foreach (Sprite sprite in _sprites)
            {
                if (sprite is BossProjectileOne && sprite.Parent is BossOne && (gameTime.TotalGameTime.TotalSeconds - _time) > 1)
                {
                    BossProjectileOne bullet = (BossProjectileOne)sprite;

                    if (_ship.SpriteBoundry.Intersects(bullet.rectangle) && !_ship.IsRemoved)
                    {
                        _healthBar.ChangeHealthBarState();
                        _ship.HeroStatus = _healthBar.HealthBarStatus;
                        BattleReportStats.HitsTaken++;

                        if (_healthBar.HealthBarStatus <= 0)
                        {
                            _deathSound.Play();
                            StageOneScene.GameResult = true;
                            BattleReportStats.MissionStatus = "FAILURE";
                            _healthBar.HealthBarStatus = 1;
                            break;
                        }
                        _gettingHit.Play();
                        bullet.IsRemoved= true;
                        _time = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                }

                if (sprite is BossOne && (gameTime.TotalGameTime.TotalSeconds - _time) > 1)
                {
                    BossOne bossOne = (BossOne)sprite;

                    if (_ship.SpriteBoundry.Intersects(bossOne.rectangle) && !_ship.IsRemoved)
                    {
                        _healthBar.ChangeHealthBarState();
                        _ship.HeroStatus = _healthBar.HealthBarStatus;
                        BattleReportStats.HitsTaken++;

                        if (_healthBar.HealthBarStatus <= 0)
                        {
                            _deathSound.Play();
                            StageOneScene.GameResult = true;
                            BattleReportStats.MissionStatus = "FAILURE";
                            _healthBar.HealthBarStatus = 1;
                            break;
                        }
                        _gettingHit.Play();
                        _time = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                }
            }

            base.Update(gameTime);
        }
    }
}
