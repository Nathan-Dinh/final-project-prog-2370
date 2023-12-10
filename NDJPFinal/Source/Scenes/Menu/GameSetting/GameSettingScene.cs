using JP_ND_FinalProject.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NDJPFinal.Source.Scenes.Menu.GameSetting
{
    public class GameSettingScene : GameScene
    {

        GameSettingComponent  gameSettingComponent;
        public GameSettingScene(Game game) : base(game)
        {
            Main game1 = (Main)game;
             var spriteBactch = game1.SpriteBatch;

            gameSettingComponent = new GameSettingComponent(game, spriteBactch);
            Components.Add(gameSettingComponent);

        }
    }
}
