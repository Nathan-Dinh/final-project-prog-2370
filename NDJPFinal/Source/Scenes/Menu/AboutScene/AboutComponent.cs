using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace NDJPFinal.Source.Scenes.Menu.GameSetting
{
    public class AboutComponent : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D backgroundTexture;
        SpriteFont spriteFont;
        public AboutComponent(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            backgroundTexture = game.Content.Load<Texture2D>("2d/Background/Window_Header (3)");
            spriteFont = game.Content.Load<SpriteFont>("Font/RegularFont");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(spriteFont, WrapText(spriteFont, "In a dystopian future, Ethan, a tormented man with a " +
                "dark past, is hired by the corrupt corporation EnerCorp to hunt down the Luminae, a " +
                "species whose radiant energy is exploited to power the last remaining cities. " +
                "As Ethan delves deeper into the space, he witnesses the horrors inflicted upon " +
                "the Luminae, their essence drained by" +
                " monstrous machines. Consumed by guilt " +
                "and empathy, he questions his purpose.",600f), new Vector2(100, 100), Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public string WrapText(SpriteFont spriteFont, string text, float maxLineWidth)
        {
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = spriteFont.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = spriteFont.MeasureString(word);

                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }

            return sb.ToString();
        }

    }
}
