using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using NDJPFinal.Source.Sprites;
using JP_ND_FinalProject.Scenes;

namespace NDJPFinal.Source.Scenes.Menu.StartScene
{
    public class StartScene : GameScene
    {
       private SpriteBatch SpriteBatch;
       private MenuComponent Menu;

        public int getSelectedIndex()
        {
            return Menu.SelectedIndex;
        }
        public StartScene(Game game) : base(game)
        {
            // Casts the 'game' parameter as an instance of the 'Main' class
            Main game1 = (Main)game;

            // Assigns the spriteBatch from 'game1' to the local spriteBatch variable
            SpriteBatch = game1.SpriteBatch;

            // Loads SpriteFont resources for regular and highlighted fonts from game content
            SpriteFont regular = game1.Content.Load<SpriteFont>("Font/RegularFont");
            SpriteFont highlited = game1.Content.Load<SpriteFont>("Font/HighlightedFont");

            // Initializes a new MenuComponent instance with specific parameters:
            // - game1: The main game instance
            // - spriteBatch: The SpriteBatch used for drawing
            // - regular: The regular font for menu items
            // - highlited: The highlighted font for the selected menu item
            // - Vector2(250, 175): The position to display the menu
            // - An array of strings representing menu options: { "START", "ABOUT", "HELP", "EXIT" }
            // - Color.White: Color for regular menu items
            // - Color.White: Color for highlighted menu item
            Menu = new MenuComponent(game1, SpriteBatch, regular, highlited,
                new Vector2(250, 175), new string[] { "START", "ABOUT", "HELP", "EXIT" },
                Color.White, Color.White);

            Components.Add(Menu);
        }
    }
}
