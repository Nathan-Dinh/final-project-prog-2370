﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using NDJPFinal;
using NDJPFinal.Source.Scenes;
using NDJPFinal.Source.Sprites;

namespace JP_ND_FinalProject.Scenes
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
                new Vector2(265, 175), new string[] { "START", "OPTIONS", "HELP", "EXIT" },
                Color.White, Color.Red) ;

            this.Components.Add(menu);
        }
    }
}
