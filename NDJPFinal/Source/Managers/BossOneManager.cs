using Microsoft.Xna.Framework;
using NDJPFinal.Source.Global;
using NDJPFinal.Source.Scenes.Stages;
using NDJPFinal.Source.Sprites;
using NDJPFinal.Source.Sprites.Boss.BossOne;
using NDJPFinal.Source.Sprites.Hero;
using System;
using System.Collections.Generic;

namespace NDJPFinal.Source.Manager
{
    public class BossOneManager : GameComponent
    {
        private Bullet _bullet;
        private BossOne _bossOne;
        private ScrolllingBackground _scrolllingBackground;
        private BossOneHealthBar _bossOneHealthBar;
        Random random = new Random();
        private List<Sprite> _sprites;
        public float BossStatus = 1;
        public float speedOne;
        public float speedTwo;

        public BossOneManager(Game game, ScrolllingBackground scrolllingBackground, BossOne bossOne, BossOneHealthBar bossOneHealthBar, List<Sprite> sprite) : base(game)
        {
            this._scrolllingBackground = scrolllingBackground;
            this._bossOne = bossOne;
            this._sprites = sprite;
            this._bossOneHealthBar = bossOneHealthBar;
        }

        public override void Update(GameTime gameTime)
        {
            if (BossStatus <= 0.25)
            {
                _bossOne._speed = random.Next(6, 10);
                _bossOne.attackIntervel = 0.2f;
                speedOne = random.Next(7, 100);   
                speedTwo = random.Next(0, 10);
            }
            else if (BossStatus <= 0.50)
            {
                _bossOne.attackIntervel = 0.4f;
                _bossOne._speed = random.Next(3, 3);
                speedOne = random.Next(6, 7);
                speedTwo = random.Next(0, 10);
            }
            else if (BossStatus <= 0.75)
            {
                _bossOne. attackIntervel = 0.5f;
                _bossOne._speed = random.Next(2, 3);
                speedOne = random.Next(4, 7);
                speedTwo = random.Next(0, 10);
            }
            else
            {
                _bossOne.attackIntervel = 0.6f;
                _bossOne._speed = random.Next(1, 3);
                speedOne = random.Next(0, 7);
                speedTwo = random.Next(0, 10);
            }

            if (_bossOne.Position.X + _bossOne.TextureWidth / 2 < 0)
            {
                _bossOne._direction = new Vector2(speedOne, speedTwo);
            }
            if (_bossOne.Position.Y < 0)
            {
                _bossOne._direction = new Vector2(-speedOne, speedTwo);
            }
            if (_bossOne.Position.X + _bossOne.TextureWidth > _scrolllingBackground._spriteWidth)
            {
                _bossOne._direction = new Vector2(-speedOne, -speedTwo);
            }
            if (_bossOne.Position.Y + _bossOne.TextureHeight / 2 > _scrolllingBackground._spriteWidth / 2)
            {
                _bossOne._direction = new Vector2(speedOne, -speedTwo);
            }

            foreach (Sprite sprite in _sprites)
            {
                if (sprite is Bullet && sprite.Parent is Hero)
                {
                    Bullet bullet = (Bullet)sprite;

                    if (_bossOne.rectangle.Intersects(bullet.SpriteBoundry))
                    {
                        sprite.IsRemoved = true;
                        BattleReportStats.AmmoHits++;

                        if (_bossOneHealthBar.HealthBarStatus <= 0)
                        {
                            StageOneScene.GameResult = true;
                            BattleReportStats.MissionStatus = "SUCCESS";
                            _bossOne.IsRemoved = true;
                        }
                        _bossOneHealthBar.ChangeHealthBarState();
                        BossStatus = _bossOneHealthBar.HealthBarStatus;
                    }
                }
            }

            base.Update(gameTime);
        }
    }
}
