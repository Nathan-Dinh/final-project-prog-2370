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
    public class Bullet : Sprite
    {
        #region Properties
        // Private field to track time
        private float _timer;

        // List to store rectangles representing frames for the projectile's animation
        private List<Rectangle> _projectileAnimation;

        // List to store the current animation frames for the projectile
        private List<Rectangle> _currentAnimationFrames;

        // Tracks the current frame of the projectile animation
        private int _tracker;

        // Represents the width of the texture for the projectile
        public int TextureWidth;

        // Represents the height of the texture for the projectile
        public int TextureHeight;

        // Property to get the boundary of the sprite (used for collision detection or positioning)
        public Rectangle SpriteBoundry
        {
            get
            {
                // Returns a rectangle representing the boundary of the sprite based on its position, width, and height
                return new Rectangle((int)Position.X - TextureWidth, (int)Position.Y - TextureHeight, 0, 0);
            }
        }
        #endregion

        public Bullet(Texture2D texture, float layer) : base(texture, layer)
        {
            // Calculate dimensions of frames for the projectile's animation
            TextureWidth = texture.Width / 4; // Assuming the texture is divided into 4 frames horizontally
            TextureHeight = texture.Height;

            // Initialize lists to store frames for the projectile's animation
            _currentAnimationFrames = new List<Rectangle>();
            _projectileAnimation = new List<Rectangle>();

            // Create rectangles for each frame of the projectile's animation
            for (int x = 0; x < 4; x++) // Assuming 4 frames for the projectile's animation
            {
                _projectileAnimation.Add(new Rectangle(x * TextureWidth, 0, TextureWidth, TextureHeight));
            }

            // Set the current animation frames to the generated projectile animation frames
            _currentAnimationFrames = _projectileAnimation;
        }

        public Bullet(Texture2D texture, float layer, float rotation) : base(texture, layer)
        {
            // Calculate dimensions of frames for the projectile's animation
            TextureWidth = texture.Width / 4; // Assuming the texture is divided into 4 frames horizontally
            TextureHeight = texture.Height;

            // Assign the rotation value to the private field _rotation
            _rotation = rotation;

            // Initialize lists to store frames for the projectile's animation
            _currentAnimationFrames = new List<Rectangle>();
            _projectileAnimation = new List<Rectangle>();

            // Create rectangles for each frame of the projectile's animation
            for (int x = 0; x < 4; x++) // Assuming 4 frames for the projectile's animation
            {
                _projectileAnimation.Add(new Rectangle(x * TextureWidth, 0, TextureWidth, TextureHeight));
            }

            // Set the current animation frames to the generated projectile animation frames
            _currentAnimationFrames = _projectileAnimation;
        }


        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            // Increment the timer by the elapsed time since the last update
            _timer += (float)gametime.ElapsedGameTime.TotalSeconds;

            // If the tracker reaches a certain value, reset it to 0
            if (_tracker == 3)
            {
                _tracker = 0;
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
                _tracker += 1;

                // Reset the timer to restart the frame change interval
                _timer = 0;
            }

            // Call the base class's Update method to perform additional updates
            base.Update(gametime, sprites);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, _currentAnimationFrames[_tracker], Color.White, _rotation, new Vector2(0, 0), 1f, SpriteEffects.None, _layer);
        }

    }
}
