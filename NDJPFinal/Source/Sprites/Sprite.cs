using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NDJPFinal.Source.Sprites
{
    public class Sprite : ICloneable
    {
        protected Texture2D _texture;

        protected Vector2 _postion;

        public float _rotation;

        protected float _scale = 1;

        protected float _layer;


        public KeyboardState currentKey;

        public KeyboardState previousKey;

        public Vector2 Position;

        public Vector2 Origin;
        public float LinearVelcitoy;
        public float LifeSpan;
        public Sprite Parent;

        public bool IsRemoved;

        public Sprite(Texture2D texture, float layer)
        {
            _texture = texture;
            _layer = layer;
            LinearVelcitoy = 6f;
            LifeSpan = 0f;
            IsRemoved = false;
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
        }

        public virtual void Update(GameTime gametime, List<Sprite> sprites)
        {
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, Origin, _scale, SpriteEffects.None, _layer);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }


    }
}
