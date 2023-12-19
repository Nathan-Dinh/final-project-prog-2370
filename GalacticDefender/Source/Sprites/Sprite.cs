/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
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
        #region Properities
        // Texture representing the sprite
        protected Texture2D _texture;

        // Position of the sprite in 2D space
        protected Vector2 _position;

        // Rotation of the sprite in radians
        protected float _rotation;

        // Scale factor of the sprite
        protected float _scale = 1;

        // Layer depth of the sprite for rendering order
        protected float _layer;

        // Represents the current state of the keyboard
        public KeyboardState CurrentKey;

        // Represents the previous state of the keyboard
        public KeyboardState PreviousKey;

        // Position of the sprite in 2D space
        public Vector2 Position;

        // Origin point of the sprite for rotations and scaling
        public Vector2 Origin;

        // Linear velocity of the sprite
        public float LinearVelocity;

        // Lifespan of the sprite before removal (if applicable)
        public float LifeSpan;

        // Reference to the parent sprite if this sprite is a child
        public Sprite Parent;

        // Indicates if the sprite has been removed
        public bool IsRemoved;
        #endregion
        public Sprite(Texture2D texture, float layer)
        {
            _texture = texture;
            _layer = layer;
            LinearVelocity = 6f;
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

        // This method creates a shallow copy of the current object.
        // It utilizes the MemberwiseClone() method provided by the .NET framework.
        public object Clone()
        {
            // Returns a shallow copy of the current object
            return MemberwiseClone();
        }
    }
}
