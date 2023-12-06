using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDJPFinal.Source.Fonts
{
    internal class Font
    {
        protected SpriteFont _font;
        protected float _layer;
        protected string _content;

        protected Vector2 _postion;


        public Font(SpriteFont font,string content, float layer)
        {
            this._font = font;
            this._layer = layer;
            this._content = content;
          
        }

        public virtual void Update(GameTime gametime, List<Font> fonts)
        {

        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font,_content,_postion,Color.Red);
        }
    }
}
