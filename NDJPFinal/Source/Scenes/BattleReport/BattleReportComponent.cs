using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NDJPFinal.Source.Global;
using System.Reflection;

namespace NDJPFinal.Source.Scenes.BattleReport
{
    public class BattleReportComponent : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D backgroundTexture;
        public SpriteFont Font;

        public BattleReportComponent(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            backgroundTexture = game.Content.Load<Texture2D>("2d/Background/Window_Header (3)");
            Font = game.Content.Load<SpriteFont>("Font/HighlightedFont");
        }

        public override void Update(GameTime gameTime)
        {
            if (BattleReportStats.Seconds == 0 && BattleReportStats.Minutes == 0)
            {
                BattleReportStats.Seconds = (int)(BattleReportStats.PlaySession.Seconds);
                BattleReportStats.Minutes = (int)(BattleReportStats.PlaySession.TotalMinutes);
/*
                BattleReportStats.Seconds = (int)(gameTime.TotalGameTime.TotalSeconds - BattleReportStats.Seconds);
                BattleReportStats.Minutes = (int)(gameTime.TotalGameTime.TotalMinutes - BattleReportStats.Minutes);*/

                BattleReportStats.TotalScore = (BattleReportStats.HitsTaken *-50) + 
                    ((BattleReportStats.AmmoHits - BattleReportStats.AmmoShot) * 25);

                if (BattleReportStats.MissionStatus == "SUCCESS")
                {
                    BattleReportStats.TotalScore += 1000;
                }
                else
                {
                    BattleReportStats.TotalScore += 500;
                }
            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(Font, "Battle Report", new Vector2(200, 100), Color.Black);
            spriteBatch.DrawString(Font, $"Status: {BattleReportStats.MissionStatus}", new Vector2(80, 200), Color.Black);
            spriteBatch.DrawString(Font, $"Time: {BattleReportStats.Minutes} : {BattleReportStats.Seconds}", new Vector2(80, 300), Color.Black);
            spriteBatch.DrawString(Font, $"Shots fired: {BattleReportStats.AmmoShot}", new Vector2(80, 400), Color.Black);
            spriteBatch.DrawString(Font, $"Shots Hit: {BattleReportStats.AmmoHits}", new Vector2(80, 500), Color.Black);
            spriteBatch.DrawString(Font, $"Score :{BattleReportStats.TotalScore} ", new Vector2(200, 600), Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
