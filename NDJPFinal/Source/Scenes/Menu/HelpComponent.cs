using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NDJPFinal.Source.Sprites;
using NDJPFinal.Source.Sprites.HelpPage;
using NDJPFinal.Source.Sprites.Hero;
using System.Collections.Generic;

namespace NDJPFinal.Source.Scenes.Menu
{
    public class HelpComponent : DrawableGameComponent
    {
        public SpriteBatch SpriteBatch;
        public Texture2D backgroundTexture;
        public List<Sprite> ListOfSprite;

        public HelpComponent(Game game, SpriteBatch SpriteBatch) : base(game)
        {
            this.SpriteBatch= SpriteBatch;
            var spaceBar = game.Content.Load<Texture2D>("2d/Background/BACKSPACEALTERNATIVE (1)");
            var upArrow = game.Content.Load<Texture2D>("2d/Background/ARROWUP");
            var downArrow = game.Content.Load<Texture2D>("2d/Background/ARROWDOWN");
            var rightArrow = game.Content.Load<Texture2D>("2d/Background/ARROWRIGHT");
            var leftArrow = game.Content.Load<Texture2D>("2d/Background/ARROWLEFT");
            backgroundTexture = game.Content.Load<Texture2D>("2d/Background/Window_Header (3)");


            var SpaceBarSprite = new Spacebar(spaceBar, 0.1f) 
            {
                Position = new Vector2(100,200)
            };
            var arrowKeys = new ArrowKeys(upArrow, downArrow, rightArrow, leftArrow, 0.1f) 
            {
                Position= new Vector2(180,450)
            };
            
            ListOfSprite= new List<Sprite>() {
                SpaceBarSprite,
                arrowKeys,
            };
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var sprite in ListOfSprite)
                sprite.Update(gameTime,ListOfSprite);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);
            foreach (var sprite in ListOfSprite)
                sprite.Draw(SpriteBatch);
            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
