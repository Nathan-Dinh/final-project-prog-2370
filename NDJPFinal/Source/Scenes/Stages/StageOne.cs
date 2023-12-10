/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using NDJPFinal.Source.Global;
using NDJPFinal.Source.Manager;
using NDJPFinal.Source.Sprites;
using NDJPFinal.Source.Sprites.Boss.BossOne;
using NDJPFinal.Source.Sprites.Hero;
using System.Collections.Generic;

namespace NDJPFinal.Source.Scenes.Stages
{
    public class StageOne : DrawableGameComponent
    {
        // Represents the drawing surface used to draw sprites
        private SpriteBatch _spriteBatch;

        // List to store the sprites
        private List<Sprite> _sprites;

        // Manages instances of the Hero character
        public HeroManager HeroManager;

        // Manages instances of the BossOne character
        public BossOneManager BossOneManager;
        public StageOne(Game game) : base(game)
        {
            #region Textures
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            var backgroundTexture = game.Content.Load<Texture2D>("2d/background/SpaceSpriteSheet");
            var bossOneTexture = game.Content.Load<Texture2D>("2d/Boss/BossOne-Squid/Squid Full sheet orange (1)");
            var bossProjectileOne = game.Content.Load<Texture2D>("2d/wepon/Main ship weapon - Projectile - Big Space Gun");
            var bossOneHealthBarLayerOne = new Texture2D(GraphicsDevice, 1, 1);
            var bossOneHealthBarLayerTwo = new Texture2D(GraphicsDevice, 1, 1);
            bossOneHealthBarLayerOne.SetData(new[] { Color.White });
            bossOneHealthBarLayerTwo.SetData(new[] { Color.White });

            var heroTexture = game.Content.Load<Texture2D>("2d/ship-sprite-sheet (1)");
            var heroHealthBarLayerOne = game.Content.Load<Texture2D>("2d/Hero/greenbar (3) (1)");
            var heroHealthBarLayerTwo = game.Content.Load<Texture2D>("2d/Hero/greenbar (2) (1)");
            var heroDeathSpriteSheet = game.Content.Load<Texture2D>("explosion");
            var rocketTexture = game.Content.Load<Texture2D>("2d/Wepon/Main ship weapon - Projectile - Rocket");
            var bullterTexture = game.Content.Load<Texture2D>("2d/Wepon/Main ship weapon - Projectile - Auto cannon bullet");
            #endregion

            var damageSoundEffect = game.Content.Load<SoundEffect>("Sound/zap-testground");

            #region Initialization  
            var backGroundTexture = new ScrolllingBackground(backgroundTexture, 0, new Vector2(0, 0));

            var hero = new Hero(heroTexture, heroDeathSpriteSheet, 0.2f, 5, 5)
            {
                Position = new Vector2(400, 700),
                Bullet = new Bullet(bullterTexture, 0.1f),
                Rocket = new Rocket(rocketTexture, 0.1f),
                DamageSoundEffect = damageSoundEffect
            };
            var bossOne = new BossOne(bossOneTexture, 0.2f, 4)
            {
                Position = new Vector2(400, 100),
                Bullet = new Bullet(bossProjectileOne, 0.1f, 3.14159f),
                BossProjectileOne = new BossProjectileOne(bossProjectileOne, 0.1f)
            };
            var healthBar = new HeroHealthBar(heroHealthBarLayerOne, heroHealthBarLayerTwo, 0.1f)
            {
                Position = new Vector2(120, 750)
            };
            var bossHealthBar = new BossOneHealthBar(bossOneHealthBarLayerOne, bossOneHealthBarLayerTwo, backGroundTexture, 0.1f)
            {
                Position = new Vector2(0, 0),
            };

            _sprites = new List<Sprite>()
            {
                backGroundTexture,
                hero,
                bossOne,
                healthBar,
                bossHealthBar
            };
            #endregion

            #region Managers
            var heroManager = new BossOneManager(game, backGroundTexture, bossOne, bossHealthBar, _sprites);
            var bossOneManager = new HeroManager(game, hero, backGroundTexture, healthBar, _sprites);

            game.Components.Add(heroManager);
            game.Components.Add(bossOneManager);
            #endregion
        }

        public override void Update(GameTime gameTime)
        {
            // Iterate through each sprite in the _sprites list and update them
            foreach (var sprite in _sprites.ToArray())
                sprite.Update(gameTime, _sprites);

            // Perform any post-update actions or additional processing related to sprites
            PostUpdate();

            // Update the BattleReportStats, possibly to track game-related statistics or data
            BattleReportStats.UpdateDateTime();

            // Call the base class's Update method to handle any other necessary updates
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            // Begins a new batch of sprite drawing with a specific sorting mode (FrontToBack)
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);

            // Iterates through each sprite in the _sprites list and draws them using the _spriteBatch
            foreach (var sprite in _sprites)
                sprite.Draw(_spriteBatch);

            // Ends the current sprite batch, finalizing the drawing of sprites
            _spriteBatch.End();

            // Calls the base class's Draw method to handle any other necessary drawing operations
            base.Draw(gameTime);
        }

        private void PostUpdate()
        {
            // Iterate through the _sprites list using a for loop
            for (int i = 0; i < _sprites.Count; i++)
            {
                // Check if the IsRemoved property of the current sprite at index 'i' is true
                if (_sprites[i].IsRemoved)
                {
                    // If IsRemoved is true for the sprite at index 'i':

                    // Remove the sprite from the _sprites list at index 'i'
                    _sprites.RemoveAt(i);

                    // Decrement 'i' by 1 to account for the removal of the sprite
                    i--;
                }
            }
        }
    }
}
