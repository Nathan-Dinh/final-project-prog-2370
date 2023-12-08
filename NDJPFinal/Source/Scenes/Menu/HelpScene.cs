using JP_ND_FinalProject.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDJPFinal.Source.Scenes.Menu
{

    public class HelpScene : GameScene
    {

        SpriteBatch spriteBactch;
        HelpComponent helpComponent;

        public HelpScene(Game game) : base(game)
        {
            Main game1 = (Main)game;
            spriteBactch = game1.SpriteBatch;
            helpComponent = new HelpComponent(game1, spriteBactch);
            this.Components.Add(helpComponent);
        }
    }
}
