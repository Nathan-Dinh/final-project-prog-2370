using JP_ND_FinalProject.Scenes;
using Microsoft.Xna.Framework;

namespace NDJPFinal.Source.Scenes.Stages
{
    public class StageOneScene : GameScene
    {
        public static bool GameResult = false;

        public StageOne stageOne;
        public StageOneScene(Game game) : base(game)
        {
            Main game1 = (Main)Game;
            stageOne = new StageOne(game1);
            Components.Add(stageOne);
        }

        public void ResetStage(Game game) 
        {
            GameResult = false;
            game.Components.Remove(stageOne.heroManager);
            game.Components.Remove(stageOne.bossOneManager);
            Components.Remove(stageOne);
            stageOne = new StageOne(game);
            Components.Add(stageOne);

        }
    }
}
