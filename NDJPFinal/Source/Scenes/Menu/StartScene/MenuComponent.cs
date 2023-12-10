﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NDJPFinal.Source.Sprites;
using System.Collections.Generic;

namespace NDJPFinal.Source.Scenes.Menu.StartScene
{
    internal class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont regular, highlighted, randomFont;
        private Vector2 position;
        string[] items;
        private Color regularColor, highlightColor;
        private KeyboardState oldState;
        public int selectedIndex = 0;
        ScrolllingBackground scrolllingBackground;
        public Texture2D backgroundTextureTwo;
        public SoundEffect menuSelectSound;
        private List<Sprite> _sprites;

        internal MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont regular, SpriteFont highlighted,
            Vector2 position, string[] items, Color regularColor, Color highlightColor) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.regular = regular;
            this.highlighted = highlighted;
            this.position = position;
            this.items = items;
            this.regularColor = regularColor;
            this.highlightColor = highlightColor;
            var backgroundTexture = game.Content.Load<Texture2D>("2d/Background/SpaceSpriteSheet");
            menuSelectSound = game.Content.Load<SoundEffect>("Sound/Final Fantasy VII Sound Effects - Cursor Move (mp3cut.net)");
            backgroundTextureTwo = game.Content.Load<Texture2D>("2d/Background/Window_Header (1)");

            randomFont = game.Content.Load<SpriteFont>("Font/RandomFont");

            _sprites = new List<Sprite>();
            scrolllingBackground = new ScrolllingBackground(backgroundTexture, 0.1f, new Vector2(0, 0));
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                selectedIndex = selectedIndex + 1 == items.Length ? 0 : selectedIndex + 1;
                menuSelectSound.Play();
            }
            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                selectedIndex = selectedIndex == 0 ? items.Length - 1 : selectedIndex - 1;
                menuSelectSound.Play();
            }

            oldState = ks;

            scrolllingBackground.Update(gameTime, _sprites);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            scrolllingBackground.Draw(spriteBatch);
            spriteBatch.Draw(backgroundTextureTwo, new Vector2(150, 100), Color.White);

            spriteBatch.DrawString(randomFont, "Galactic", new Vector2(260, 425), Color.Black);
            spriteBatch.DrawString(randomFont, "Defender", new Vector2(250, 480), Color.Black);
            for (int i = 0; i < items.Length; i++)
            {
                if (i == selectedIndex)
                {
                    spriteBatch.DrawString(highlighted, items[i],
                    new Vector2(position.X, position.Y + highlighted.LineSpacing * i), highlightColor);
                }
                else
                {
                    spriteBatch.DrawString(regular, items[i],
                    new Vector2(position.X, position.Y + highlighted.LineSpacing * i), regularColor);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
