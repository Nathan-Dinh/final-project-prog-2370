/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NDJPFinal.Source.Sprites;
using NDJPFinal.Source.Sprites.HelpPage;
using System.Collections.Generic;

namespace NDJPFinal.Source.Scenes.Menu.HelpScene
{
    public class HelpComponent : DrawableGameComponent
    {
        public SpriteBatch SpriteBatch; // Represents a SpriteBatch used for drawing graphics in the game.

        public Texture2D BackgroundTexture; // Represents a 2D texture used as a background in the game.

        public SpriteFont Font; // Represents a SpriteFont used for rendering text in the game.

        public List<Sprite> ListOfSprite; // Represents a list of Sprite objects in the game.

        public HelpComponent(Game game, SpriteBatch SpriteBatch) : base(game)
        {
            // Assigns the provided SpriteBatch to the class's SpriteBatch property
            this.SpriteBatch = SpriteBatch;

            // Loads textures for different arrow keys and the space bar
            var spaceBar = game.Content.Load<Texture2D>("2d/Background/BACKSPACEALTERNATIVE (1)");
            var upArrow = game.Content.Load<Texture2D>("2d/Background/ARROWUP");
            var downArrow = game.Content.Load<Texture2D>("2d/Background/ARROWDOWN");
            var rightArrow = game.Content.Load<Texture2D>("2d/Background/ARROWRIGHT");
            var leftArrow = game.Content.Load<Texture2D>("2d/Background/ARROWLEFT");

            // Loads a SpriteFont for rendering text
            Font = game.Content.Load<SpriteFont>("Font/HighlightedFont");

            // Loads a background texture
            BackgroundTexture = game.Content.Load<Texture2D>("2d/Background/Window_Header (3)");


            // Creates a new instance of the 'Spacebar' class using the 'spaceBar' texture
            // and assigns it to the variable 'SpaceBarSprite'
            var SpaceBarSprite = new Spacebar(spaceBar, 0.1f)
            {
                Position = new Vector2(100, 200) // Sets the position of the 'SpaceBarSprite'
            };

            // Creates a new instance of the 'ArrowKeys' class using the arrow textures
            // and assigns it to the variable 'arrowKeys'
            var arrowKeys = new ArrowKeys(upArrow, downArrow, rightArrow, leftArrow, 0.1f)
            {
                Position = new Vector2(170, 450) // Sets the position of the 'arrowKeys'
            };

            // Creates a new list 'ListOfSprite' and adds the created sprites to it
            ListOfSprite = new List<Sprite>() {
                    SpaceBarSprite, // Adds the 'SpaceBarSprite' instance to the list
                    arrowKeys,      // Adds the 'arrowKeys' instance to the list
            };
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var sprite in ListOfSprite)
                sprite.Update(gameTime, ListOfSprite);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin(); // Begins the sprite batch for drawing

            SpriteBatch.Draw(BackgroundTexture, new Vector2(0, 0), Color.White); // Draws the background texture at position (0, 0)

            SpriteBatch.DrawString(Font, "Shoot", new Vector2(400, 200), Color.Black); // Draws the text "Shoot" at position (400, 200) using the specified font

            SpriteBatch.DrawString(Font, "Movements", new Vector2(400, 450), Color.Black); // Draws the text "Movements" at position (400, 450) using the specified font

            foreach (var sprite in ListOfSprite)
                sprite.Draw(SpriteBatch); // Calls the Draw method for each sprite in the ListOfSprite, allowing them to draw themselves

            SpriteBatch.End(); // Ends the sprite batch

            base.Draw(gameTime); // Calls the base Draw method, which may handle additional drawing or processing
        }
    }
}
