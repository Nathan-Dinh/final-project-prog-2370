/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
using JP_ND_FinalProject.Scenes;
using Microsoft.Xna.Framework;

namespace NDJPFinal.Source.Scenes.Stages
{
    public class StageOneScene : GameScene
    {
        // A public static boolean variable named GameResult, initialized to false
        public static bool GameResult = false;

        // Declaration of a 'stageOne' instance of the 'StageOne' class
        public StageOne StageOne;

        // Constructor for the 'StageOneScene' class that takes a 'game' parameter
        public StageOneScene(Game game) : base(game)
        {
            // Casting the 'Game' object to type 'Main'
            Main game1 = (Main)Game;

            // Creating a new instance of 'StageOne' and assigning it to the 'stageOne' field
            StageOne = new StageOne(game1);

            // Adding the 'stageOne' component to the list of components in 'StageOneScene'
            Components.Add(StageOne);
        }
    }
}
