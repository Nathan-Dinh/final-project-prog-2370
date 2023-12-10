/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
using JP_ND_FinalProject.Scenes;
using Microsoft.Xna.Framework;

namespace NDJPFinal.Source.Scenes.BattleReport
{
    public class BattleReportScene : GameScene
    {
        public  BattleReportComponent BattleReportComponent;

        public BattleReportScene(Game game) : base(game)
        {
            // Cast the 'game' instance as 'Main' class
            Main game1 = game as Main;

            // Retrieve the SpriteBatch from the Main class instance
            var spriteBatch = game1.SpriteBatch;

            // Create a new BattleReportComponent instance with the retrieved spriteBatch
            BattleReportComponent = new BattleReportComponent(game, spriteBatch);

            // Add the BattleReportComponent to the Components collection of the current instance
            this.Components.Add(BattleReportComponent);
        }
    }
}
