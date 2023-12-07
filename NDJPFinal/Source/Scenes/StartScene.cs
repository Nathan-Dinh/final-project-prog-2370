using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDJPFinal;
using NDJPFinal.Source.Scenes;

namespace JP_ND_FinalProject.Scenes
{
    public class StartScene : GameScene 
    {
        SpriteBatch spriteBatch;
        MenuComponent menu;
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
                new Vector2(100, 100), new string[] { "START", "OPTIONS","HELP", "EXIT" },
                Color.White, Color.Red);
            this.Components.Add(menu);
        }
    }
}
