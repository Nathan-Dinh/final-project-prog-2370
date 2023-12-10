/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace NDJPFinal.Source.Sprites.Boss.BossOne
{
    public class BossProjectileOne : Sprite
    {
        // Timer used for some timing functionality
        private float _timer;

        // List to store rectangles (presumably source rectangles for sprites or textures)
        private List<Rectangle> _sourceRectangles = new List<Rectangle>();

        // Width of the texture
        public int TextureWidth;

        // Height of the texture
        public int TextureHeight;

        // Tracker to keep track of something (e.g., frames or iterations)
        private int _tracker;

        // Property representing a rectangle associated with the position
        public Rectangle Collision
        {
            get
            {
                // Returns a rectangle starting at the Position coordinates with a width and height of 0
                return new Rectangle((int)Position.X, (int)Position.Y, 0, 0);
            }
        }

        public BossProjectileOne(Texture2D texture, float layer) : base(texture, layer)
        {
            // Calculate the width of each individual texture frame or segment by dividing the texture width by 10
            TextureWidth = texture.Width / 10;

            // Set the texture height as the original texture height
            TextureHeight = texture.Height;

            // Loop 10 times to create source rectangles for each segment/frame of the texture
            for (int i = 0; i < 10; i++)
            {
                // Create a new rectangle and add it to the _sourceRectangles list
                // The rectangle's X-coordinate and width are based on the calculated textureWidth for each frame
                // The rectangles are aligned horizontally from left to right (assuming a horizontal sprite sheet)
                _sourceRectangles.Add(new Rectangle(TextureWidth * i, 0, TextureWidth, TextureHeight));
            }
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            // Increment the timer by the elapsed time since the last update
            _timer += (float)gametime.ElapsedGameTime.TotalSeconds;

            // Check if the tracker value has reached 3, and if so, reset it to 0
            if (_tracker == 3)
            {
                _tracker = 0;
            }

            // Check if the timer has exceeded the object's lifespan
            // If the lifespan has been exceeded, set the IsRemoved flag to true
            if (_timer > LifeSpan)
            {
                IsRemoved = true;
            }

            // Move the object's position upwards based on its linear velocity
            Position.Y -= LinearVelocity;

            // Check if the timer has exceeded 0.25 seconds
            // If it has, increment the tracker and reset the timer to 0
            if (_timer > 0.25)
            {
                _tracker += 1;
                _timer = 0;
            }

            base.Update(gametime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, _sourceRectangles[_tracker], Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, _layer);
        }
    }
}
