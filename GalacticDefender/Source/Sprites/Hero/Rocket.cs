/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace NDJPFinal.Source.Sprites.Hero
{
    public class Rocket : Sprite
    {
        #region Properities
        // Private field to keep track of time
        private float _timer;

        // List to store rectangles representing frames of animation
        private List<Rectangle> _frames = new List<Rectangle>();

        // Public property representing the width of the texture
        public int TextureWidth;

        // Public property representing the height of the texture
        public int TextureHeight;

        // Public property representing the current frame index or tracker
        private int _frameTracker;
        #endregion

        public Rocket(Texture2D texture, float layer) : base(texture, layer)
        {
            TextureWidth = texture.Width / 3;
            TextureHeight = texture.Height;

            for (int x = 0; x < 4; x++)
            {
                _frames.Add(new Rectangle(x * TextureWidth, 0, TextureWidth, TextureHeight));
            }
        }


        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            // Increment the timer by the elapsed time since the last update
            _timer += (float)gametime.ElapsedGameTime.TotalSeconds;

            // If the FrameTracker reaches a certain value, reset it to 0
            if (_frameTracker == 3)
            {
                _frameTracker = 0;
            }

            // If the timer exceeds the specified LifeSpan, mark the sprite as removed
            if (_timer > LifeSpan)
            {
                IsRemoved = true;
            }

            // Move the sprite upwards based on its linear velocity
            Position.Y -= LinearVelocity;

            // If the timer exceeds a specific value (0.25 seconds in this case)
            if (_timer > 0.25)
            {
                // Move to the next frame of animation
                _frameTracker += 1;

                // Reset the timer to restart the frame change interval
                _timer = 0;
            }

            // Call the base class's Update method to update the sprite
            base.Update(gametime, sprites);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, _frames[_frameTracker], Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, _layer);
        }
    }
}
