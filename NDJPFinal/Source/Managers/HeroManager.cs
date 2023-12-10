/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
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
        // Represents the player's ship or hero entity
        private Hero _ship;

        // Manages the scrolling background in the game scene
        private ScrolllingBackground _scrollingBackground;

        // A list holding various game sprites, such as enemies, projectiles, or other entities
        private List<Sprite> _sprites;

        // Represents the health bar associated with the player's ship
        private HeroHealthBar _healthBar;

        // Tracks time in the game scene
        private float _time;

        // Sound effect played when the player's ship gets hit or damaged
        private SoundEffect _gettingHit;

        // Sound effect played when the player's ship is destroyed
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
            // Update method responsible for handling ship movement and collision detection
            _ship.PreviousKey = _ship.CurrentKey;
            _ship.CurrentKey = Keyboard.GetState();

            // Handling ship movement based on keyboard input
            if (_ship.CurrentKey.IsKeyDown(Keys.Left) && _ship.Position.X > 0)
            {
                _ship.Position.X -= _ship.LinearVelocity;
            }

            if (_ship.CurrentKey.IsKeyDown(Keys.Right) && _ship.Position.X + _ship.TextureWidth < _scrollingBackground.SpriteWidth)
            {
                _ship.Position.X += _ship.LinearVelocity;
            }

            if (_ship.CurrentKey.IsKeyDown(Keys.Up) && _ship.Position.Y > 0)
            {
                _ship.Position.Y -= _ship.LinearVelocity;
            }

            if (_ship.CurrentKey.IsKeyDown(Keys.Down) && _ship.Position.Y + _ship.TextureHeight < _scrollingBackground.SpriteHeight)
            {
                _ship.Position.Y += _ship.LinearVelocity;
            }

            // Collision detection between ship and game sprites
            foreach (Sprite sprite in _sprites)
            {
                // Collision with projectiles fired by the boss
                if (sprite is BossProjectileOne && sprite.Parent is BossOne && (gameTime.TotalGameTime.TotalSeconds - _time) > 1)
                {
                    // Handle collision with the ship
                    BossProjectileOne bullet = (BossProjectileOne)sprite;

                    if (_ship.SpriteBoundry.Intersects(bullet.Collision) && !_ship.IsRemoved)
                    {
                        // Decrease ship health and track hits taken
                        _healthBar.ChangeHealthBarState();
                        _ship.HeroStatus = _healthBar.HealthBarStatus;
                        BattleReportStats.HitsTaken++;

                        // Check if ship's health is depleted
                        if (_healthBar.HealthBarStatus <= 0)
                        {
                            _deathSound.Play();
                            StageOneScene.GameResult = true;
                            BattleReportStats.MissionStatus = "FAILURE";
                            _healthBar.HealthBarStatus = 1;
                            break;
                        }

                        // Play sound effects and remove bullet
                        _gettingHit.Play();
                        bullet.IsRemoved = true;
                        _time = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                }

                // Collision with the boss entity itself
                if (sprite is BossOne && (gameTime.TotalGameTime.TotalSeconds - _time) > 1)
                {
                    // Handle collision with the boss entity
                    BossOne bossOne = (BossOne)sprite;

                    if (_ship.SpriteBoundry.Intersects(bossOne.Collision) && !_ship.IsRemoved)
                    {
                        // Decrease ship health and track hits taken
                        _healthBar.ChangeHealthBarState();
                        _ship.HeroStatus = _healthBar.HealthBarStatus;
                        BattleReportStats.HitsTaken++;

                        // Check if ship's health is depleted
                        if (_healthBar.HealthBarStatus <= 0)
                        {
                            _deathSound.Play();
                            StageOneScene.GameResult = true;
                            BattleReportStats.MissionStatus = "FAILURE";
                            _healthBar.HealthBarStatus = 1;
                            break;
                        }

                        // Play sound effects
                        _gettingHit.Play();
                        _time = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                }
            }

            base.Update(gameTime);
        }
    }
}
