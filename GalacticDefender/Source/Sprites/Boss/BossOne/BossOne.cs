/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NDJPFinal.Source.Sprites.Hero;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NDJPFinal.Source.Sprites.Boss.BossOne
{
    public class BossOne : Sprite
    {
        // Instances of Bullet and BossProjectileOne classes
        public Bullet Bullet;
        public BossProjectileOne BossProjectileOne;


        #region Properities
        // Vector representing the direction of the sprite
        public Vector2 Direction;

        // Boolean indicating whether there has been a change in direction
        public bool DireactionChange;

        // List to store rectangles representing frames for the sprite's animation
        private List<Rectangle> _spriteSheetFrames;

        // Keeps track of the current frame of the sprite's animation
        private int _spriteFrameTracker;

        // Time interval for attack or action (e.g., time between attacks)
        public float AttackIntervel;

        // Property representing the collision rectangle of the object
        public Rectangle Collision
        {
            get
            {
                // Returns a rectangle that defines the collision area based on the sprite's position, width, and height
                return new Rectangle(
                    (int)Position.X - 5,             // X coordinate of the collision area (adjusted by -5)
                    (int)Position.Y - 5,             // Y coordinate of the collision area (adjusted by -5)
                    TextureWidth / 2,                // Width of the collision area (half of the texture width)
                    TextureHeight / 2                // Height of the collision area (half of the texture height)
                );
            }
        }

        // Variables to handle time-related functionality
        private float _time;
        private float _spriteTime;

        // Speed of the object
        public float _speed;

        // Width and height of the object's texture
        public int TextureWidth;
        public int TextureHeight;

        // Flags to control certain states or conditions
        private bool _reveseTrue = false; // Indicates a reverse state

        // Random instance for generating random values
        Random random = new Random();
        #endregion


        public BossOne(Texture2D texture, float layer, int frames) : base(texture, layer)
        {
            // Set the texture for the object
            this._texture = texture;

            // Set the default direction of the object (moving to the right)
            Direction = new Vector2(3, 0);

            // Set a random speed for the object (range: 0 to 4)
            _speed = random.Next(0, 5);

            // Calculate the width and height of each frame of the sprite sheet
            TextureWidth = texture.Width / frames;  // Assuming 'frames' represents the number of frames horizontally
            TextureHeight = texture.Height;

            // Initialize the list to store rectangles representing frames of the sprite sheet
            _spriteSheetFrames = new List<Rectangle>();

            // Create rectangles for each frame of the sprite sheet's animation
            for (int x = 0; x < frames; x++)
            {
                _spriteSheetFrames.Add(new Rectangle(x * TextureWidth, 0, TextureWidth, TextureHeight));
            }
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {

            // Update the object's position based on its direction
            Position += Direction;

            // Check if the specified time has passed since the last attack
            if (gametime.TotalGameTime.TotalSeconds - _time > AttackIntervel)
            {
                // If enough time has passed, add a bullet (assuming AddBullet method adds a bullet to the sprites list)
                AddBullet(sprites);

                // Update the time to the current game time for tracking the attack interval
                _time = (float)gametime.TotalGameTime.TotalSeconds;
            }

            // Check if the specified time has passed since the last sprite update
            if (gametime.TotalGameTime.TotalSeconds - _spriteTime > 0.4f)
            {
                // Check conditions for sprite animation or frame updates
                if (!_reveseTrue && _spriteFrameTracker == _spriteSheetFrames.Count() - 1)
                {
                    // If not in reverse mode and at the last frame, reset to the first frame
                    _spriteFrameTracker = 0;
                }

                // Move to the next frame in the sprite's animation sequence
                _spriteFrameTracker++;

                // Update the time for sprite animation to the current game time
                _spriteTime = (float)gametime.TotalGameTime.TotalSeconds;
            }

            base.Update(gametime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(base._texture, Position, _spriteSheetFrames[_spriteFrameTracker], Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, _layer);
        }


        // Method to add a bullet (assumed to be of type BossProjectileOne) to the sprites list
        private void AddBullet(List<Sprite> sprites)
        {
            // Create a new instance of BossProjectileOne by cloning the BossProjectileOne object
            var bullet = BossProjectileOne.Clone() as BossProjectileOne;

            // Set the position of the bullet relative to the center of the boss object
            bullet.Position = Position + new Vector2(TextureWidth / 2, TextureHeight / 2);

            // Set the linear velocity of the bullet based on the boss's linear velocity and speed, in the opposite direction
            bullet.LinearVelocity = (LinearVelocity + _speed) * -1;

            // Set the lifespan of the bullet (time before the bullet disappears)
            bullet.LifeSpan = 2f;

            // Set the parent of the bullet to this boss object
            bullet.Parent = this;

            // Add the bullet to the sprites list
            sprites.Add(bullet);
        }
    }
}
