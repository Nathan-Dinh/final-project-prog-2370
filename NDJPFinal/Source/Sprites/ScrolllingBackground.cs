/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace NDJPFinal.Source.Sprites
{
    public class ScrolllingBackground : Sprite
    {
        #region 2DTexture
        private Texture2D _backgroundTexture;
        #endregion

        #region Properties
        // List of rectangles representing frames for the current animation
        private List<Rectangle> _currentAnimationFrames;

        // List of rectangles representing frames for the background animation
        private List<Rectangle> _backgroundAnimationFrames;

        // Index of the current image/frame for the first animation
        private int _currentImage1;

        // Index of the current image/frame for the second animation
        private int _currentImage2;

        // Number of rows in the sprite sheet
        private int _rows = 2;

        // Number of columns in the sprite sheet
        private int _columns = 3;

        // Width of a single sprite in the sprite sheet
        public int SpriteWidth;

        // Height of a single sprite in the sprite sheet
        public int SpriteHeight;

        // Position of the first entity or sprite
        private Vector2 _position1;

        // Position of the second entity or sprite
        private Vector2 _position2;

        // Velocity vector for movement
        private Vector2 _velocity;

        // Random object for generating random numbers
        Random random = new Random();
        #endregion

        public ScrolllingBackground(Texture2D texture,float layer,Vector2 position) : base(texture,layer)
        {
            // Assigns the background texture
            this._backgroundTexture = texture;

            // Sets the velocity for scrolling the background vertically
            this._velocity = new Vector2(0, 2);

            // Initializes variables to handle current images
            this._currentImage1 = 0;
            this._currentImage2 = 1;

            // Determines the width and height of each sprite frame
            var frameWidth = (texture.Width / 3);
            var frameHeight = (texture.Height / 2);
            this.SpriteWidth = frameWidth;
            this.SpriteHeight = frameHeight;

            // Initializes lists to store animation frames for the background
            _currentAnimationFrames = new List<Rectangle>();
            _backgroundAnimationFrames = new List<Rectangle>();

            // Generates rectangles for each frame of the background animation
            for (int r = 0; r < _rows; r++)
            {
                for (int c = 0; c < _columns; c++)
                {
                    _backgroundAnimationFrames.Add(new Rectangle((c * frameWidth), (r * frameHeight), frameWidth, frameHeight));
                }
            }

            // Sets the current animation frames to the generated background animation frames
            _currentAnimationFrames = _backgroundAnimationFrames;

            // Sets the initial positions for the two background sections
            this._postion1 = position;
            this._postion2 = new Vector2(_postion1.X, (_postion1.Y - _currentAnimationFrames[0].Height));
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            MoveBackground(gameTime);
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_backgroundTexture, _postion1, _currentAnimationFrames[_currentImage2], Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(_backgroundTexture, _postion2, _currentAnimationFrames[_currentImage1], Color.White,0f,new Vector2(0,0), 1f, SpriteEffects.None, 0.0f);
        }

        private void MoveBackground(GameTime gameTime)
        {
            // Update the positions of the two background sections based on their velocity and elapsed time
            this._postion1 += _velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.05f;
            this._postion2 += _velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.05f;

            // Check if the first background section has moved beyond its height
            if (_postion1.Y > _currentAnimationFrames[0].Height)
            {
                // Randomly select a new image for the second section
                _currentImage2 = random.Next(0, 5);

                // Reset the position of the first section to be above the second section with the new image
                _postion1.Y = _postion2.Y - _currentAnimationFrames[_currentImage2].Height;
            }

            // Check if the second background section has moved beyond its height
            if (_postion2.Y > _currentAnimationFrames[0].Height)
            {
                // Randomly select a new image for the first section
                _currentImage1 = random.Next(0, 5);

                // Reset the position of the second section to be above the first section with the new image
                _postion2.Y = _postion1.X - _currentAnimationFrames[_currentImage1].Height;
            }
        }
    }
}
