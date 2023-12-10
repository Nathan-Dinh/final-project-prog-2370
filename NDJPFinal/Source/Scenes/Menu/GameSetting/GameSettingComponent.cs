using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDJPFinal.Source.Scenes.Menu.GameSetting
{
    public class GameSettingComponent : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D backgroundTexture;
        public GameSettingComponent(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            backgroundTexture = game.Content.Load<Texture2D>("2d/Background/Window_Header (3)");
        }

        public override void Update(GameTime gameTime) 
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
