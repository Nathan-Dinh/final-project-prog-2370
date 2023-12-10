using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace NDJPFinal.Source.Sprites.HelpPage
{
    public class ArrowKeys : Sprite
    {
        public Texture2D textureUPArrow;
        public Texture2D textureDownArrow;
        public Texture2D textureLeftArrow;
        public Texture2D textureRightArrow;

        public List<Rectangle> upArrowFrames;
        public List<Rectangle> downArrowFrames;
        public List<Rectangle> leftArrowFrames;
        public List<Rectangle> rightArrowFrames;

        public int TextureWidth;
        public int TextureHeight;


        public int upArrowFramesTracker;
        public int downArrowFramesTracker;
        public int rightArrowFramesTracker;
        public int leftArrowFramesTracker;

        public ArrowKeys(Texture2D textureUpArrow,
            Texture2D textureDownArrow,
            Texture2D textureRightArrow,
            Texture2D textureLeftArrow, float layer) : base(textureUpArrow, layer)
        {
            this.textureUPArrow = textureUpArrow;
            this.textureDownArrow = textureDownArrow;
            this.textureLeftArrow = textureLeftArrow;
            this.textureRightArrow = textureRightArrow;

            this.TextureWidth = textureUPArrow.Width / 2;
            this.TextureHeight = textureUPArrow.Height;

            this.upArrowFrames = new List<Rectangle>();
            this.downArrowFrames = new List<Rectangle>();
            this.leftArrowFrames = new List<Rectangle>();
            this.rightArrowFrames = new List<Rectangle>();

            upArrowFramesTracker = 0;
            downArrowFramesTracker = 0;
            rightArrowFramesTracker = 0;
            leftArrowFramesTracker = 0;

            for (int i = 0; i < 2; i++)
            {
                upArrowFrames.Add(new Rectangle(TextureWidth * i, 0, TextureWidth, TextureHeight));
                downArrowFrames.Add(new Rectangle(TextureWidth * i, 0, TextureWidth, TextureHeight));
                leftArrowFrames.Add(new Rectangle(TextureWidth * i, 0, TextureWidth, TextureHeight));
                rightArrowFrames.Add(new Rectangle(TextureWidth * i, 0, TextureWidth, TextureHeight));
            }
        }


        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            this.previousKey = this.currentKey;
            this.currentKey = Keyboard.GetState();

            if (this.currentKey.IsKeyDown(Keys.Left))
            {
                leftArrowFramesTracker = 1;
            }
            else
            {
                leftArrowFramesTracker = 0;
            }

            if (this.currentKey.IsKeyDown(Keys.Right))
            {
                rightArrowFramesTracker = 1;
            }
            else
            {
                rightArrowFramesTracker = 0;
            }

            if (this.currentKey.IsKeyDown(Keys.Down))
            {
                downArrowFramesTracker = 1;
            }
            else
            {
                downArrowFramesTracker = 0;
            }

            if (this.currentKey.IsKeyDown(Keys.Up))
            {
                upArrowFramesTracker = 1;
            }
            else
            {
                upArrowFramesTracker = 0;
            }


            base.Update(gametime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureUPArrow, Position - new Vector2(0, 80), upArrowFrames[upArrowFramesTracker], Color.White);
            spriteBatch.Draw(textureDownArrow, Position + new Vector2(0, 0), downArrowFrames[downArrowFramesTracker], Color.White);
            spriteBatch.Draw(textureLeftArrow, Position - new Vector2(80, 0), leftArrowFrames[leftArrowFramesTracker], Color.White);
            spriteBatch.Draw(textureRightArrow, Position + new Vector2(80, 0), rightArrowFrames[rightArrowFramesTracker], Color.White);
        }
    }
}
