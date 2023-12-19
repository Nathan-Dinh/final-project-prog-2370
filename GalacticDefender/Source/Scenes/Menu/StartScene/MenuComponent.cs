/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NDJPFinal.Source.Sprites;
using System.Collections.Generic;

namespace NDJPFinal.Source.Scenes.Menu.StartScene
{
    internal class MenuComponent : DrawableGameComponent
    {
        // Represents the rendering of 2D sprites
        private SpriteBatch _spriteBatch;

        // Represents the fonts used for displaying text
        private SpriteFont _regular, _highlighted, _randomFont;

        // Represents the position vector for displaying the menu
        private Vector2 _position;

        // An array of strings representing menu items
        private string[] _items;

        // Colors for the regular and highlighted states of the menu items
        private Color _regularColor, _highlightColor;

        // Represents the previous keyboard state to track changes
        private KeyboardState _oldState;

        // Index representing the currently selected menu item
        public int SelectedIndex;

        // Instance of ScrolllingBackground, possibly for the game's background
        private ScrolllingBackground ScrolllingBackground;

        // Texture representing the background
        public Texture2D BackgroundTextureTwo;

        // SoundEffect played when selecting a menu item
        public SoundEffect MenuSelectSound;

        // List to store sprites; likely used for managing visual elements in the scene
        private List<Sprite> _sprites;

        internal MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont regular, SpriteFont highlighted,
            Vector2 position, string[] items, Color regularColor, Color highlightColor) : base(game)
        {
            // Assigning the provided parameters to the respective class fields
            this._spriteBatch = spriteBatch;
            this._regular = regular;
            this._highlighted = highlighted;
            this._position = position;
            this._items = items;
            this._regularColor = regularColor;
            this._highlightColor = highlightColor;
            this.SelectedIndex = 0;

            // Loading a background texture for the menu from the game's content
            var backgroundTexture = game.Content.Load<Texture2D>("2d/Background/SpaceSpriteSheet");

            // Loading a sound effect for menu selection from the game's content
            MenuSelectSound = game.Content.Load<SoundEffect>("Sound/Final Fantasy VII Sound Effects - Cursor Move (mp3cut.net)");

            // Loading another background texture for the menu from the game's content
            BackgroundTextureTwo = game.Content.Load<Texture2D>("2d/Background/Window_Header (1)");

            // Loading a SpriteFont for rendering text in a different style
            _randomFont = game.Content.Load<SpriteFont>("Font/RandomFont");

            // Creating a new list to store sprites
            _sprites = new List<Sprite>();

            // Creating a scrolling background object using the previously loaded background texture
            ScrolllingBackground = new ScrolllingBackground(backgroundTexture, 0.1f, new Vector2(0, 0));

        }

        public override void Update(GameTime gameTime)
        {
            // Obtains the current state of the keyboard
            KeyboardState ks = Keyboard.GetState();

            // Checks for Down arrow key press and updates the selected index accordingly
            if (ks.IsKeyDown(Keys.Down) && _oldState.IsKeyUp(Keys.Down))
            {
                // Increases the selectedIndex by 1, looping back to 0 if it reaches the end of the items array
                SelectedIndex = SelectedIndex + 1 == _items.Length ? 0 : SelectedIndex + 1;

                // Plays the menuSelectSound when the Down key is pressed
                MenuSelectSound.Play();
            }

            // Checks for Up arrow key press and updates the selected index accordingly
            if (ks.IsKeyDown(Keys.Up) && _oldState.IsKeyUp(Keys.Up))
            {
                // Decreases the selectedIndex by 1, looping to the end of the items array if it reaches 0
                SelectedIndex = SelectedIndex == 0 ? _items.Length - 1 : SelectedIndex - 1;

                // Plays the menuSelectSound when the Up key is pressed
                MenuSelectSound.Play();
            }

            // Updates the oldState to the current KeyboardState for the next iteration
            _oldState = ks;

            // Updates the scrolling background based on the elapsed time and sprite collection
            ScrolllingBackground.Update(gameTime, _sprites);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Begins the SpriteBatch to start drawing
            _spriteBatch.Begin();

            // Draws the scrolling background using the ScrolllingBackground class
            ScrolllingBackground.Draw(_spriteBatch);

            // Draws another background texture at a specific position (150, 100) with Color.White tint
            _spriteBatch.Draw(BackgroundTextureTwo, new Vector2(150, 100), Color.White);

            // Draws text using the randomFont SpriteFont for the game title and subtitle
            _spriteBatch.DrawString(_randomFont, "Galactic", new Vector2(260, 425), Color.Black);
            _spriteBatch.DrawString(_randomFont, "Defender", new Vector2(250, 480), Color.Black);

            // Iterates through the items array to draw menu items
            for (int i = 0; i < _items.Length; i++)
            {
                // Checks if the current item index matches the selectedIndex for highlighting
                if (i == SelectedIndex)
                {
                    // Draws the highlighted item using the highlighted SpriteFont and highlightColor
                    _spriteBatch.DrawString(_highlighted, _items[i],
                        new Vector2(_position.X, _position.Y + _highlighted.LineSpacing * i), _highlightColor);
                }
                else
                {
                    // Draws the regular items using the regular SpriteFont and regularColor
                    _spriteBatch.DrawString(_regular, _items[i],
                        new Vector2(_position.X, _position.Y + _highlighted.LineSpacing * i), _regularColor);
                }
            }

            // Ends the SpriteBatch after drawing all elements
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

