using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NDJPFinal.Source.Fonts;
using NDJPFinal.Source.Manager;
using NDJPFinal.Source.Sprites;
using System;
using System.Collections.Generic;

namespace NDJPFinal
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<Sprite> _sprites;
        private List<Font> _font;
        private ScrolllingBackground scrolllingBackground;


        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            var _backgroundTexture = this.Content.Load<Texture2D>("2d/background/SpaceSpriteSheet");
            var _shipTexture = this.Content.Load<Texture2D>("2d/ship-sprite-sheet (1)");
            var _rocketTexture = this.Content.Load<Texture2D>("2d/Wepon/Main ship weapon - Projectile - Rocket");
            var _bulletTexture = this.Content.Load<Texture2D>("2d/Wepon/Main ship weapon - Projectile - Auto cannon bullet");
            var _bossProjectileOne = this.Content.Load<Texture2D>("2d/wepon/Main ship weapon - Projectile - Big Space Gun");
            var _bossOneTexture = this.Content.Load<Texture2D>("2d/Boss/BossOne-Squid/Squid Full sheet orange (1)");
            var _healthBar = this.Content.Load<Texture2D>("2d/Hero/greenbar (3) (1)");
            var _healthBar2 = this.Content.Load<Texture2D>("2d/Hero/greenbar (2) (1)");
            var _regular = this.Content.Load<SpriteFont>("Font/RandomFont");
            var _deathAnimation = this.Content.Load<Texture2D>("explosion");
            var _blackRectangle = new Texture2D(GraphicsDevice, 1, 1);
            var _greenRectangle = new Texture2D(GraphicsDevice, 1, 1);
            _blackRectangle.SetData(new[] { Color.White });
            _greenRectangle.SetData(new[] { Color.White });

            var backGroundTexture = new ScrolllingBackground(_backgroundTexture, 0, new Vector2(0, 0));

            var hero = new Ship(_shipTexture, _deathAnimation, 0.2f, 5)
            {
                Position = new Vector2(0, 0),
                Bullet = new Bullet(_bulletTexture, 0.1f),
                Rocket = new Rocket(_rocketTexture, 0.1f),
            };
            var bossOne = new BossOne(_bossOneTexture, 0.2f, 4)
            {
                Position = new Vector2(400, 100),
                Bullet = new Bullet(_bossProjectileOne, 0.1f, 3.14159f),
                BossProjectileOne = new BossProjectileOne(_bossProjectileOne,0.1f)
            };
            var healthBar = new HealthBar(_healthBar, _healthBar2, 0.1f) 
            {
                Position = new Vector2(120,750)
            };
            var bossHealthBar = new BossOneHealthBar(_blackRectangle, _greenRectangle, backGroundTexture, 0.1f) {
                Position = new Vector2(0, 0),
            };


            var testFont = new TestFont(_regular,"test",0.1f) 
            {
                Position = new Vector2(10,10)
            };

            _sprites = new List<Sprite>()
            {
                backGroundTexture,
                hero,
                bossOne,
                healthBar,
                bossHealthBar
            };

            _font = new List<Font> 
            { testFont };

            BossOneCollisionManager bossOneCollisionManager = new BossOneCollisionManager(this, backGroundTexture ,bossOne, bossHealthBar, _sprites);
            ShipCollisionManager shipCollisionManager = new ShipCollisionManager(this, hero, backGroundTexture,healthBar,_sprites);
            this.Components.Add(bossOneCollisionManager);
            this.Components.Add(shipCollisionManager);
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var sprite in _sprites.ToArray())
                sprite.Update(gameTime, _sprites);
            foreach (var font in _font.ToArray())
                font.Update(gameTime, _font);
            PostUpdate();

            // TODO: Add your update logic here

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

        protected override void Draw(GameTime gameTime)
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();
            GraphicsDevice.Clear(Color.White);
            // TODO: Add your drawing code here
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);

            foreach (var sprite in _sprites)
                sprite.Draw(_spriteBatch);
            _spriteBatch.End();
          
            base.Draw(gameTime);
        }
    }

    public static class Program 
    {
        [STAThread]
        static void Main()
        {
            using var game = new NDJPFinal.Main();
            game.Run();

        }
    }

}