/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NDJPFinal.Source.Global;

namespace NDJPFinal.Source.Scenes.BattleReport
{
    public class BattleReportComponent : DrawableGameComponent
    {
        // Declaration of SpriteBatch to handle rendering 2D graphics
        private SpriteBatch _spriteBatch;

        // Texture2D for storing background image or texture
        private Texture2D _backgroundTexture;

        // SpriteFont for storing font information used in rendering text
        private SpriteFont _font;

        public BattleReportComponent(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this._spriteBatch = spriteBatch;
            _backgroundTexture = game.Content.Load<Texture2D>("2d/Background/Window_Header (3)");
            _font = game.Content.Load<SpriteFont>("Font/HighlightedFont");
        }

        public override void Update(GameTime gameTime)
        {
            // Check if the battle report timer has reached 0 seconds and 0 minutes
            if (BattleReportStats.Seconds == 0 && BattleReportStats.Minutes == 0)
            {
                // Reset the time metrics to the recorded play session's time
                BattleReportStats.Seconds = (int)(BattleReportStats.PlaySession.Seconds);
                BattleReportStats.Minutes = (int)(BattleReportStats.PlaySession.TotalMinutes);

                // Calculate the total score based on hits taken, ammo hits, ammo shots, and mission status
                BattleReportStats.TotalScore = (BattleReportStats.HitsTaken * -50) +
                    ((BattleReportStats.AmmoHits - BattleReportStats.AmmoShot) * 25);

                // Adjust total score based on mission status
                if (BattleReportStats.MissionStatus == "SUCCESS")
                {
                    // Add score if the mission was successful
                    BattleReportStats.TotalScore += 1000;
                }
                else
                {
                    // Add a different score if the mission was not successful
                    BattleReportStats.TotalScore += 250;
                }
            }
        }
        public override void Draw(GameTime gameTime)
        {
            // Begin drawing using the SpriteBatch
            _spriteBatch.Begin();

            // Draw the background texture at the specified position with the provided color
            _spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

            // Draw various battle report details using SpriteFont and provided text and positions
            _spriteBatch.DrawString(_font, "Battle Report", new Vector2(200, 100), Color.Black);
            _spriteBatch.DrawString(_font, $"Status: {BattleReportStats.MissionStatus}", new Vector2(80, 200), Color.Black);
            _spriteBatch.DrawString(_font, $"Time: {BattleReportStats.Minutes} : {BattleReportStats.Seconds}", new Vector2(80, 300), Color.Black);
            _spriteBatch.DrawString(_font, $"Shots fired: {BattleReportStats.AmmoShot}", new Vector2(80, 400), Color.Black);
            _spriteBatch.DrawString(_font, $"Shots Hit: {BattleReportStats.AmmoHits}", new Vector2(80, 500), Color.Black);
            _spriteBatch.DrawString(_font, $"Score :{BattleReportStats.TotalScore} ", new Vector2(200, 600), Color.Black);

            // End the drawing process using the SpriteBatch
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
