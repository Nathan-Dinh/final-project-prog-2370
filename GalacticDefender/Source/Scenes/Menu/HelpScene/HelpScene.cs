/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
using JP_ND_FinalProject.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NDJPFinal.Source.Scenes.Menu.HelpScene
{

    public class HelpScene : GameScene
    {
        private SpriteBatch _spriteBatch;
        private HelpComponent _helpComponent;
        public HelpScene(Game game) : base(game)
        {
            Main game1 = (Main)game; // Casts the 'Game' instance to the 'Main' type
            _spriteBatch = game1.SpriteBatch; // Assigns the 'SpriteBatch' property of 'game1' to the local 'spriteBatch' variable
            _helpComponent = new HelpComponent(game1, _spriteBatch); // Creates a new instance of 'HelpComponent' by passing 'game1' and 'spriteBatch' as parameters
            Components.Add(_helpComponent); // Adds the 'helpComponent' to the 'Components' collection of the game
        }
    }
}
