/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace NDJPFinal.Source.Scenes.Menu.GameSetting
{
    public class AboutComponent : DrawableGameComponent
    {
        // Used to manage and perform 2D rendering operations efficiently
        private SpriteBatch _spriteBatch;

        // Represents a 2D image or texture, often used for backgrounds, sprites, or UI elements
        private Texture2D _backgroundTexture;

        // Represents a font used for rendering text in 2D
        private SpriteFont _spriteFont;
        public AboutComponent(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this._spriteBatch = spriteBatch;
            _backgroundTexture = game.Content.Load<Texture2D>("2d/Background/Window_Header (3)");
            _spriteFont = game.Content.Load<SpriteFont>("Font/RegularFont");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(); // Starts the sprite drawing process

            // Draws a background texture at the specified position (0, 0) with the color set to White
            _spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

            // Draws a string of text using the specified spriteFont, wrapped to fit within 600 pixels width
            // The text narrates a story about a tormented man, Ethan, and his experiences in a dystopian future
            _spriteBatch.DrawString(_spriteFont,
                WrapText(_spriteFont, "In a dystopian future, Ethan, a tormented man with a dark past, " +
                "is hired by the corrupt corporation EnerCorp to hunt down the Luminae, " +
                "a species whose radiant energy is exploited to power the last remaining cities. " +
                "As Ethan delves deeper into the space, he witnesses the horrors inflicted upon the Luminae, " +
                "their essence drained by monstrous machines. " +
                "Consumed by guilt and empathy, he questions his purpose.", 600f),
                new Vector2(100, 100), // Position of the text on the screen
                Color.Black); // Color of the text

            _spriteBatch.End(); // Ends the sprite drawing process

            base.Draw(gameTime); // Calls the base Draw method to proceed with the game's drawing logic
        }

        public string WrapText(SpriteFont spriteFont, string text, float maxLineWidth)
        {
            // Split the input text into an array of words
            string[] words = text.Split(' ');

            // StringBuilder to construct the wrapped text
            StringBuilder sb = new StringBuilder();

            // Variable to track the current line width
            float lineWidth = 0f;

            // Calculate the width of a space character in the font
            float spaceWidth = spriteFont.MeasureString(" ").X;

            // Iterate through each word in the text
            foreach (string word in words)
            {
                // Measure the size (width) of the current word using the provided spriteFont
                Vector2 size = spriteFont.MeasureString(word);

                // Check if adding the word to the current line exceeds the maximum line width
                if (lineWidth + size.X < maxLineWidth)
                {
                    // Append the word to the current line along with a space
                    sb.Append(word + " ");

                    // Update the line width with the added word and space
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    // Add a line break and start a new line with the current word
                    sb.Append("\n" + word + " ");

                    // Reset the line width with the new word for the new line
                    lineWidth = size.X + spaceWidth;
                }
            }

            // Return the resulting wrapped text as a single string
            return sb.ToString();
        }
    }
}
