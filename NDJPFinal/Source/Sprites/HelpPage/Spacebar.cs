using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace NDJPFinal.Source.Sprites.HelpPage
{
    public class Spacebar : Sprite
    {
        private Texture2D _texture;
        private List<Rectangle> _frames;
        private int FrameWidth;
        private int FrameHeight;
        private int FrameTracker;

        public Spacebar(Texture2D texture, float layer) : base(texture, layer)
        {
            this._texture = texture;
            this.FrameWidth = this._texture.Width/2;
            this.FrameHeight = this._texture.Height;
            this.FrameTracker = 0;

           _frames= new List<Rectangle>();

            for (int i = 0; i < 2; i++)
            {
                _frames.Add(new Rectangle(FrameWidth * i, 0, FrameWidth,FrameHeight));
            }
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {

            this.previousKey = this.currentKey;
            this.currentKey = Keyboard.GetState();

             if (this.currentKey.IsKeyDown(Keys.Space))
             {
                FrameTracker = 1;
             }
             else
             {
                FrameTracker= 0;
             }

            base.Update(gametime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, _frames[FrameTracker],Color.White);
        }
    }
}
