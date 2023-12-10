/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
using JP_ND_FinalProject.Scenes;
using Microsoft.Xna.Framework;

namespace NDJPFinal.Source.Scenes.Menu.GameSetting
{
    public class AboutScene : GameScene
    {
        AboutComponent  gameSettingComponent;
        public AboutScene(Game game) : base(game)
        {
            Main game1 = (Main)game; // Casts the 'Game' instance to the 'Main' type
            var spriteBatch = game1.SpriteBatch; // Retrieves the 'SpriteBatch' object from 'game1' and assigns it to a local variable 'spriteBatch'
            gameSettingComponent = new AboutComponent(game, spriteBatch); // Creates a new instance of 'AboutComponent' by passing 'game' and 'spriteBatch' as parameters
            Components.Add(gameSettingComponent); // Adds the 'gameSettingComponent' to the 'Components' collection of the game
        }
    }
}
