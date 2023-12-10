using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using NDJPFinal.Source.Sprites;
using JP_ND_FinalProject.Scenes;

namespace NDJPFinal.Source.Scenes.Menu.StartScene
{
    public class StartScene : GameScene
    {
        SpriteBatch spriteBatch;
        MenuComponent menu;
        ScrolllingBackground scrolllingBackground;

        public int getSelectedIndex()
        {
            return menu.selectedIndex;
        }
        public StartScene(Game game) : base(game)
        {
            Main game1 = (Main)game;
            spriteBatch = game1.SpriteBatch;
            SpriteFont regular = game1.Content.Load<SpriteFont>("Font/RegularFont");
            SpriteFont highlited = game1.Content.Load<SpriteFont>("Font/HighlightedFont");
            menu = new MenuComponent(game1, spriteBatch, regular, highlited,
                new Vector2(250, 175), new string[] { "START", "OPTIONS", "HELP", "EXIT" },
                Color.White, Color.White);

            Components.Add(menu);
        }
    }
}
