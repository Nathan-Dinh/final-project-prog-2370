/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        private SoundEffect _soundEffect; // Sound effect variable 1

        private SoundEffect _soundEffect2; // Sound effect variable 2

        private BossOne _bossOne; // Instance of a BossOne class

        private ScrolllingBackground _scrolllingBackground; // Instance of a ScrolllingBackground class

        private BossOneHealthBar _bossOneHealthBar; // Instance of a BossOneHealthBar class

        private Random _random = new Random(); // Random number generator instance

        private List<Sprite> _sprites; // List to hold Sprite instances

        public float BossStatus = 1; // Variable to represent Boss status (initially set to 1)

        public float SpeedOne; // Variable for speed (not initialized)

        public float SpeedTwo; // Another variable for speed (not initialized)

        private bool _soundFlag = false; // Boolean flag variable initialized as false

        public BossOneManager(Game game, ScrolllingBackground scrolllingBackground, BossOne bossOne, BossOneHealthBar bossOneHealthBar, List<Sprite> sprite) : base(game)
        {
            this._scrolllingBackground = scrolllingBackground;
            this._bossOne = bossOne;
            this._sprites = sprite;
            this._bossOneHealthBar = bossOneHealthBar;
            _soundEffect = game.Content.Load<SoundEffect>("Sound/demonic-woman-scream-6333 (mp3cut.net)");
            _soundEffect2 = game.Content.Load<SoundEffect>("Sound/monster_hurt_c_08-102842 (mp3cut.net)");
        }

        public override void Update(GameTime gameTime)
        {
            if (BossStatus <= 0.30)
            {
                // Adjust boss properties when health is less than or equal to 30%
                _bossOne._speed = _random.Next(6, 10);
                _bossOne.AttackIntervel = 0.2f;
                SpeedOne = _random.Next(7, 100);
                SpeedTwo = _random.Next(0, 20);
            }
            else if (BossStatus <= 0.50)
            {
                // Adjust boss properties when health is less than or equal to 50%
                _bossOne.AttackIntervel = 0.4f;
                _bossOne._speed = _random.Next(3, 3); // <-- Appears to be a constant value; might need correction
                SpeedOne = _random.Next(8, 10);
                SpeedTwo = _random.Next(0, 16);

                // Check if sound flag is false and play the sound effect
                if (!_soundFlag)
                {
                    _soundEffect.Play();
                    _soundFlag = true;
                }
            }
            else if (BossStatus <= 0.80)
            {
                // Adjust boss properties when health is less than or equal to 80%
                _bossOne.AttackIntervel = 0.5f;
                _bossOne._speed = _random.Next(2, 3);
                SpeedOne = _random.Next(6, 7);
                SpeedTwo = _random.Next(0, 13);
            }
            else
            {
                // Adjust boss properties when health is above 80%
                _bossOne.AttackIntervel = 0.6f;
                _bossOne._speed = _random.Next(1, 3);
                SpeedOne = _random.Next(4, 7);
                SpeedTwo = _random.Next(0, 10);
            }

            // Adjust boss direction based on certain conditions and position
            if (_bossOne.Position.X + _bossOne.TextureWidth / 2 < 0)
            {
                _bossOne.Direction = new Vector2(SpeedOne, SpeedTwo);
            }
            if (_bossOne.Position.Y < 0)
            {
                _bossOne.Direction = new Vector2(-SpeedOne, SpeedTwo);
            }
            if (_bossOne.Position.X + _bossOne.TextureWidth > _scrolllingBackground.SpriteWidth)
            {
                _bossOne.Direction = new Vector2(-SpeedOne, -SpeedTwo);
            }
            if (_bossOne.Position.Y + _bossOne.TextureHeight / 2 > _scrolllingBackground.SpriteWidth / 2)
            {
                _bossOne.Direction = new Vector2(SpeedOne, -SpeedTwo);
            }

            // Check for collisions between boss and hero's bullets
            foreach (Sprite sprite in _sprites)
            {
                if (sprite is Bullet && sprite.Parent is Hero)
                {
                    Bullet bullet = (Bullet)sprite;

                    if (_bossOne.Collision.Intersects(bullet.SpriteBoundry))
                    {
                        sprite.IsRemoved = true;
                        BattleReportStats.AmmoHits++;

                        if (_bossOneHealthBar.HealthBarStatus <= 0)
                        {
                            // Update mission status and boss status when health bar is empty
                            StageOneScene.GameResult = true;
                            BattleReportStats.MissionStatus = "SUCCESS";
                            _bossOne.IsRemoved = true;
                        }
                        _soundEffect2.Play();
                        _bossOneHealthBar.ChangeHealthBarState();
                        BossStatus = _bossOneHealthBar.HealthBarStatus;
                    }
                }
            }

            base.Update(gameTime);
        }
    }
}
