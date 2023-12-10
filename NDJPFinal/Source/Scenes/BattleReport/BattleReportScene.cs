using JP_ND_FinalProject.Scenes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDJPFinal.Source.Scenes.BattleReport
{
    public class BattleReportScene : GameScene
    {
        public  BattleReportComponent BattleReportComponent;

        public BattleReportScene(Game game) : base(game)
        {
            Main game1 = game as Main;
            var spriteBactch = game1.SpriteBatch;
            BattleReportComponent = new BattleReportComponent(game, spriteBactch);
              this.Components.Add(BattleReportComponent);
        }
    }
}
