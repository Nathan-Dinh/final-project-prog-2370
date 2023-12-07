using JP_ND_FinalProject;
using JP_ND_FinalProject.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NDJPFinal.Source.Manager;
using NDJPFinal.Source.Sprites;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NDJPFinal.Source.Scenes.Stages
{
    public class StageOneScene : GameScene
    {
        public StageOneScene(Game game) : base(game)
        {
            var actuallyGame = new StageOne(game);
            Components.Add(actuallyGame);
        }
    }
}
