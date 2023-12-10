using JP_ND_FinalProject.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NDJPFinal.Source.Scenes.Menu.GameSetting
{
    public class AboutScene : GameScene
    {

        AboutComponent  gameSettingComponent;
        public AboutScene(Game game) : base(game)
        {
            Main game1 = (Main)game;
             var spriteBactch = game1.SpriteBatch;

            gameSettingComponent = new AboutComponent(game, spriteBactch);
            Components.Add(gameSettingComponent);

        }
    }
}
