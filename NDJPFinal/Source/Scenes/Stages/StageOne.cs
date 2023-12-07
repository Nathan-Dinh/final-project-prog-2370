using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NDJPFinal.Source.Fonts;
using NDJPFinal.Source.Manager;
using NDJPFinal.Source.Sprites;
using NDJPFinal.Source.Sprites.Hero;
using NDJPFinal.Source.Sprites.Boss.BossOne;
using System.Collections.Generic;


namespace NDJPFinal.Source.Scenes.Stages
{
    internal class StageOne : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private List<Sprite> _sprites;
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

            #region Initialization  
            var backGroundTexture = new ScrolllingBackground(backgroundTexture, 0, new Vector2(0, 0));

            var hero = new Hero(heroTexture, heroDeathSpriteSheet, 0.2f, 5, 5)
            {
                Position = new Vector2(400, 700),
                Bullet = new Bullet(bullterTexture, 0.1f),
                Rocket = new Rocket(rocketTexture, 0.1f),
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
            BossOneManager bossOneCollisionManager = new BossOneManager(game, backGroundTexture, bossOne, bossHealthBar, _sprites);
            HeroManager shipCollisionManager = new HeroManager(game, hero, backGroundTexture, healthBar, _sprites);

            game.Components.Add(bossOneCollisionManager);
            game.Components.Add(shipCollisionManager);
            #endregion
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var sprite in _sprites.ToArray())
                sprite.Update(gameTime, _sprites);
            PostUpdate();
            base.Update(gameTime);
        }

        private void PostUpdate()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);
            foreach (var sprite in _sprites)
                sprite.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
